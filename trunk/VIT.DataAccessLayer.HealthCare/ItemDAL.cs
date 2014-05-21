// -----------------------------------------------------------------------
// <copyright file="ItemDAL.cs" company="VIT">
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
    public partial class ItemDAL
    {
        public IQueryable<Item> GetAllIncludeLink()
        {
            var entity = this.GetAll().Include(c => c.Link);

            return entity;
        }

        public IQueryable<Item> GetAllIncludeAll()
        {
            var entity = this.GetAll()
                .Include(c => c.Category)
                .Include(c => c.Category.CategoryLanguages)
                .Include(c => c.Article)
                .Include(c => c.Article.ArticleLanguages)
                .Include(c => c.Article.TypeArticles)
                .Include(c => c.File)
                .Include(c => c.File.FileVideo)
                .Include(c => c.File.FileAudio)
                .Include(c => c.File.FileDocument)
                .Include(c => c.File.FileLanguages)
                .Include(c => c.ItemImages)
                .Include(c => c.Link)
                .Include(c => c.Link.LinkLanguages)
                .Include(c => c.Product)
                .Include(c => c.Product.ProductLanguages)
                .Include(c => c.Product.ProductCar)
                .Include(c => c.ProductManufacturer)
                .Include(c => c.ProductModel)
                .Include(c => c.SupportOnline)
                .Include(c => c.Province)
                .Include(c => c.Color)
                .Include(c => c.Style)
                .Include(c => c.ModuleConfig)
                .Include(c => c.ModuleConfig.ModuleConfigParams)
                .Include(c => c.ModuleConfig.ModuleConfigLanguages);

            return entity;
        }
    }


    /// <summary>
    /// The IItemDAL
    /// </summary>
    public partial interface IItemDAL
    {
        /// <summary>
        /// Get all Item join Link
        /// </summary>
        /// <returns>
        /// List Item
        /// </returns>
        IQueryable<Item> GetAllIncludeLink();

        /// <summary>
        /// Get all Item -> all data
        /// </summary>
        /// <returns>
        /// List Item
        /// </returns>
        IQueryable<Item> GetAllIncludeAll();
    }
}
