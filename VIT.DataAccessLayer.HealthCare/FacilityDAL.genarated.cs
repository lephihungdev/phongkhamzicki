namespace VIT.DataAccessLayer.HealthCare
{
	using VIT.DataAccessLayer.HealthCare.Data;
	using VIT.DataHelper.LinqHelper.Infrastructure;
	using VIT.Entity.HealthCare;

    public partial class FacilityDAL : RepositoryBase<HealthCareEntities, Facility> , IFacilityDAL
    {
        public FacilityDAL(IDatabaseFactory<HealthCareEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IFacilityDAL : IRepositoryBase<Facility>
    {
    }
    
}
