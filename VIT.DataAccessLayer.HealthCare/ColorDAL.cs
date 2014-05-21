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
    /// The custom ColorDAL
    /// </summary>
    public partial class ColorDAL
    {
        public IQueryable<Color> GetAllIncludeItem()
        {
            var entity = this.GetAll()
                .Include(color => color.Item);

            return entity;
        }

        public IQueryable<Color> GetAllIncludeStyle()
        {
            var entity = this.GetAll()
               .Include(color => color.Styles);

            return entity;
        }

        public IQueryable<Color> GetAllIncludes()
        {
            var entity = this.GetAll()
               .Include(color => color.Item)
               .Include(color => color.Styles);

            return entity;
        }
    }

    /// <summary>
    /// The IColorDAL
    /// </summary>
    public partial interface IColorDAL
    {
        /// <summary>
        /// Get all Color join Item
        /// </summary>
        /// <returns>
        /// List Color
        /// </returns>
        IQueryable<Color> GetAllIncludeItem();

        /// <summary>
        /// Get all Color join Style
        /// </summary>
        /// <returns>
        /// List Color
        /// </returns>
        IQueryable<Color> GetAllIncludeStyle();

        /// <summary>
        /// Get all Color join Item and Style
        /// </summary>
        /// <returns>
        /// List Color
        /// </returns>
        IQueryable<Color> GetAllIncludes();
    }
}
