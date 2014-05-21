namespace VIT.BusinessLogicLayer.HealthCare
{
    using System;
    using System.Linq;

    using VIT.DataAccessLayer.HealthCare;
    using VIT.DataTransferObject.HealthCare;
    using VIT.Entity.HealthCare;

    public class DoctorBLL : BLLBase
    {
        private readonly IDoctorDAL _dal;

        public DoctorBLL(string connectionString = "")
            : base(connectionString)
        {
            this._dal = new DoctorDAL(this.DatabaseFactory);
        }

        public IQueryable<DoctorDto> Get(int facilityId, string key = "")
        {
            var query = this._dal.GetAll()
                .Where(e => e.FacilityId == facilityId)
                .Select(e => new DoctorDto
                    {
                        Id = e.Id,
                        Address = e.Address,
                        Birthday = e.Birthday,
                        Email = e.Email,
                        FirstName = e.FirstName,
                        LastName = e.FirstName,
                        Phone = e.Phone,
                        Sex = e.Sex
                    });

            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(e => e.FirstName.StartsWith(key) || e.LastName.StartsWith(key) || e.Address.StartsWith(key) || e.Email.StartsWith(key));
            }

            return query;
        }

        public void Update(DoctorDto dto, int facilityId)
        {
            var entity = this._dal.GetAll().FirstOrDefault(e => e.Id == dto.Id && e.Charges.Count == 0 || e.Charges.Any(c => c.FacilityId == facilityId));
            
            if(entity == null) throw new Exception("Đối tượng không tồn tại");

            entity.Address = dto.Address;
            entity.Birthday = dto.Birthday;
            entity.Email = entity.Email;
            entity.FirstName = entity.FirstName;
            entity.LastName = entity.LastName;
            entity.Phone = entity.Phone;
            entity.Sex = entity.Sex;
            

            this.SaveChanges();
        }

        public void Insert(DoctorDto dto, int facilityId)
        {
            var entity = new Doctor();
            entity.Address = dto.Address;
            entity.Birthday = dto.Birthday;
            entity.Email = dto.Email;
            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;
            entity.Phone = dto.Phone;
            entity.Sex = dto.Sex;
            entity.FacilityId = facilityId;
            this._dal.Add(entity);

            this.SaveChanges();
        }

        public void Delete(int id, int facilityId)
        {
            var entity = this._dal.GetAll().FirstOrDefault(e => e.Id == id && e.Charges.Count == 0 || (e.Charges.Any(c => c.FacilityId == facilityId) && !e.Charges.Any(c => c.FacilityId != facilityId)));

            if (entity == null) throw new Exception("Đối tượng không tồn tại");

            this._dal.Delete(entity);

            this.SaveChanges();
        }
    }
}
