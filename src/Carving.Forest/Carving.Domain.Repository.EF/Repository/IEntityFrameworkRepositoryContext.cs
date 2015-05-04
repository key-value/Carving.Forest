// Carving.Domain.Repository.EF IBaseRepository.cs
// writer sundy
// Last Update Time 2015-04-14-20:36
// Create Time 2015-04-14-20:36

using System.Data.Entity;
using Carving.Domain.Core.Repositories;

namespace Carving.Domain.Repository.EF.Repository
{
    public interface IEntityFrameworkRepositoryContext : IRepositoryContext
    {
        #region Properties
        /// <summary>
        /// 获取当前仓储上下文所使用的Entity Framework的<see cref="DbContext"/>实例。
        /// </summary>
        DbContext Context { get; }
        #endregion
    }
}
