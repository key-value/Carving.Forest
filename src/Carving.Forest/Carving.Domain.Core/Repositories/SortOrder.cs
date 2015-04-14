// Carving.Domain.Core SortOrder.cs
// writer sundy
// Last Update Time 2015-04-14-20:31
// Create Time 2015-04-14-20:31
namespace Carving.Domain.Core.Repositories
{
    public enum SortOrder
    {
        /// <summary>
        /// Indicates that the sorting style is not specified.
        /// </summary>
        Unspecified = -1,
        /// <summary>
        /// Indicates an ascending sorting.
        /// </summary>
        Ascending = 0,
        /// <summary>
        /// Indicates a descending sorting.
        /// </summary>
        Descending = 1
    }
}