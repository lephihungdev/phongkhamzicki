namespace VIT.DataAccessLayer.HealthCare
{
	using VIT.DataAccessLayer.HealthCare.Data;
	using VIT.DataHelper.LinqHelper.Infrastructure;
	using VIT.Entity.HealthCare;

    public partial class ChargeInstrumentDAL : RepositoryBase<HealthCareEntities, ChargeInstrument> , IChargeInstrumentDAL
    {
        public ChargeInstrumentDAL(IDatabaseFactory<HealthCareEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IChargeInstrumentDAL : IRepositoryBase<ChargeInstrument>
    {
    }
    
}
