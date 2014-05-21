// -----------------------------------------------------------------------
// <copyright file="ArticleDAL.cs" company="VIT">
// VIT @ 2012
// </copyright>
// -----------------------------------------------------------------------

namespace VIT.DataAccessLayer
{
    using System.Data.Entity;
    using System.Linq;

    using VIT.Entity;

    /// <summary>
    /// The custom ArticleDAL
    /// </summary>
    public partial class ArticleDAL
    {
        public IQueryable<Article> GetAllIncludeItem()
        {
            var articles = this.GetAll()
                .Include(article => article.Item);

            return articles;
        }

        public IQueryable<Article> GetAllIncludes()
        {
            var articles = this.GetAll()
                .Include(article => article.Item)
                .Include(article => article.ArticleLanguages);

            return articles;
        }
    }

    /// <summary>
    /// The IArticleDAL
    /// </summary>
    public partial interface IArticleDAL
    {
        /// <summary>
        /// Get all Article join Item
        /// </summary>
        /// <returns>
        /// List Article
        /// </returns>
        IQueryable<Article> GetAllIncludeItem();

        /// <summary>
        /// Get all Article join Item and ArticleLanguage
        /// </summary>
        /// <returns>
        /// List Article
        /// </returns>
        IQueryable<Article> GetAllIncludes();
    }
}
