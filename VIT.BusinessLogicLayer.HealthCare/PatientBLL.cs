namespace VIT.BusinessLogicLayer.HealthCare
{
    using System;
    using System.Linq;

    using VIT.DataAccessLayer.HealthCare;
    using VIT.DataTransferObject.HealthCare;
    using VIT.Entity.HealthCare;

    public class PatientBLL : BLLBase
    {
        private readonly IPatientDAL _dal;
        private readonly IChargeDAL _chargeDAL;

        public PatientBLL(string connectionString = "")
            : base(connectionString)
        {
            this._dal = new PatientDAL(this.DatabaseFactory);
            this._chargeDAL = new ChargeDAL(this.DatabaseFactory);
        }

        /// <returns>
        /// bệnh nhân mới tạo hoặc đă từn khám chữa bệnh ở đây
        /// </returns>
        public IQueryable<PatientDto> Get(int facilityId)
        {
            var query = this._dal.GetAll()
                .Where(e => e.Charges.Count == 0 || e.Charges.Any(c => c.FacilityId == facilityId))
                .Select(e => new PatientDto
                    {
                        Id = e.Id,
                        Address = e.Address,
                        BirthYear = e.BirthYear,
                        Email = e.Email,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        Phone = e.Phone,
                        Sex = e.Sex
                    });

            return query;
        }

        /// <summary>
        /// Các phòng khám có thể chia sẻ thông tin bệnh nhân
        /// </summary>
        /// <param name="key">
        /// Giá trị cần tìm, nếu nhập số thì tìm theo Id
        /// </param>
        /// <param name="facilityId">
        /// nếu facilityId > 0 thì tìm bệnh nhân của 1 phòng khám xác định
        /// </param>
        /// <returns>
        /// danh sách bệnh nhân
        /// </returns>
        public IQueryable<PatientDto> Search(string key, bool hasCharge, int facilityId = 0)
        {
            var query = this._dal.GetAll();
            if (hasCharge)
            {
                if (facilityId > 0) query = query.Where(e => e.Charges.Any(c => c.FacilityId == facilityId));
                else query = query.Where(e => e.Charges.Any());
            }
            else
            {
                if (facilityId > 0) query = query.Where(e => e.Charges.Count == 0);
            }

            var patients = query.Select(e => new PatientDto
                {
                    Id = e.Id,
                    Address = e.Address,
                    BirthYear = e.BirthYear,
                    Email = e.Email,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Phone = e.Phone,
                    Sex = e.Sex
                });

            if (!string.IsNullOrEmpty(key))
            {
                int value;
                if (int.TryParse(key, out value))
                {
                    patients = patients.Where(e => e.Id == value);
                }
                else
                {
                    patients = patients.Where(e => e.FirstName.StartsWith(key) || e.LastName.StartsWith(key) || e.Address.StartsWith(key) || e.Email.StartsWith(key));
                }
            }

            return patients;
        } 

        public void Update(PatientDto dto, int facilityId)
        {
            var entity = this._dal.GetAll().FirstOrDefault(e => e.Id == dto.Id && (e.Charges.Count == 0 || (e.Charges.Any(c => c.FacilityId == facilityId) && !e.Charges.Any(c => c.FacilityId != facilityId))));
            if(entity == null) throw new Exception("Đối tượng không tồn tại");         

            entity.Address = dto.Address;
            entity.BirthYear = dto.BirthYear;
            entity.Email = entity.Email;
            entity.FirstName = entity.FirstName;
            entity.LastName = entity.LastName;
            entity.Phone = entity.Phone;
            entity.Sex = entity.Sex;

            this.SaveChanges();
        }

        public void Insert(PatientDto dto, int facilityId)
        {
            var entity = new Patient();
            entity.Address = dto.Address;
            entity.BirthYear = dto.BirthYear;
            entity.Email = dto.Email;
            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;
            entity.Phone = dto.Phone;
            entity.Sex = dto.Sex;
            this._dal.Add(entity);

            this.SaveChanges();
            dto.Id = entity.Id;
        }

        public void Delete(int id, int facilityId)
        {
            var entity = this._dal.GetAll().FirstOrDefault(e => e.Id == id);
            if (entity == null) throw new Exception("Đối tượng không tồn tại");

            var charges = this._chargeDAL.GetAll().Where(e => e.PatientId == id);
            if (charges.Any(c => c.FacilityId != facilityId)) throw new Exception("Bệnh nhân này đã khám và chữa bệnh ở nhiều nơi, không thể xóa");
            if (charges.Any(c => c.FacilityId == facilityId)) throw new Exception("Phải xóa hết bệnh án");    

            this._dal.Delete(entity);

            this.SaveChanges();
        }
    }
}
