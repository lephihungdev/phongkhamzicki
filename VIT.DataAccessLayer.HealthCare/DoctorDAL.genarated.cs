namespace VIT.DataAccessLayer.HealthCare
{
	using VIT.DataAccessLayer.HealthCare.Data;
	using VIT.DataHelper.LinqHelper.Infrastructure;
	using VIT.Entity.HealthCare;

    public partial class DoctorDAL : RepositoryBase<HealthCareEntities, Doctor> , IDoctorDAL
    {
        public DoctorDAL(IDatabaseFactory<HealthCareEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IDoctorDAL : IRepositoryBase<Doctor>
    {
    }
    
}
