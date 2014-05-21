namespace VIT.DataAccessLayer.HealthCare
{
	using VIT.DataAccessLayer.HealthCare.Data;
	using VIT.DataHelper.LinqHelper.Infrastructure;
	using VIT.Entity.HealthCare;

    public partial class ICDDAL : RepositoryBase<HealthCareEntities, ICD> , IICDDAL
    {
        public ICDDAL(IDatabaseFactory<HealthCareEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IICDDAL : IRepositoryBase<ICD>
    {
    }
    
}
