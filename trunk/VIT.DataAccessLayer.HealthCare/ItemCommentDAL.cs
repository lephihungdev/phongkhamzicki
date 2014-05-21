// -----------------------------------------------------------------------
// <copyright file="ItemCommentDAL.cs" company="VIT">
// VIT @ 2012
// </copyright>
// -----------------------------------------------------------------------

namespace VIT.DataAccessLayer
{
    using System.Data.Entity;
    using System.Linq;

    using VIT.Entity;

    /// <summary>
    /// The custom ItemCommentDAL
    /// </summary>
    public partial class ItemCommentDAL
    {
        public IQueryable<ItemComment> GetAllIncludeItem()
        {
            var entity = this.GetAll()
                .Include(c => c.Item);

            return entity;
        }

        public IQueryable<ItemComment> GetAllIncludeItemView()
        {
            var entity = this.GetAll()
                .Include(c => c.Item)
                .Include(c => c.Item.ItemView);

            return entity;
        }
    }

    /// <summary>
    /// The IItemCommentDAL
    /// </summary>
    public partial interface IItemCommentDAL
    {
        /// <summary>
        /// Get all Comment join Item
        /// </summary>
        /// <returns>
        /// List ItemComment
        /// </returns>
        IQueryable<ItemComment> GetAllIncludeItem();

        /// <summary>
        /// Get all Comment join Item and ItemView
        /// </summary>
        /// <returns>
        /// List ItemComment
        /// </returns>
        IQueryable<ItemComment> GetAllIncludeItemView();
    }
}
