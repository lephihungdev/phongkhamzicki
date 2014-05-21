// -----------------------------------------------------------------------
// <copyright file="CategoryDAL.cs" company="VIT">
// VIT @ 2012
// </copyright>
// -----------------------------------------------------------------------

namespace VIT.DataAccessLayer
{
    using System.Data.Entity;
    using System.Linq;

    using VIT.Entity;

    /// <summary>
    /// The custom CategoryDAL
    /// </summary>
    public partial class CategoryDAL
    {
        public IQueryable<Category> GetAllIncludeItem()
        {
            var categories = this.GetAll()
                .Include(category => category.Item);

            return categories;
        }

        public IQueryable<Category> GetAllIncludes()
        {
            var categories = this.GetAll()
                .Include(category => category.Item)
                .Include(category => category.CategoryLanguages)
                .Include(category => category.Articles)
                .Include(category => category.Products)
                .Include(category => category.Links)
                .Include(category => category.SupportOnlines)
                .Include(category => category.Files);

            return categories;
        }
    }

    /// <summary>
    /// The ICategoryLanguageDAL
    /// </summary>
    public partial interface ICategoryDAL
    {
        /// <summary>
        /// Get all CategoryLanguage join Item
        /// </summary>
        /// <returns>
        /// List Category
        /// </returns>
        IQueryable<Category> GetAllIncludeItem();

        /// <summary>
        /// Get all CategoryLanguage join Item and CategoryLanguage
        /// </summary>
        /// <returns>
        /// List Category
        /// </returns>
        IQueryable<Category> GetAllIncludes();
    }
}
