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
    /// The custom StyleDAL
    /// </summary>
    public partial class StyleDAL
    {
        public IQueryable<Style> GetAllIncludeItem()
        {
            var entity = this.GetAll()
                .Include(style => style.Item);

            return entity;
        }

        public IQueryable<Style> GetAllIncludeColor()
        {
            var entity = this.GetAll()
                .Include(style => style.Colors);

            return entity;
        }

        public IQueryable<Style> GetAllIncludes()
        {
            var entity = this.GetAll()
                .Include(style => style.Item)
                .Include(style => style.Colors);

            return entity;
        }
    }

    /// <summary>
    /// The IStyleDAL
    /// </summary>
    public partial interface IStyleDAL
    {
        /// <summary>
        /// Get all Style join Item
        /// </summary>
        /// <returns>
        /// List Style
        /// </returns>
        IQueryable<Style> GetAllIncludeItem();

        /// <summary>
        /// Get all Style join Color
        /// </summary>
        /// <returns>
        /// List Style
        /// </returns>
        IQueryable<Style> GetAllIncludeColor();

        /// <summary>
        /// Get all Style join Item and Color
        /// </summary>
        /// <returns>
        /// List Style
        /// </returns>
        IQueryable<Style> GetAllIncludes();
    }
}
