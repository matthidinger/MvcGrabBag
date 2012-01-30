using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;

namespace MvcGrabBag.Web.EntityFramework
{
    public class ECommerceDb : DbContext, IDataContext
    {
        private readonly EfDatabase _database;

        public ECommerceDb()
        {
            // Automatically filter out Categories where IsDeleted == false
            Categories = new FilteredDbSet<Category>(this, c => c.IsDeleted == false);
            _database = new EfDatabase(Database);
        }

        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Product> Products { get; set; }

        IDatabase IDataContext.Database
        {
            get { return _database; }
        }


        private class EfDatabase : IDatabase
        {
            private readonly Database _database;

            public EfDatabase(Database database)
            {
                _database = database;
            }

            public int ExecuteSqlCommand(string sql, params object[] parameters)
            {
                return _database.ExecuteSqlCommand(sql, parameters);
            }

            public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
            {
                return _database.SqlQuery<TElement>(sql, parameters);
            }

            public IEnumerable SqlQuery(Type elementType, string sql, params object[] parameters)
            {
                return _database.SqlQuery(elementType, sql, parameters);
            }
        }
    }
}