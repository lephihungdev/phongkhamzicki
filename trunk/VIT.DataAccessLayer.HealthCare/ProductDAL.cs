// -----------------------------------------------------------------------
// <copyright file="ProductDAL.cs" company="VIT">
// VIT @ 2012
// </copyright>
// -----------------------------------------------------------------------

namespace VIT.DataAccessLayer
{
    using System.Data.Entity;
    using System.Linq;

    using VIT.Entity;

    /// <summary>
    /// The custom ProductDAL
    /// </summary>
    public partial class ProductDAL
    {
        public IQueryable<Product> GetAllIncludeItem()
        {
            var Products = this.GetAll()
                .Include(Product => Product.Item);

            return Products;
        }

        public IQueryable<Product> GetAllIncludes()
        {
            var Products = this.GetAll()
                .Include(Product => Product.Item)
                .Include(Product => Product.ProductLanguages);

            return Products;
        }
    }

    /// <summary>
    /// The IProductDAL
    /// </summary>
    public partial interface IProductDAL
    {
        /// <summary>
        /// Get all Product join Item
        /// </summary>
        /// <returns>
        /// List Product
        /// </returns>
        IQueryable<Product> GetAllIncludeItem();

        /// <summary>
        /// Get all Product join Item and ProductLanguage
        /// </summary>
        /// <returns>
        /// List Product
        /// </returns>
        IQueryable<Product> GetAllIncludes();
    }
}
