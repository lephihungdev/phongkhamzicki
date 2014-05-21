// -----------------------------------------------------------------------
// <copyright file="ProvinceDAL.cs" company="VIT">
// VIT @ 2012
// </copyright>
// -----------------------------------------------------------------------

namespace VIT.DataAccessLayer
{
    using System.Data.Entity;
    using System.Linq;

    using VIT.Entity;

    /// <summary>
    /// The custom ProvinceDAL
    /// </summary>
    public partial class ProvinceDAL
    {
        public IQueryable<Province> GetAllIncludeItem()
        {
            var entity = this.GetAll()
                .Include(color => color.Item);

            return entity;
        }
    }

    /// <summary>
    /// The IColorDAL
    /// </summary>
    public partial interface IProvinceDAL
    {
        /// <summary>
        /// Get all Color join Item
        /// </summary>
        /// <returns>
        /// List Color
        /// </returns>
        IQueryable<Province> GetAllIncludeItem();
    }
}
