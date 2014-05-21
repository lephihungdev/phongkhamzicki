// -----------------------------------------------------------------------
// <copyright file="LinkLanguageDAL.cs" company="VIT">
// VIT @ 2012
// </copyright>
// -----------------------------------------------------------------------

namespace VIT.DataAccessLayer
{
    using System.Data.Entity;
    using System.Linq;

    using VIT.Entity;

    /// <summary>
    /// The custom LinkDAL
    /// </summary>
    public partial class LinkDAL
    {
        public IQueryable<Link> GetAllIncludeItem()
        {
            var entity = this.GetAll()
                .Include(c => c.Item);

            return entity;
        }

        public IQueryable<Link> GetAllIncludeCategory()
        {
            var entity = this.GetAll()
                .Include(c => c.Category);

            return entity;
        }

        public IQueryable<Link> GetAllIncludeItemAndCategory()
        {
            var entity = this.GetAll()
                .Include(c => c.Item)
                .Include(c => c.Category);

            return entity;
        }

        public IQueryable<Link> GetAllIncludes()
        {
            var entity = this.GetAll()
                .Include(c => c.Item)
                .Include(c => c.Category)
                .Include(link => link.LinkLanguages);

            return entity;
        }
    }

    /// <summary>
    /// The ILinkDAL
    /// </summary>
    public partial interface ILinkDAL
    {
        /// <summary>
        /// Get all Link join Item
        /// </summary>
        /// <returns>
        /// List Link
        /// </returns>
        IQueryable<Link> GetAllIncludeItem();

        /// <summary>
        /// Get all Link join Category
        /// </summary>
        /// <returns>
        /// List Link
        /// </returns>
        IQueryable<Link> GetAllIncludeCategory();

        /// <summary>
        /// Get all Link join Item and Category
        /// </summary>
        /// <returns>
        /// List Link
        /// </returns>
        IQueryable<Link> GetAllIncludeItemAndCategory();

        /// <summary>
        /// Get all Link join Item and Category
        /// </summary>
        /// <returns>
        /// List Link
        /// </returns>
        IQueryable<Link> GetAllIncludes();
    }
}
