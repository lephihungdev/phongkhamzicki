namespace VIT.BusinessLogicLayer.HealthCare
{
    using System;
    using System.Linq;

    using VIT.DataAccessLayer.HealthCare;
    using VIT.DataTransferObject.HealthCare;
    using VIT.Entity.HealthCare;

    public class TreatmentBLL : BLLBase
    {
        private readonly IInstrumentDAL _dal;

        private const string inventoryTypeInstrument = "Instrument";

        public TreatmentBLL(string connectionString = "")
            : base(connectionString)
        {
            this._dal = new InstrumentDAL(this.DatabaseFactory);
        }

        public IQueryable<CptDto> Get(int facilityId, string key = "")
        {
            var query = this._dal.GetAll()
                .Where(e => e.FacilityId == facilityId)
                .Select(e => new CptDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    Fee = e.Fee,
                    Active = e.Active,
                    Stock = e.Stock
                });

            if (!string.IsNullOrEmpty(key)) query = query.Where(e => e.Name.Contains(key));

            return query;
        }

        public void Update(CptDto dto, int facilityId)
        {
            var entity = this._dal.GetAll().FirstOrDefault(e => e.Id == dto.Id && e.FacilityId == facilityId);

            if (entity == null) throw new Exception("Đối tượng không tồn tại");

            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.Fee = dto.Fee;
            entity.Active = dto.Active;
            entity.Stock = dto.Stock;

            this.SaveChanges();
        }

        public void Insert(CptDto dto, int facilityId)
        {
            var exist = this._dal.GetAll().Any(e => e.Name == dto.Name);

            if (exist) throw new Exception("Đối tượng đã tồn tại");

            var entity = new Instrument();
            entity.FacilityId = facilityId;
            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.Fee = dto.Fee;
            entity.Active = dto.Active;
            entity.Stock = dto.Stock;

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
