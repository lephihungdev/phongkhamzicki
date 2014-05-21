namespace VIT.DataAccessLayer.HealthCare
{
	using VIT.DataAccessLayer.HealthCare.Data;
	using VIT.DataHelper.LinqHelper.Infrastructure;
	using VIT.Entity.HealthCare;

    public partial class UserDAL : RepositoryBase<HealthCareEntities, User> , IUserDAL
    {
        public UserDAL(IDatabaseFactory<HealthCareEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IUserDAL : IRepositoryBase<User>
    {
    }
    
}
