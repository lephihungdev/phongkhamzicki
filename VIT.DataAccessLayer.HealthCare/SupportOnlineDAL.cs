// -----------------------------------------------------------------------
// <copyright file="SupportOnlineDAL.cs" company="VIT">
// VIT @ 2012
// </copyright>
// -----------------------------------------------------------------------

namespace VIT.DataAccessLayer
{
    using System.Data.Entity;
    using System.Linq;

    using VIT.Entity;

    /// <summary>
    /// The custom SupportOnlineDAL
    /// </summary>
    public partial class SupportOnlineDAL
    {
        public IQueryable<SupportOnline> GetAllIncludeType()
        {
            var entity = this.GetAll()
                .Include(c => c.SupportOnlineType);

            return entity;
        }

        public IQueryable<SupportOnline> GetAllIncludeItem()
        {
            var entity = this.GetAll()
                .Include(c => c.Item);

            return entity;
        }

        public IQueryable<SupportOnline> GetAllIncludeCategory()
        {
            var entity = this.GetAll()
                .Include(c => c.Category);

            return entity;
        }

        public IQueryable<SupportOnline> GetAllIncludes()
        {
            var entity = this.GetAll()
                .Include(c => c.SupportOnlineType)
                .Include(c => c.Item)
                .Include(c => c.Category)
                .Include(c => c.Category.CategoryLanguages);

            return entity;
        }
    }

    /// <summary>
    /// The ICategoryLanguageDAL
    /// </summary>
    public partial interface ISupportOnlineDAL
    {
        /// <summary>
        /// Get all SupportOnline join Item
        /// </summary>
        /// <returns>
        /// List SupportOnline
        /// </returns>
        IQueryable<SupportOnline> GetAllIncludeType();

        /// <summary>
        /// Get all SupportOnline join Item
        /// </summary>
        /// <returns>
        /// List SupportOnline
        /// </returns>
        IQueryable<SupportOnline> GetAllIncludeItem();

        /// <summary>
        /// Get all SupportOnline join Category
        /// </summary>
        /// <returns>
        /// List SupportOnline
        /// </returns>
        IQueryable<SupportOnline> GetAllIncludeCategory();

        /// <summary>
        /// Get all SupportOnline join Item and Category
        /// </summary>
        /// <returns>
        /// List SupportOnline
        /// </returns>
        IQueryable<SupportOnline> GetAllIncludes();
    }
}
