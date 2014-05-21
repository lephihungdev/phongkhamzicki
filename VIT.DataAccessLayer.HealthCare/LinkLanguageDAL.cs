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
    /// The custom LinkLanguageDAL
    /// </summary>
    public partial class LinkLanguageDAL
    {
        public IQueryable<LinkLanguage> GetAllIncludeLink()
        {
            var entity = this.GetAll()
                .Include(c => c.Link);

            return entity;
        }

        public IQueryable<LinkLanguage> GetAllIncludeItem()
        {
            var entity = this.GetAll()
                .Include(c => c.Link)
                .Include(c => c.Link.Item);

            return entity;
        }

        public IQueryable<LinkLanguage> GetAllIncludeCategory()
        {
            var entity = this.GetAll()
                .Include(c => c.Link)
                .Include(c => c.Link.Category);

            return entity;
        }

        public IQueryable<LinkLanguage> GetAllIncludeItemAndCategory()
        {
            var entity = this.GetAll()
                .Include(c => c.Link)
                .Include(c => c.Link.Item)
                .Include(c => c.Link.Category);

            return entity;
        }
    }

    /// <summary>
    /// The ICategoryLanguageDAL
    /// </summary>
    public partial interface ILinkLanguageDAL
    {
        /// <summary>
        /// Get all LinkLanguage join Link
        /// </summary>
        /// <returns>
        /// List LinkLanguage
        /// </returns>
        IQueryable<LinkLanguage> GetAllIncludeLink();

        /// <summary>
        /// Get all LinkLanguage join Item
        /// </summary>
        /// <returns>
        /// List LinkLanguage
        /// </returns>
        IQueryable<LinkLanguage> GetAllIncludeItem();

        /// <summary>
        /// Get all LinkLanguage join Category
        /// </summary>
        /// <returns>
        /// List LinkLanguage
        /// </returns>
        IQueryable<LinkLanguage> GetAllIncludeCategory();

        /// <summary>
        /// Get all LinkLanguage join Item and Category
        /// </summary>
        /// <returns>
        /// List LinkLanguage
        /// </returns>
        IQueryable<LinkLanguage> GetAllIncludeItemAndCategory();
    }
}
