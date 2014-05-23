namespace VIT.BusinessLogicLayer.HealthCare
{
    using System;
    using System.Linq;

    using VIT.DataAccessLayer.HealthCare;
    using VIT.DataTransferObject.HealthCare;
    using VIT.Entity.HealthCare;

    public class DrugBLL : BLLBase
    {
        private readonly IDrugDAL _dal;

        public DrugBLL(string connectionString = "")
            : base(connectionString)
        {
            this._dal = new DrugDAL(this.DatabaseFactory);
        }

        public IQueryable<DrugDto> Get(int facilityId, string key = "")
        {
            var query = this._dal.GetAll()
                .Where(e => e.FacilityId == facilityId)
                .Select(e => new DrugDto
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Description = e.Description,
                        Fee = e.Fee,
                        Active = e.Active
                    });

            if (!string.IsNullOrEmpty(key)) query = query.Where(e => e.Name.StartsWith(key));

            return query;
        }

        public void Update(DrugDto dto, int facilityId)
        {
            var entity = this._dal.GetAll().FirstOrDefault(e => e.Id == dto.Id && e.FacilityId == facilityId);
            
            if(entity == null) throw new Exception("Đối tượng không tồn tại");

            entity.Description = dto.Description;
            entity.Fee = dto.Fee;
            entity.Active = dto.Active;

            this.SaveChanges();
        }

        public void Insert(DrugDto dto, int facilityId)
        {
            var exist = this._dal.GetAll().Any(e => e.Name == dto.Name);

            if (exist) throw new Exception("Đối tượng đã tồn tại");

            var entity = new Drug();
            entity.FacilityId = facilityId;
            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.Fee = dto.Fee;
            entity.Active = dto.Active;

            this._dal.Add(entity);

            this.SaveChanges();
            dto.Id = entity.Id;
        }

        public void Delete(int id, int facilityId)
        {
            var entity = this._dal.GetAll().FirstOrDefault(e => e.Id == id && e.FacilityId == facilityId);

            if (entity == null) throw new Exception("Đối tượng không tồn tại");

            this._dal.Delete(entity);

            this.SaveChanges();
        }
    }
}
