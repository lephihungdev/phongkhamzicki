namespace VIT.DataAccessLayer.HealthCare
{
	using VIT.DataAccessLayer.HealthCare.Data;
	using VIT.DataHelper.LinqHelper.Infrastructure;
	using VIT.Entity.HealthCare;

    public partial class InventoryDAL : RepositoryBase<HealthCareEntities, Inventory> , IInventoryDAL
    {
        public InventoryDAL(IDatabaseFactory<HealthCareEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IInventoryDAL : IRepositoryBase<Inventory>
    {
    }
    
}
