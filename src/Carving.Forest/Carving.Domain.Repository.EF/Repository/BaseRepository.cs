// Carving.Domain.Repository.EF BaseRepository.cs
// writer sundy
// Last Update Time 2015-04-14-20:34
// Create Time 2015-04-14-20:34

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Carving.Domain.Core;
using Carving.Domain.Core.Repositories;
using Carving.Domain.Core.Specifications;
using Carving.Infrastructrue;

namespace Carving.Domain.Repository.EF.Repository
{
    public class BaseRepository<TAggregateRoot> : Repository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {
        private readonly IEntityFrameworkRepositoryContext _baseContext;

        public BaseRepository(IRepositoryContext context)
            : base(context)
        {
            if (context is IEntityFrameworkRepositoryContext)
                _baseContext = context as IEntityFrameworkRepositoryContext;
        }

        protected IEntityFrameworkRepositoryContext EFContext
        {
            get { return _baseContext; }
        }

        private MemberExpression GetMemberInfo(LambdaExpression lambda)
        {
            if (lambda == null)
                throw new ArgumentNullException("method");

            MemberExpression memberExpr = null;

            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                memberExpr =
                    ((UnaryExpression)lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }

            if (memberExpr == null)
                throw new ArgumentException("method");

            return memberExpr;
        }

        private string GetEagerLoadingPath(Expression<Func<TAggregateRoot, dynamic>> eagerLoadingProperty)
        {
            var memberExpression = GetMemberInfo(eagerLoadingProperty);
            var parameterName = eagerLoadingProperty.Parameters.First().Name;
            var memberExpressionStr = memberExpression.ToString();
            var path = memberExpressionStr.Replace(parameterName + ".", "");
            return path;
        }

        protected override void DoAdd(TAggregateRoot aggregateRoot)
        {
            _baseContext.RegisterNew(aggregateRoot);
        }

        protected override TAggregateRoot DoGetByKey(Guid key)
        {
            return _baseContext.Context.Set<TAggregateRoot>().First(p => p.ID == key);
        }

        protected override IEnumerable<TAggregateRoot> DoGetAll(ISpecification<TAggregateRoot> specification,
            Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            var results = DoFindAll(specification, sortPredicate, sortOrder);
            if (results == null || !results.Any())
                throw new ArgumentException("无法根据指定的查询条件找到所需的聚合根。");
            return results;
        }

        protected override PagedResult<TAggregateRoot> DoGetAll(ISpecification<TAggregateRoot> specification,
            Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            var results = DoFindAll(specification, sortPredicate, sortOrder, pageNumber, pageSize);
            if (results == null || results == PagedResult<TAggregateRoot>.Empty || results.Data.Count() == 0)
                throw new ArgumentException("无法根据指定的查询条件找到所需的聚合根。");
            return results;
        }

