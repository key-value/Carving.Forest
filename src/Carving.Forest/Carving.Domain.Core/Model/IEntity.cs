// Carving.Domain.Core IEntity.cs
// writer sundy
// Last Update Time 2015-04-14-20:11
// Create Time 2015-04-14-20:11

using System;

namespace Carving.Domain.Core
{
    public interface IEntity
    {
        #region Properties
        /// <summary>
        /// 获取当前领域实体类的全局唯一标识。
        /// </summary>
        Guid ID { get; }
        #endregion
    }
}