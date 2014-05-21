namespace VIT.DataAccessLayer.HealthCare
{
	using VIT.DataAccessLayer.HealthCare.Data;
	using VIT.DataHelper.LinqHelper.Infrastructure;
	using VIT.Entity.HealthCare;

    public partial class CPTDAL : RepositoryBase<HealthCareEntities, CPT> , ICPTDAL
    {
        public CPTDAL(IDatabaseFactory<HealthCareEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ICPTDAL : IRepositoryBase<CPT>
    {
    }
    
}
