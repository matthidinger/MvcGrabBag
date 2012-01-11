using System;

namespace MvcGrabBag.Web.Caching
{
    /// <summary>
    /// Provides caching storage of items
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// Gets an item from the cache if it exists; creates and stores it into the cache otherwise
        /// </summary>
        /// <typeparam name="T">The type of item to pull from cache</typeparam>
        /// <param name="scope">Cache the item unique to the user or for shared throughout the application</param>
        /// <param name="key">The unique key that the item is cached as</param>
        /// <param name="minutes">How long to keep the item in cache</param>
        /// <param name="createNew">A function to createNew the item if it does not already exist in the cache</param>
        T Get<T>(CacheScope scope, string key, int minutes, Func<T> createNew);

        /// <summary>
        /// Remove an item from the cache based on the unique key
        /// </summary>
        /// <param name="key">The unique key that the item is cached as</param>
        void Remove(string key);
    }
}