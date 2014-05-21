// -----------------------------------------------------------------------
// <copyright file="CategoryLanguageDAL.cs" company="VIT">
// VIT @ 2012
// </copyright>
// -----------------------------------------------------------------------

namespace VIT.DataAccessLayer
{
    using System.Data.Entity;
    using System.Linq;

    using VIT.Entity;

    /// <summary>
    /// The custom CategoryLanguageDAL
    /// </summary>
    public partial class CategoryLanguageDAL
    {
        public IQueryable<CategoryLanguage> GetAllIncludeItem()
        {
            var categoryLanguage = this.GetAll()
                .Include(c => c.Category)
                .Include(c => c.Category.Item);

            return categoryLanguage;
        }

        public IQueryable<CategoryLanguage> GetAllIncludeItemView()
        {
            var categoryLanguage = this.GetAll()
                .Include(c => c.Category)
                .Include(c => c.Category.Item)
                .Include(c => c.Category.Item.ItemView);

            return categoryLanguage;
        }

        public IQueryable<CategoryLanguage> GetAllIncludeCategory()
        {
            var categoryLanguage = this.GetAll()
                .Include(c => c.Category);

            return categoryLanguage;
        }
    }

    /// <summary>
    /// The ICategoryLanguageDAL
    /// </summary>
    public partial interface ICategoryLanguageDAL
    {
        /// <summary>
        /// Get all CategoryLanguage join Item
        /// </summary>
        /// <returns>
        /// List CategoryLanguage
        /// </returns>
        IQueryable<CategoryLanguage> GetAllIncludeItem();

        /// <summary>
        /// Get all CategoryLanguage join Item and ItemView
        /// </summary>
        /// <returns>
        /// List CategoryLanguage
        /// </returns>
        IQueryable<CategoryLanguage> GetAllIncludeItemView();

        /// <summary>
        /// Get all CategoryLanguage join Category
        /// </summary>
        /// <returns>
        /// List CategoryLanguage
        /// </returns>
        IQueryable<CategoryLanguage> GetAllIncludeCategory();
    }
}
