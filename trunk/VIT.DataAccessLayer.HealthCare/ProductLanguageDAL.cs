// -----------------------------------------------------------------------
// <copyright file="ProductLanguageDAL.cs" company="VIT">
// VIT @ 2012
// </copyright>
// -----------------------------------------------------------------------

namespace VIT.DataAccessLayer
{
    using System.Data.Entity;
    using System.Linq;

    using VIT.Entity;

    /// <summary>
    /// The custom ProductLanguageDAL
    /// </summary>
    public partial class ProductLanguageDAL
    {
        public IQueryable<ProductLanguage> GetAllIncludeItem()
        {
            var productLanguage = this.GetAll()
                .Include(c => c.Product)
                .Include(c => c.Product.Item);

            return productLanguage;
        }

        public IQueryable<ProductLanguage> GetAllIncludeProduct()
        {
            var productLanguage = this.GetAll()
                .Include(c => c.Product);

            return productLanguage;
        }

        public IQueryable<ProductLanguage> GetAllIncludeCategory()
        {
            var productLanguage = this.GetAll()
                .Include(c => c.Product)
                .Include(c => c.Product.Category)
                .Include(c => c.Product.Category.CategoryLanguages);

            return productLanguage;
        }

        public IQueryable<ProductLanguage> GetAllIncludeItemCategory()
        {
            var productLanguage = this.GetAll()
                .Include(c => c.Product)
                .Include(c => c.Product.Item)
                .Include(c => c.Product.Category)
                .Include(c => c.Product.Category.CategoryLanguages);

            return productLanguage;
        }

        public IQueryable<ProductLanguage> GetAllIncludeItemImage()
        {
            var productLanguage = this.GetAll()
                .Include(c => c.Product)
                .Include(c => c.Product.Item)
                .Include(c => c.Product.Item.ItemImages);

            return productLanguage;
        }

        public IQueryable<ProductLanguage> GetAllIncludeViewImage()
        {
            var productLanguage = this.GetAll()
                .Include(c => c.Product)
                .Include(c => c.Product.Item)
                .Include(c => c.Product.Item.ItemView)
                .Include(c => c.Product.Item.ItemImages);

            return productLanguage;
        }

        public IQueryable<ProductLanguage> GetAllIncludes()
        {
            var productLanguage = this.GetAll()
                .Include(c => c.Product)
                .Include(c => c.Product.Item)
                .Include(c => c.Product.Category)
                .Include(c => c.Product.Category.CategoryLanguages);

            return productLanguage;
        }
    }

    /// <summary>
    /// The IProductLanguageDAL
    /// </summary>
    public partial interface IProductLanguageDAL
    {
        /// <summary>
        /// Get all ProductLanguage join Item
        /// </summary>
        /// <returns>
        /// List ProductLanguage
        /// </returns>
        IQueryable<ProductLanguage> GetAllIncludeItem();

        /// <summary>
        /// Get all ProductLanguage join Article
        /// </summary>
        /// <returns>
        /// List ProductLanguage
        /// </returns>
        IQueryable<ProductLanguage> GetAllIncludeProduct();

        /// <summary>
        /// Get all ProductLanguage join Category
        /// </summary>
        /// <returns>
        /// List ProductLanguage
        /// </returns>
        IQueryable<ProductLanguage> GetAllIncludeCategory();

        /// <summary>
        /// Get all ProductLanguage join Category and Item
        /// </summary>
        /// <returns>
        /// List ProductLanguage
        /// </returns>
        IQueryable<ProductLanguage> GetAllIncludeItemCategory();

        /// <summary>
        /// Get all ProductLanguage join Image and Item
        /// </summary>
        /// <returns>
        /// List ProductLanguage
        /// </returns>
        IQueryable<ProductLanguage> GetAllIncludeItemImage();

        /// <summary>
        /// Get all ProductLanguage join Image and ItemView
        /// </summary>
        /// <returns>
        /// List ProductLanguage
        /// </returns>
        IQueryable<ProductLanguage> GetAllIncludeViewImage();

        /// <summary>
        /// Get all ProductLanguage join Category and Item and Manufacturer and Supplier
        /// </summary>
        /// <returns>
        /// List ProductLanguage
        /// </returns>
        IQueryable<ProductLanguage> GetAllIncludes();
    }
}
