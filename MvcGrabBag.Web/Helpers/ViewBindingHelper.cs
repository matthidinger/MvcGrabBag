using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using MvcGrabBag.Web.Controllers;
using Telerik.Web.Mvc;
using Telerik.Web.Mvc.Infrastructure;
using System.Linq.Dynamic;

namespace MvcGrabBag.Web.Helpers
{
    public static class GridQueryExtensions
    {
        /// <summary>
        /// Prepare a LINQ Query for Telerik Grid binding. Will handle paging, sorting, SQL-side querying, and auto-projecting the remaining columns to enable client-side code as well
        /// </summary>
        /// <param name="query">The starting query</param>
        /// <param name="serverSelector">The SQL-side selector - only specify columns that actually be translated to SQL</param>
        /// <param name="clientSelector">The Client-side selector - free to write client-side .NET code here</param>
        public static IEnumerable ForGrid<TEntity, TServer, TClient>(this IQueryable<TEntity> query, Expression<Func<TEntity, TServer>> serverSelector, Expression<Func<TServer, TClient>> clientSelector, GridCommand command, IGridPropertyMap propertyMap, out int totalRows)
        {
            // apply filtering
            query = query.ApplyFiltering(command.FilterDescriptors, propertyMap);
            totalRows = query.Count();

            // apply ordering
            query = query.ApplySorting(command.GroupDescriptors, command.SortDescriptors, propertyMap);


            // project to serverSelector
            var serverQuery = query.Select(serverSelector);


            // apply paging to servermodel
            serverQuery = serverQuery.ApplyPaging(command.Page, command.PageSize);

            // asenumerable
            var clientQuery = serverQuery.AsEnumerable();

            // select servermodel to clientmodel
            var clientModel = clientQuery.Select(clientSelector.Compile());

            if (command.GroupDescriptors.Any())
            {
                return clientModel.AsQueryable().ApplyGrouping(command.GroupDescriptors);
            }

            return clientModel;
        }

        public static IQueryable<T> ApplyFiltering<T>(this IQueryable<T> data, IList<IFilterDescriptor> filterDescriptors, IGridPropertyMap gridPropertyMap)
        {
            var compositeDescriptors = filterDescriptors.OfType<CompositeFilterDescriptor>().SelectMany(d => d.FilterDescriptors);

            var fixedDescriptors =
                (from FilterDescriptor s in filterDescriptors.Union(compositeDescriptors).OfType<FilterDescriptor>()
                 let mappedName = gridPropertyMap.GetServerSidePropertyName(s.Member)
                 select new FilterDescriptor(mappedName, s.Operator, s.Value))
                    .Cast<IFilterDescriptor>().ToList();

            if (fixedDescriptors.Any())
            {
                var where = ExpressionBuilder.Expression<T>(fixedDescriptors);
                data = data.Where(where);
            }
            return data;
        }

        public static IEnumerable ApplyGrouping<T>(this IQueryable<T> data, IList<GroupDescriptor> groupDescriptors)
        {
            Func<IEnumerable<T>, IEnumerable<AggregateFunctionsGroup>> selector = null;
            foreach (var group in groupDescriptors.Reverse())
            {
                var groupKey = group.Member;
                var keyLambda = System.Linq.Dynamic.DynamicExpression.ParseLambda<T, object>(groupKey);

                if (selector == null)
                {
                    selector = m => BuildInnerGroup(m, keyLambda.Compile());
                }
                else
                {
                    selector = BuildGroup(keyLambda.Compile(), selector);
                }
            }
            return selector.Invoke(data).ToList();
        }

        private static Func<IEnumerable<T>, IEnumerable<AggregateFunctionsGroup>> BuildGroup<T>(Func<T, object> groupSelector, Func<IEnumerable<T>, IEnumerable<AggregateFunctionsGroup>> selectorBuilder)
        {
            var tempSelector = selectorBuilder;
            return g => g.GroupBy(groupSelector)
                            .Select(c => new AggregateFunctionsGroup
                                             {
                                                 Key = c.Key,
                                                 HasSubgroups = true,
                                                 Items = tempSelector.Invoke(c).ToList()
                                             });
        }

        private static IEnumerable<AggregateFunctionsGroup> BuildInnerGroup<T>(IEnumerable<T> group, Func<T, object> groupSelector)
        {
            return group.GroupBy(groupSelector)
                .Select(i => new AggregateFunctionsGroup
                                 {
                                     Key = i.Key,
                                     Items = i.ToList()
                                 });
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> data, int currentPage, int pageSize)
        {
            pageSize = pageSize == 0 ? 10 : pageSize;
            if (pageSize > 0 && currentPage > 0)
            {
                data = data.Skip((currentPage - 1) * pageSize);
            }
            data = data.Take(pageSize);
            return data;
        }

        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> data,
                                                    IList<GroupDescriptor> groupDescriptors, IList<SortDescriptor> sortDescriptors, IGridPropertyMap map)
        {
            if (groupDescriptors.Any())
            {
                foreach (var groupDescriptor in groupDescriptors.Reverse())
                {
                    data = AddSortExpression(data, groupDescriptor.SortDirection, map.GetServerSidePropertyName(groupDescriptor.Member));
                }
            }
            if (sortDescriptors.Any())
            {
                foreach (SortDescriptor sortDescriptor in sortDescriptors)
                {
                    data = AddSortExpression(data, sortDescriptor.SortDirection, map.GetServerSidePropertyName(sortDescriptor.Member));
                }
            }
            return data;
        }

        private static IQueryable<T> AddSortExpression<T>(IQueryable<T> data, ListSortDirection sortDirection, string memberName)
        {
            return sortDirection == ListSortDirection.Descending ? data.OrderBy(memberName + " desc") : data.OrderBy(memberName);
        }
    }

    public interface IGridPropertyMap
    {
        string GetServerSidePropertyName(string clientProperty);
    }
}