        protected override IEnumerable<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification,
            Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            var query = _baseContext.Context.Set<TAggregateRoot>()
                .Where(specification.GetExpression());
            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        return query.SortBy(sortPredicate).ToList();
                    case SortOrder.Descending:
                        return query.SortByDescending(sortPredicate).ToList();
                    default:
                        break;
                }
            }
            return query.ToList();
        }

        protected override PagedResult<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification,
            Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "页码必须大于或等于1。");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "每页大小必须大于或等于1。");

            var query = _baseContext.Context.Set<TAggregateRoot>()
                .Where(specification.GetExpression());
            var skip = (pageNumber - 1) * pageSize;
            var take = pageSize;
            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        var pagedGroupAscending =
                            query.SortBy(sortPredicate)
                                .Skip(skip)
                                .Take(take)
                                .GroupBy(p => new { Total = query.Count() })
                                .FirstOrDefault();
                        if (pagedGroupAscending == null)
                            return null;
                        return new PagedResult<TAggregateRoot>(pagedGroupAscending.Key.Total,
                            (pagedGroupAscending.Key.Total + pageSize - 1) / pageSize, pageSize, pageNumber,
                            pagedGroupAscending.Select(p => p).ToList());
                    case SortOrder.Descending:
                        var pagedGroupDescending =
                            query.SortByDescending(sortPredicate)
                                .Skip(skip)
                                .Take(take)
                                .GroupBy(p => new { Total = query.Count() })
                                .FirstOrDefault();
                        if (pagedGroupDescending == null)
                            return null;
                        return new PagedResult<TAggregateRoot>(pagedGroupDescending.Key.Total,
                            (pagedGroupDescending.Key.Total + pageSize - 1) / pageSize, pageSize, pageNumber,
                            pagedGroupDescending.Select(p => p).ToList());
                    default:
                        break;
                }
            }
            throw new InvalidOperationException("基于分页功能的查询必须指定排序字段和排序顺序。");
        }

        protected override TAggregateRoot DoGet(ISpecification<TAggregateRoot> specification)
        {
            var result = DoFind(specification);
            if (result == null)
                throw new ArgumentException("无法根据指定的查询条件找到所需的聚合根。");
            return result;
        }

        protected override TAggregateRoot DoFind(ISpecification<TAggregateRoot> specification)
        {
            return _baseContext.Context.Set<TAggregateRoot>().Where(specification.IsSatisfiedBy).FirstOrDefault();
        }

        protected override bool DoExists(ISpecification<TAggregateRoot> specification)
        {
            var count = _baseContext.Context.Set<TAggregateRoot>().Count(specification.IsSatisfiedBy);
            return count != 0;
        }

        protected override void DoRemove(TAggregateRoot aggregateRoot)
        {
            _baseContext.RegisterDeleted(aggregateRoot);
        }

        protected override void DoUpdate(TAggregateRoot aggregateRoot)
        {
            _baseContext.RegisterModified(aggregateRoot);
        }

        protected override TAggregateRoot DoFind(ISpecification<TAggregateRoot> specification,
            params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            var dbset = _baseContext.Context.Set<TAggregateRoot>();
            if (eagerLoadingProperties != null &&
                eagerLoadingProperties.Length > 0)
            {
                var eagerLoadingProperty = eagerLoadingProperties[0];
                var eagerLoadingPath = GetEagerLoadingPath(eagerLoadingProperty);
                var dbquery = dbset.Include(eagerLoadingPath);
                for (var i = 1; i < eagerLoadingProperties.Length; i++)
                {
                    eagerLoadingProperty = eagerLoadingProperties[i];
                    eagerLoadingPath = GetEagerLoadingPath(eagerLoadingProperty);
                    dbquery = dbquery.Include(eagerLoadingPath);
                }
                return dbquery.Where(specification.GetExpression()).FirstOrDefault();
            }
            return dbset.Where(specification.GetExpression()).FirstOrDefault();
        }

        protected override IEnumerable<TAggregateRoot> DoGetAll(ISpecification<TAggregateRoot> specification,
            Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder,
            params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            var results = DoFindAll(specification, sortPredicate, sortOrder, eagerLoadingProperties);
            if (results == null || results.Count() == 0)
                throw new ArgumentException("无法根据指定的查询条件找到所需的聚合根。");
            return results;
        }

        protected override PagedResult<TAggregateRoot> DoGetAll(ISpecification<TAggregateRoot> specification,
            Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize,
            params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            var results = DoFindAll(specification, sortPredicate, sortOrder, pageNumber, pageSize,
                eagerLoadingProperties);
            if (results == null || results == PagedResult<TAggregateRoot>.Empty || results.Data.Count() == 0)
                throw new ArgumentException("无法根据指定的查询条件找到所需的聚合根。");
            return results;
        }

        protected override IEnumerable<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification,
            Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder,
            params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            var dbset = _baseContext.Context.Set<TAggregateRoot>();
            IQueryable<TAggregateRoot> queryable = null;
            if (eagerLoadingProperties != null &&
                eagerLoadingProperties.Length > 0)
            {
                var eagerLoadingProperty = eagerLoadingProperties[0];
                var eagerLoadingPath = GetEagerLoadingPath(eagerLoadingProperty);
                var dbquery = dbset.Include(eagerLoadingPath);
                for (var i = 1; i < eagerLoadingProperties.Length; i++)
                {
                    eagerLoadingProperty = eagerLoadingProperties[i];
                    eagerLoadingPath = GetEagerLoadingPath(eagerLoadingProperty);
                    dbquery = dbquery.Include(eagerLoadingPath);
                }
                queryable = dbquery.Where(specification.GetExpression());
            }
            else
                queryable = dbset.Where(specification.GetExpression());

            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        return queryable.SortBy(sortPredicate).ToList();
                    case SortOrder.Descending:
                        return queryable.SortByDescending(sortPredicate).ToList();
                    default:
                        break;
                }
            }
            return queryable.ToList();
        }

        protected override PagedResult<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification,
            Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize,
            params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "页码必须大于或等于1。");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "每页大小必须大于或等于1。");

            var skip = (pageNumber - 1) * pageSize;
            var take = pageSize;

            var dbset = _baseContext.Context.Set<TAggregateRoot>();
            IQueryable<TAggregateRoot> queryable = null;
            if (eagerLoadingProperties != null &&
                eagerLoadingProperties.Length > 0)
            {
                var eagerLoadingProperty = eagerLoadingProperties[0];
                var eagerLoadingPath = GetEagerLoadingPath(eagerLoadingProperty);
                var dbquery = dbset.Include(eagerLoadingPath);
                for (var i = 1; i < eagerLoadingProperties.Length; i++)
                {
                    eagerLoadingProperty = eagerLoadingProperties[i];
                    eagerLoadingPath = GetEagerLoadingPath(eagerLoadingProperty);
                    dbquery = dbquery.Include(eagerLoadingPath);
                }
                queryable = dbquery.Where(specification.GetExpression());
            }
            else
                queryable = dbset.Where(specification.GetExpression());

            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        var pagedGroupAscending =
                            queryable.SortBy(sortPredicate)
                                .Skip(skip)
                                .Take(take)
                                .GroupBy(p => new { Total = queryable.Count() })
                                .FirstOrDefault();
                        if (pagedGroupAscending == null)
                            return null;
                        return new PagedResult<TAggregateRoot>(pagedGroupAscending.Key.Total,
                            (pagedGroupAscending.Key.Total + pageSize - 1) / pageSize, pageSize, pageNumber,
                            pagedGroupAscending.Select(p => p).ToList());
                    case SortOrder.Descending:
                        var pagedGroupDescending =
                            queryable.SortByDescending(sortPredicate)
                                .Skip(skip)
                                .Take(take)
                                .GroupBy(p => new { Total = queryable.Count() })
                                .FirstOrDefault();
                        if (pagedGroupDescending == null)
                            return null;
                        return new PagedResult<TAggregateRoot>(pagedGroupDescending.Key.Total,
                            (pagedGroupDescending.Key.Total + pageSize - 1) / pageSize, pageSize, pageNumber,
                            pagedGroupDescending.Select(p => p).ToList());
                    default:
                        break;
                }
            }
            throw new InvalidOperationException("基于分页功能的查询必须指定排序字段和排序顺序。");
        }

        protected override TAggregateRoot DoGet(ISpecification<TAggregateRoot> specification,
            params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            var result = DoFind(specification, eagerLoadingProperties);
            if (result == null)
                throw new ArgumentException("无法根据指定的查询条件找到所需的聚合根。");
            return result;
        }
    }
}