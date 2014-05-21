namespace VIT.DataAccessLayer.HealthCare
{
	using VIT.DataAccessLayer.HealthCare.Data;
	using VIT.DataHelper.LinqHelper.Infrastructure;
	using VIT.Entity.HealthCare;

    public partial class PatientDAL : RepositoryBase<HealthCareEntities, Patient> , IPatientDAL
    {
        public PatientDAL(IDatabaseFactory<HealthCareEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IPatientDAL : IRepositoryBase<Patient>
    {
    }
    
}
