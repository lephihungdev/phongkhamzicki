namespace VIT.DataAccessLayer.HealthCare
{
	using VIT.DataAccessLayer.HealthCare.Data;
	using VIT.DataHelper.LinqHelper.Infrastructure;
	using VIT.Entity.HealthCare;

    public partial class ChargeDrugDAL : RepositoryBase<HealthCareEntities, ChargeDrug> , IChargeDrugDAL
    {
        public ChargeDrugDAL(IDatabaseFactory<HealthCareEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IChargeDrugDAL : IRepositoryBase<ChargeDrug>
    {
    }
    
}
