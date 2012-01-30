using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;

namespace MvcGrabBag.Web.EntityFramework
{
    public interface IDataContext
    {
        IDbSet<Category> Categories { get; }
        IDbSet<Product> Products { get; }
        IDatabase Database { get; }
        int SaveChanges();
    }

    public interface IDatabase
    {
        int ExecuteSqlCommand(string sql, params object[] parameters);
        IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters);
        IEnumerable SqlQuery(Type elementType, string sql, params object[] parameters);
    }
}