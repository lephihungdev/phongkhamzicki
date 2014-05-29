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
                        Code = e.Code,
                        Description = e.Description,
                        Fee = e.Fee,
                        Active = e.Active
                    });

            if (!string.IsNullOrEmpty(key)) query = query.Where(e => e.Code.StartsWith(key));

            return query;
        }

        public void Save(CptDto dto, int facilityId)
        {
            var entity = this._dal.GetAll().FirstOrDefault(e => e.Code == dto.Code && e.FacilityId == facilityId);

            if (entity == null)
            {
                entity = new CPT();
                entity.Code = dto.Code;
                entity.FacilityId = facilityId;
                this._dal.Add(entity);
            }
            else
            {
                this._dal.Update(entity);
            };

            entity.Description = dto.Description;
            entity.Fee = dto.Fee;
            entity.Active = dto.Active;

            this.SaveChanges();
        }

        public void Delete(string code, int facilityId)
        {
            var entity = this._dal.GetAll().FirstOrDefault(e => e.Code == code && e.FacilityId == facilityId);

            if (entity == null) throw new Exception("Đối tượng không tồn tại");

            this._dal.Delete(entity);

            this.SaveChanges();
        }
    }
}
