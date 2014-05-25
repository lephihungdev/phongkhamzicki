namespace VIT.BusinessLogicLayer.HealthCare
{
    using System;
    using System.Linq;

    using VIT.DataAccessLayer.HealthCare;
    using VIT.DataTransferObject.HealthCare;
    using VIT.Entity.HealthCare;

    public class ChargeBLL : BLLBase
    {
        private readonly IChargeDAL _dal;
        private readonly IChargeDrugDAL _chargeDrugDAL;

        public ChargeBLL(string connectionString = "")
            : base(connectionString)
        {
            this._dal = new ChargeDAL(this.DatabaseFactory);
            this._chargeDrugDAL = new ChargeDrugDAL(this.DatabaseFactory);
        }

        public IQueryable<ChargeDto> Get(int patientId, int facilityId = 0)
        {
            var query = this._dal.GetAll().Where(e => e.PatientId == patientId);
            if(facilityId > 0) query = query.Where(e => e.FacilityId == facilityId);
            var charges = query.Select(e => new ChargeDto
                    {
                        Id = e.Id,
                        PatientName = e.Patient.FirstName + " " + e.Patient.LastName,
                        DoctorName = e.Doctor.FirstName + " " + e.Doctor.LastName,
                        CPTCode = e.CPTCode,
                        Diagnostic = e.Diagnostic,
                        ICDCode1 = e.ICDCode1,
                        ICDCode2 = e.ICDCode2,
                        ICDCode3 = e.ICDCode3,
                        ICDCode4 = e.ICDCode4,
                        Note = e.Note,
                        Days = e.Days,
                        DateOnset = e.DateOnset,
                        DateService = e.DateService,
                        DoctorId = e.DoctorId
                    });

            return charges;
        }

        public IQueryable<ChargeDrugDto> GetDrugs(int patientId, int chargeId = 0)
        {
            var query = this._chargeDrugDAL.GetAll().Where(e => e.PatientId == patientId);
            if (chargeId == 0) query = query.Where(e => e.ChargeId == null);
            else query = query.Where(e => e.ChargeId == chargeId);
            var drugs = query.Select(e => new ChargeDrugDto
            {
                Id = e.Id,
                DrugId = e.DrugId,
                DrugName = e.Drug.Name,
                Quality = e.Quality,
                Note = e.Note
            });

            return drugs;
        }

        public void Update(ChargeDto dto, int facilityId, int userId)
        {
            var entity = this._dal.GetAll().FirstOrDefault(e => e.Id == dto.Id && e.FacilityId == facilityId);
            
            if(entity == null) throw new Exception("Đối tượng không tồn tại");

            entity.DoctorId = dto.DoctorId;
            entity.DateOnset = dto.DateOnset;
            entity.DateService = dto.DateService;
            entity.CPTCode = dto.CPTCode;
            entity.Diagnostic = dto.Diagnostic;
            entity.ICDCode1 = dto.ICDCode1;
            entity.ICDCode2 = dto.ICDCode2;
            entity.ICDCode3 = dto.ICDCode3;
            entity.ICDCode4 = dto.ICDCode4;
            entity.Note = dto.Note;
            entity.Days = dto.Days;
            entity.UserId = userId;

            this.SaveChanges();
        }

        public void Insert(ChargeDto dto, int patientId, int facilityId, int userId)
        {
            var entity = new Charge();
            entity.PatientId = patientId;
            entity.FacilityId = facilityId;
            entity.DoctorId = dto.DoctorId;
            entity.DateOnset = dto.DateOnset;
            entity.DateService = dto.DateService;
            entity.CPTCode = dto.CPTCode;
            entity.Diagnostic = dto.Diagnostic;
            entity.ICDCode1 = dto.ICDCode1;
            entity.ICDCode2 = dto.ICDCode2;
            entity.ICDCode3 = dto.ICDCode3;
            entity.ICDCode4 = dto.ICDCode4;
            entity.Note = dto.Note;
            entity.Days = dto.Days;
            entity.UserId = userId;
            this._dal.Add(entity);

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
