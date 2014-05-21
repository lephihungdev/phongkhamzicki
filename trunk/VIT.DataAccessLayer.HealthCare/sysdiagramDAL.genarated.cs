namespace VIT.DataAccessLayer.HealthCare
{
	using VIT.DataAccessLayer.HealthCare.Data;
	using VIT.DataHelper.LinqHelper.Infrastructure;
	using VIT.Entity.HealthCare;

    public partial class sysdiagramDAL : RepositoryBase<HealthCareEntities, sysdiagram> , IsysdiagramDAL
    {
        public sysdiagramDAL(IDatabaseFactory<HealthCareEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IsysdiagramDAL : IRepositoryBase<sysdiagram>
    {
    }
    
}
