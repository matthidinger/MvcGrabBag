﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace MvcGrabBag.Web.EntityFramework
{
    public static class DbSetHelper
    {
        public static IQueryable<TEntity> Unfiltered<TEntity>(this IDbSet<TEntity> set) where TEntity : class
        {
            var filteredDbSet = set as FilteredDbSet<TEntity>;
            return filteredDbSet != null ? filteredDbSet.Unfiltered() : set;
        }
    }

    public class FilteredDbSet<TEntity> : IDbSet<TEntity>, IOrderedQueryable<TEntity>, IListSource
            where TEntity : class
    {
        private readonly DbSet<TEntity> _set;
        private readonly IQueryable<TEntity> _filteredSet;
        private readonly Action<TEntity> _initializeEntity;

        public FilteredDbSet(DbContext context)
            : this(context.Set<TEntity>(), i => true, null)
        {
        }

        public FilteredDbSet(DbContext context, Expression<Func<TEntity, bool>> filter)
            : this(context.Set<TEntity>(), filter, null)
        {
        }

        public FilteredDbSet(DbContext context, Expression<Func<TEntity, bool>> filter, Action<TEntity> initializeEntity)
            : this(context.Set<TEntity>(), filter, initializeEntity)
        {
        }

        private FilteredDbSet(DbSet<TEntity> set, Expression<Func<TEntity, bool>> filter, Action<TEntity> initializeEntity)
        {
            _set = set;
            _filteredSet = set.Where(filter);
            MatchesFilter = filter.Compile();
            _initializeEntity = initializeEntity;
        }

        public IQueryable<TEntity> Unfiltered()
        {
            return _set;
        }

        public Func<TEntity, bool> MatchesFilter { get; private set; }

        public void ThrowIfEntityDoesNotMatchFilter(TEntity entity)
        {
            if (!MatchesFilter(entity))
                throw new ArgumentOutOfRangeException();
        }

        public TEntity Add(TEntity entity)
        {
            DoInitializeEntity(entity);
            ThrowIfEntityDoesNotMatchFilter(entity);
            return _set.Add(entity);
        }

        public TEntity Attach(TEntity entity)
        {
            ThrowIfEntityDoesNotMatchFilter(entity);
            return _set.Attach(entity);
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity
        {
            var entity = _set.Create<TDerivedEntity>();
            DoInitializeEntity(entity);
            return entity;
        }

        public TEntity Create()
        {
            var entity = _set.Create();
            DoInitializeEntity(entity);
            return entity;
        }

        public TEntity Find(params object[] keyValues)
        {
            var entity = _set.Find(keyValues);
            if (entity == null)
                return null;

            // If the user queried an item outside the filter, then we throw an error.
            // If IDbSet had a Detach method we would use it...sadly, we have to be ok with the item being in the Set.
            ThrowIfEntityDoesNotMatchFilter(entity);
            return entity;
        }

        public TEntity Remove(TEntity entity)
        {
            ThrowIfEntityDoesNotMatchFilter(entity);
            return _set.Remove(entity);
        }

        /// <summary>
        /// Returns the items in the local cache
        /// </summary>
        /// <remarks>
        /// It is possible to add/remove entities via this property that do NOT match the filter.
        /// Use the <see cref="ThrowIfEntityDoesNotMatchFilter"/> method before adding/removing an item from this collection.
        /// </remarks>
        public ObservableCollection<TEntity> Local { get { return _set.Local; } }

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator() { return _filteredSet.GetEnumerator(); }

        IEnumerator IEnumerable.GetEnumerator() { return _filteredSet.GetEnumerator(); }

        Type IQueryable.ElementType { get { return typeof(TEntity); } }

        Expression IQueryable.Expression { get { return _filteredSet.Expression; } }

        IQueryProvider IQueryable.Provider { get { return _filteredSet.Provider; } }

        bool IListSource.ContainsListCollection { get { return false; } }

        IList IListSource.GetList() { throw new InvalidOperationException(); }

        void DoInitializeEntity(TEntity entity)
        {
            if (_initializeEntity != null)
                _initializeEntity(entity);
        }
    }

}