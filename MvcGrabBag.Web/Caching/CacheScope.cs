namespace MvcGrabBag.Web.Caching
{
    /// <summary>
    /// Defines how an item is stored in the cache
    /// </summary>
    public enum CacheScope
    {
        /// <summary>
        /// Cache a unique copy of the item for each user
        /// </summary>
        User,
        /// <summary>
        /// Cache a single copy of the item for the entire application
        /// </summary>
        Application
    }
}
