namespace VIT.BusinessLogicLayer.HealthCare
{
    using System;
    using System.Collections.Generic;

    using VIT.DataAccessLayer.HealthCare.Data;
    using VIT.DataHelper.LinqHelper.Infrastructure;

    public abstract class BLLBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private string _connectionString;
        private string ConnectionString
        {
            get { return this._connectionString ?? System.Configuration.ConfigurationManager.ConnectionStrings["VITConnectionString"].ConnectionString; }
            set { this._connectionString = value; }
        }

        protected IDatabaseFactory<HealthCareEntities> DatabaseFactory { get; private set; }

        protected void DisposeCore()
        {
            this._unitOfWork.Dispose();
            //base.DisposeCore();
        }

        protected bool CatchError(Exception ex)
        {
            this.RollBack();
            throw ex;
        }

        protected BLLBase(IUnitOfWork unitOfWork)
        {
            this.DatabaseFactory = unitOfWork.DatabaseFactory as IDatabaseFactory<HealthCareEntities>;
            this._unitOfWork = unitOfWork;
        }

        protected BLLBase(string connectionString)
        {
            if (!string.IsNullOrEmpty(connectionString))
            {
                this.ConnectionString = connectionString;
            }

            this.DatabaseFactory = new DatabaseFactory<HealthCareEntities>(this.ConnectionString);
            this._unitOfWork = new UnitOfWork(this.DatabaseFactory);
        }

        protected BLLBase()
            : this((string)null)
        {
        }

        ~BLLBase()
        {
            this.DisposeCore();
        }

        public IUnitOfWork UnitOfWork
        {
            get { return this._unitOfWork; }
        }

        public void BeginWork()
        {
            this._unitOfWork.BeginTransaction();
        }

        public void SaveChanges()
        {
            this._unitOfWork.Commit();
        }

        public void RollBack()
        {
            this._unitOfWork.RollBackTransaction();
        }

        public enum Order
        {
            /// <summary>
            /// Order Up
            /// </summary>
            Up,

            /// <summary>
            /// Order Down
            /// </summary>
            Down
        }

        #region Public method
        public IDictionary<int, string> GetSexs()
        {
            var sexs = new Dictionary<int, string>();
            sexs[0] = "Nữ";
            sexs[1] = "Nam";

            return sexs;
        }
        #endregion
    }
}
