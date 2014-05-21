// -----------------------------------------------------------------------
// <copyright file="FileLanguageDAL.cs" company="VIT">
// VIT @ 2012
// </copyright>
// -----------------------------------------------------------------------

namespace VIT.DataAccessLayer
{
    using System.Data.Entity;
    using System.Linq;

    using VIT.Entity;

    /// <summary>
    /// The custom FileLanguageDAL
    /// </summary>
    public partial class FileLanguageDAL
    {
        public IQueryable<FileLanguage> GetAllIncludeItem()
        {
            var FileLanguage = this.GetAll()
                .Include(c => c.File)
                .Include(c => c.File.Item);

            return FileLanguage;
        }

        public IQueryable<FileLanguage> GetAllIncludeItemView()
        {
            var FileLanguage = this.GetAll()
                .Include(c => c.File)
                .Include(c => c.File.Item)
                .Include(c => c.File.Item.ItemView);

            return FileLanguage;
        }

        public IQueryable<FileLanguage> GetAllIncludeFile()
        {
            var FileLanguage = this.GetAll()
                .Include(c => c.File);

            return FileLanguage;
        }

        public IQueryable<FileLanguage> GetAllIncludeCategory()
        {
            var FileLanguage = this.GetAll()
                .Include(c => c.File)
                .Include(c => c.File.Category)
                .Include(c => c.File.Category.CategoryLanguages);

            return FileLanguage;
        }

        public IQueryable<FileLanguage> GetAllIncludeItemCategory()
        {
            var FileLanguage = this.GetAll()
                .Include(c => c.File)
                .Include(c => c.File.Item)
                .Include(c => c.File.Category)
                .Include(c => c.File.Category.CategoryLanguages);

            return FileLanguage;
        }

        public IQueryable<FileLanguage> GetAllIncludeViewCategory()
        {
            var FileLanguage = this.GetAll()
                .Include(c => c.File)
                .Include(c => c.File.Item)
                .Include(c => c.File.Item.ItemView)
                .Include(c => c.File.Category)
                .Include(c => c.File.Category.CategoryLanguages);

            return FileLanguage;
        }

        public IQueryable<FileLanguage> GetAllDocumentIncludeViewCategory()
        {
            var FileLanguage = this.GetAll()
                .Include(c => c.File)
                .Include(c => c.File.Item)
                .Include(c => c.File.Item.ItemView)
                .Include(c => c.File.FileDocument)
                .Include(c => c.File.Category)
                .Include(c => c.File.Category.CategoryLanguages);

            return FileLanguage;
        }

        public IQueryable<FileLanguage> GetAllIncludes()
        {
            var FileLanguage = this.GetAll()
                .Include(c => c.File)
                .Include(c => c.File.Item)
                .Include(c => c.File.Item.ItemView)
                .Include(c => c.File.Item.ItemComment)
                .Include(c => c.File.Category)
                .Include(c => c.File.Category.CategoryLanguages);

            return FileLanguage;
        }
    }

    /// <summary>
    /// The IFileLanguageDAL
    /// </summary>
    public partial interface IFileLanguageDAL
    {
        /// <summary>
        /// Get all FileLanguage join Item
        /// </summary>
        /// <returns>
        /// List FileLanguage
        /// </returns>
        IQueryable<FileLanguage> GetAllIncludeItem();

        /// <summary>
        /// Get all FileLanguage join Item and ItemView
        /// </summary>
        /// <returns>
        /// List FileLanguage
        /// </returns>
        IQueryable<FileLanguage> GetAllIncludeItemView();

        /// <summary>
        /// Get all FileLanguage join File
        /// </summary>
        /// <returns>
        /// List FileLanguage
        /// </returns>
        IQueryable<FileLanguage> GetAllIncludeFile();

        /// <summary>
        /// Get all FileLanguage join Category
        /// </summary>
        /// <returns>
        /// List FileLanguage
        /// </returns>
        IQueryable<FileLanguage> GetAllIncludeCategory();

        /// <summary>
        /// Get all FileLanguage join Category and Item
        /// </summary>
        /// <returns>
        /// List FileLanguage
        /// </returns>
        IQueryable<FileLanguage> GetAllIncludeViewCategory();

        /// <summary>
        /// Get all FileLanguage join Category and Item
        /// </summary>
        /// <returns>
        /// List FileLanguage
        /// </returns>
        IQueryable<FileLanguage> GetAllDocumentIncludeViewCategory();

        /// <summary>
        /// Get all FileLanguage join Category and Item
        /// </summary>
        /// <returns>
        /// List FileLanguage
        /// </returns>
        IQueryable<FileLanguage> GetAllIncludeItemCategory();

        /// <summary>
        /// Get all FileLanguage join Category and Item and Comment
        /// </summary>
        /// <returns>
        /// List FileLanguage
        /// </returns>
        IQueryable<FileLanguage> GetAllIncludes();
    }
}
