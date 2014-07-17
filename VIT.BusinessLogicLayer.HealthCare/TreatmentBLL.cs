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
                    Active = e.Active
                });

            if (!string.IsNullOrEmpty(key)) query = query.Where(e => e.Name.StartsWith(key));

            return query;
        }

        public void Save(CptDto dto, int facilityId)
        {
            var entity = this._dal.GetAll().FirstOrDefault(e => e.Id == dto.Id && e.FacilityId == facilityId);

            if (entity == null)
            {
                entity = new Instrument();
                entity.FacilityId = facilityId;
                this._dal.Add(entity);
            }
            else
            {
                this._dal.Update(entity);
            };

            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.Fee = dto.Fee;
            entity.Active = dto.Active;

            this.SaveChanges();
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
