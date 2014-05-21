// -----------------------------------------------------------------------
// <copyright file="ArticleLanguageDAL.cs" company="VIT">
// VIT @ 2012
// </copyright>
// -----------------------------------------------------------------------

namespace VIT.DataAccessLayer
{
    using System.Data.Entity;
    using System.Linq;

    using VIT.Entity;

    /// <summary>
    /// The custom ArticleLanguageDAL
    /// </summary>
    public partial class ArticleLanguageDAL
    {
        public IQueryable<ArticleLanguage> GetAllIncludeItem()
        {
            var ArticleLanguage = this.GetAll()
                .Include(c => c.Article)
                .Include(c => c.Article.Item);

            return ArticleLanguage;
        }

        public IQueryable<ArticleLanguage> GetAllIncludeItemView()
        {
            var ArticleLanguage = this.GetAll()
                .Include(c => c.Article)
                .Include(c => c.Article.Item)
                .Include(c => c.Article.Item.ItemView);

            return ArticleLanguage;
        }

        public IQueryable<ArticleLanguage> GetAllIncludeArticle()
        {
            var ArticleLanguage = this.GetAll()
                .Include(c => c.Article);

            return ArticleLanguage;
        }

        public IQueryable<ArticleLanguage> GetAllIncludeCategory()
        {
            var ArticleLanguage = this.GetAll()
                .Include(c => c.Article)
                .Include(c => c.Article.Category)
                .Include(c => c.Article.Category.CategoryLanguages);

            return ArticleLanguage;
        }

        public IQueryable<ArticleLanguage> GetAllIncludeItemCategory()
        {
            var ArticleLanguage = this.GetAll()
                .Include(c => c.Article)
                .Include(c => c.Article.Item)
                .Include(c => c.Article.Category)
                .Include(c => c.Article.Category.CategoryLanguages);

            return ArticleLanguage;
        }

        public IQueryable<ArticleLanguage> GetAllIncludeViewCategory()
        {
            var ArticleLanguage = this.GetAll()
                .Include(c => c.Article)
                .Include(c => c.Article.Item)
                .Include(c => c.Article.Item.ItemView)
                .Include(c => c.Article.Category)
                .Include(c => c.Article.Category.CategoryLanguages);

            return ArticleLanguage;
        }

        public IQueryable<ArticleLanguage> GetAllIncludes()
        {
            var ArticleLanguage = this.GetAll()
                .Include(c => c.Article)
                .Include(c => c.Article.Item)
                .Include(c => c.Article.Item.ItemView)
                .Include(c => c.Article.Item.ItemComment)
                .Include(c => c.Article.Category)
                .Include(c => c.Article.Category.CategoryLanguages);

            return ArticleLanguage;
        }
    }

    /// <summary>
    /// The IArticleLanguageDAL
    /// </summary>
    public partial interface IArticleLanguageDAL
    {
        /// <summary>
        /// Get all ArticleLanguage join Item
        /// </summary>
        /// <returns>
        /// List ArticleLanguage
        /// </returns>
        IQueryable<ArticleLanguage> GetAllIncludeItem();

        /// <summary>
        /// Get all ArticleLanguage join Item and ItemView
        /// </summary>
        /// <returns>
        /// List ArticleLanguage
        /// </returns>
        IQueryable<ArticleLanguage> GetAllIncludeItemView();

        /// <summary>
        /// Get all ArticleLanguage join Article
        /// </summary>
        /// <returns>
        /// List ArticleLanguage
        /// </returns>
        IQueryable<ArticleLanguage> GetAllIncludeArticle();

        /// <summary>
        /// Get all ArticleLanguage join Category
        /// </summary>
        /// <returns>
        /// List ArticleLanguage
        /// </returns>
        IQueryable<ArticleLanguage> GetAllIncludeCategory();

        /// <summary>
        /// Get all ArticleLanguage join Category and Item
        /// </summary>
        /// <returns>
        /// List ArticleLanguage
        /// </returns>
        IQueryable<ArticleLanguage> GetAllIncludeViewCategory();

        /// <summary>
        /// Get all ArticleLanguage join Category and Item
        /// </summary>
        /// <returns>
        /// List ArticleLanguage
        /// </returns>
        IQueryable<ArticleLanguage> GetAllIncludeItemCategory();

        /// <summary>
        /// Get all ArticleLanguage join Category and Item and Comment
        /// </summary>
        /// <returns>
        /// List ArticleLanguage
        /// </returns>
        IQueryable<ArticleLanguage> GetAllIncludes();
    }
}
