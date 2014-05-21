namespace VIT.DataAccessLayer.HealthCare
{
	using VIT.DataAccessLayer.HealthCare.Data;
	using VIT.DataHelper.LinqHelper.Infrastructure;
	using VIT.Entity.HealthCare;

    public partial class ChargeDAL : RepositoryBase<HealthCareEntities, Charge> , IChargeDAL
    {
        public ChargeDAL(IDatabaseFactory<HealthCareEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IChargeDAL : IRepositoryBase<Charge>
    {
    }
    
}
