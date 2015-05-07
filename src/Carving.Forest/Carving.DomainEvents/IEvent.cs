﻿// Carving.DomainEvents IEvent.cs
// writer sundy
// Last Update Time 2015-04-29-21:38
// Create Time 2015-04-29-21:37

using System;


namespace Carving.Domain.Events
{
    public interface IEvent
    {
        #region Properties
        /// <summary>
        /// 获取事件的全局唯一标识。
        /// </summary>
        Guid ID { get; }
        /// <summary>
        /// 获取产生事件的时间戳。
        /// </summary>
        /// <remarks>为了支持事件产生时间的一致性，通常事件时间戳都是以UTC的方式进行表述。
        /// 对于应用系统本身而言，可以根据具体需求将UTC时间转换为本地时间。</remarks>
        DateTime TimeStamp { get; }
        #endregion
    }
}