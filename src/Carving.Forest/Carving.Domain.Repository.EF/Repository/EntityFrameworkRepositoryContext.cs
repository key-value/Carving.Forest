// Carving.Domain.Repository.EF EntityFrameworkRepositoryContext.cs
// writer sundy
// Last Update Time 2015-04-16-16:07
// Create Time 2015-04-16-16:07

using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading;
using Carving.Domain.Core.Repositories;
using Carving.Domain.Repository.EF.DataContext.Default;
using Carving.Infrastructrue.Aop;

namespace Carving.Domain.Repository.EF.Repository
{
    [Injection(typeof(IRepositoryContext))]
    public class EntityFrameworkRepositoryContext : RepositoryContext, IEntityFrameworkRepositoryContext
    {
        private readonly ThreadLocal<DefaultContext> localCtx = new ThreadLocal<DefaultContext>(() => new DefaultContext());

        public EntityFrameworkRepositoryContext() { }

        public override void RegisterDeleted<TAggregateRoot>(TAggregateRoot obj)
        {
            localCtx.Value.Set<TAggregateRoot>().Remove(obj);
            Committed = false;
        }

        public override void RegisterModified<TAggregateRoot>(TAggregateRoot obj)
        {
            localCtx.Value.Set<TAggregateRoot>().AddOrUpdate(obj);
            Committed = false;
        }

        public override void RegisterNew<TAggregateRoot>(TAggregateRoot obj)
        {
            localCtx.Value.Set<TAggregateRoot>().Add(obj);
            Committed = false;
        }

        public override bool DistributedTransactionSupported
        {
            get { return true; }
        }

        public override void Rollback()
        {
            Committed = false;
        }

        protected override void DoCommit()
        {
            if (!Committed)
            {
                var validationErrors = localCtx.Value.GetValidationErrors();
                var count = localCtx.Value.SaveChanges();
                Committed = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!Committed)
                    Commit();
                localCtx.Value.Dispose();
                localCtx.Dispose();
                base.Dispose(disposing);
            }
        }

        #region IEntityFrameworkRepositoryContext Members

        public DbContext Context
        {
            get { return localCtx.Value; }
        }

        #endregion
    }
}
