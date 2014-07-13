namespace VIT.DataAccessLayer.HealthCare
{
	using VIT.DataAccessLayer.HealthCare.Data;
	using VIT.DataHelper.LinqHelper.Infrastructure;
	using VIT.Entity.HealthCare;

    public partial class InstrumentDAL : RepositoryBase<HealthCareEntities, Instrument> , IInstrumentDAL
    {
        public InstrumentDAL(IDatabaseFactory<HealthCareEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IInstrumentDAL : IRepositoryBase<Instrument>
    {
    }
    
}
