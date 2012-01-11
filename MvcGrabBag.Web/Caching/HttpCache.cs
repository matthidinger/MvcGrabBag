using System;
using System.Web;

namespace MvcGrabBag.Web.Caching
{
    /// <summary>
    /// Provides caching storage of items in an HttpContext
    /// </summary>
    public class HttpCache : ICache
    {
        /// <summary>
        /// Provides a hook for unit testing
        /// </summary>
        public static Func<HttpContextBase> GetContext = () => new HttpContextWrapper(HttpContext.Current);

        /// <summary>
        /// Gets an item from the cache if it exists; creates and stores it into the cache otherwise
        /// </summary>
        /// <typeparam name="T">The type of item to pull from cache</typeparam>
        /// <param name="scope">Cache the item unique to the user or for shared throughout the application</param>
        /// <param name="key">The unique key that the item is cached as</param>
        /// <param name="minutes">How long to keep the item in cache</param>
        /// <param name="createNew">A function to create the item if it does not already exist in the cache</param>
        public T Get<T>(CacheScope scope, string key, int minutes, Func<T> createNew)
        {
            if (key == null) throw new ArgumentNullException("key");
            if (createNew == null) throw new ArgumentNullException("createNew");

            var context = GetContext();
            var userName = context.User.Identity.IsAuthenticated ? context.User.Identity.Name : "Anonymous";

            var fullKey = scope == CacheScope.User
                              ? string.Format("{0}||{1}", key, userName)
                              : string.Format("{0}", key);

            if (context.Cache[fullKey] == null)
            {
                context.Cache.Insert(fullKey,
                                     createNew(),
                                     null,
                                     DateTime.Now.AddMinutes(minutes),
                                     TimeSpan.Zero);
            }

            return (T)context.Cache[fullKey];
        }

        /// <summary>
        /// Remove an item from the cache based on the unique key
        /// </summary>
        /// <param name="key">The unique key that the item is cached as</param>
        public void Remove(string key)
        {
            var context = GetContext();
            if (context.Cache[key] != null)
                context.Cache.Remove(key);
        }
    }
}