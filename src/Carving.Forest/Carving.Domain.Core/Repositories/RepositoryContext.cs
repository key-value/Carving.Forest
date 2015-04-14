// Carving.Domain.Repository.EF RepositoryContext.cs
// writer sundy
// Last Update Time 2015-04-14-20:22
// Create Time 2015-04-14-20:22

using System;
using System.Collections.Generic;
using System.Threading;
using Carving.Infrastructrue;

namespace Carving.Domain.Core.Repositories
{
    public abstract class RepositoryContext : DisposableObject, IRepositoryContext
    {
        #region IRepositoryContext Members

        /// <summary>
        ///     Gets the ID of the repository context.
        /// </summary>
        public Guid ID
        {
            get { return _id; }
        }

        /// <summary>
        ///     Registers a new object to the repository context.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of the aggregate root.</typeparam>
        /// <param name="obj">The object to be registered.</param>
        public virtual void RegisterNew<TAggregateRoot>(TAggregateRoot obj) where TAggregateRoot : class, IAggregateRoot
        {
            if (obj.ID.Equals(Guid.Empty))
                throw new ArgumentException("The ID of the object is empty.", "obj");
            //if (modifiedCollection.ContainsKey(obj.ID))
            if (_localModifiedCollection.Value.ContainsKey(obj.ID))
                throw new InvalidOperationException(
                    "The object cannot be registered as a new object since it was marked as modified.");
            if (_localNewCollection.Value.ContainsKey(obj.ID))
                throw new InvalidOperationException("The object has already been registered as a new object.");
            _localNewCollection.Value.Add(obj.ID, obj);
            _localCommitted.Value = false;
        }

        /// <summary>
        ///     Registers a modified object to the repository context.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of the aggregate root.</typeparam>
        /// <param name="obj">The object to be registered.</param>
        public virtual void RegisterModified<TAggregateRoot>(TAggregateRoot obj)
            where TAggregateRoot : class, IAggregateRoot
        {
            if (obj.ID.Equals(Guid.Empty))
                throw new ArgumentException("The ID of the object is empty.", "obj");
            if (_localDeletedCollection.Value.ContainsKey(obj.ID))
                throw new InvalidOperationException(
                    "The object cannot be registered as a modified object since it was marked as deleted.");
            if (!_localModifiedCollection.Value.ContainsKey(obj.ID) && !_localNewCollection.Value.ContainsKey(obj.ID))
                _localModifiedCollection.Value.Add(obj.ID, obj);
            _localCommitted.Value = false;
        }

        /// <summary>
        ///     Registers a deleted object to the repository context.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of the aggregate root.</typeparam>
        /// <param name="obj">The object to be registered.</param>
        public virtual void RegisterDeleted<TAggregateRoot>(TAggregateRoot obj)
            where TAggregateRoot : class, IAggregateRoot
        {
            if (obj.ID.Equals(Guid.Empty))
                throw new ArgumentException("The ID of the object is empty.", "obj");
            if (_localNewCollection.Value.ContainsKey(obj.ID))
            {
                if (_localNewCollection.Value.Remove(obj.ID))
                    return;
            }
            var removedFromModified = _localModifiedCollection.Value.Remove(obj.ID);
            var addedToDeleted = false;
            if (!_localDeletedCollection.Value.ContainsKey(obj.ID))
            {
                _localDeletedCollection.Value.Add(obj.ID, obj);
                addedToDeleted = true;
            }
            _localCommitted.Value = !(removedFromModified || addedToDeleted);
        }

        /// <summary>
        ///     获得一个<see cref="System.Boolean" />值，该值表示当前的Unit Of Work是否支持Microsoft分布式事务处理机制。
        /// </summary>
        public abstract bool DistributedTransactionSupported { get; }

        /// <summary>
        ///     Gets a <see cref="System.Boolean" /> value which indicates whether the UnitOfWork
        ///     was committed.
        /// </summary>
        public bool Committed
        {
            get { return _localCommitted.Value; }
            protected set { _localCommitted.Value = value; }
        }

        /// <summary>
        ///     Commits the UnitOfWork.
        /// </summary>
        public virtual void Commit()
        {
            DoCommit();
        }

        /// <summary>
        ///     Rolls-back the UnitOfWork.
        /// </summary>
        public abstract void Rollback();

        #endregion

        #region Private Fields

        private readonly Guid _id = Guid.NewGuid();

        private readonly ThreadLocal<Dictionary<Guid, object>> _localNewCollection =
            new ThreadLocal<Dictionary<Guid, object>>(() => new Dictionary<Guid, object>());

        private readonly ThreadLocal<Dictionary<Guid, object>> _localModifiedCollection =
            new ThreadLocal<Dictionary<Guid, object>>(() => new Dictionary<Guid, object>());

        private readonly ThreadLocal<Dictionary<Guid, object>> _localDeletedCollection =
            new ThreadLocal<Dictionary<Guid, object>>(() => new Dictionary<Guid, object>());

        private readonly ThreadLocal<bool> _localCommitted = new ThreadLocal<bool>(() => true);

        #endregion

        #region Ctor

        #endregion

        #region Protected Methods

        /// <summary>
        ///     Clears all the registration in the repository context.
        /// </summary>
        /// <remarks>Note that this can only be called after the repository context has successfully committed.</remarks>
        protected void ClearRegistrations()
        {
            _localNewCollection.Value.Clear();
            _localModifiedCollection.Value.Clear();
            _localDeletedCollection.Value.Clear();
        }

        /// <summary>
        ///     Disposes the object.
        /// </summary>
        /// <param name="disposing">
        ///     A <see cref="System.Boolean" /> value which indicates whether
        ///     the object should be disposed explicitly.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _localCommitted.Dispose();
                _localDeletedCollection.Dispose();
                _localModifiedCollection.Dispose();
                _localNewCollection.Dispose();
            }
        }

        protected abstract void DoCommit();

        #endregion

        #region Protected Properties

        /// <summary>
        ///     Gets an enumerator which iterates over the collection that contains all the objects need to be added to the
        ///     repository.
        /// </summary>
        protected IEnumerable<KeyValuePair<Guid, object>> NewCollection
        {
            get { return _localNewCollection.Value; }
        }

        /// <summary>
        ///     Gets an enumerator which iterates over the collection that contains all the objects need to be modified in the
        ///     repository.
        /// </summary>
        protected IEnumerable<KeyValuePair<Guid, object>> ModifiedCollection
        {
            get { return _localModifiedCollection.Value; }
        }

        /// <summary>
        ///     Gets an enumerator which iterates over the collection that contains all the objects need to be deleted from the
        ///     repository.
        /// </summary>
        protected IEnumerable<KeyValuePair<Guid, object>> DeletedCollection
        {
            get { return _localDeletedCollection.Value; }
        }

        #endregion
    }
}