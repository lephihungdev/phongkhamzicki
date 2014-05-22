namespace VIT.BusinessLogicLayer.HealthCare
{
    using System;
    using System.Linq;

    using VIT.DataAccessLayer.HealthCare;
    using VIT.DataTransferObject.HealthCare;
    using VIT.Entity.HealthCare;

    public class CptBLL : BLLBase
    {
        private readonly ICPTDAL _dal;

        public CptBLL(string connectionString = "")
            : base(connectionString)
        {
            this._dal = new CPTDAL(this.DatabaseFactory);
        }

        public IQueryable<CptDto> Get(int facilityId, string key = "")
        {
            var query = this._dal.GetAll()
                .Where(e => e.FacilityId == facilityId)
                .Select(e => new CptDto
                    {
                        Id = e.Id,
                        Code = e.Code,
                        Description = e.Description,
                        Fee = e.Fee
                    });

            if (!string.IsNullOrEmpty(key)) query = query.Where(e => e.Code.StartsWith(key));

            return query;
        }

        public void Update(CptDto dto, int facilityId)
        {
            var entity = this._dal.GetAll().FirstOrDefault(e => e.Id == dto.Id && e.FacilityId == facilityId);
            
            if(entity == null) throw new Exception("Đối tượng không tồn tại");

            entity.Description = dto.Description;
            entity.Fee = dto.Fee;

            this.SaveChanges();
        }

        public void Insert(CptDto dto, int facilityId)
        {
            var exist = this._dal.GetAll().Any(e => e.Code == dto.Code);

            if (exist) throw new Exception("Đối tượng đã tồn tại");

            var entity = new CPT();
            entity.FacilityId = facilityId;
            entity.Code = dto.Code;
            entity.Description = dto.Description;
            entity.Fee = dto.Fee;
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
