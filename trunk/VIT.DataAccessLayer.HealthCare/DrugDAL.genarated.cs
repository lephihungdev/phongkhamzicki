namespace VIT.DataAccessLayer.HealthCare
{
	using VIT.DataAccessLayer.HealthCare.Data;
	using VIT.DataHelper.LinqHelper.Infrastructure;
	using VIT.Entity.HealthCare;

    public partial class DrugDAL : RepositoryBase<HealthCareEntities, Drug> , IDrugDAL
    {
        public DrugDAL(IDatabaseFactory<HealthCareEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IDrugDAL : IRepositoryBase<Drug>
    {
    }
    
}
