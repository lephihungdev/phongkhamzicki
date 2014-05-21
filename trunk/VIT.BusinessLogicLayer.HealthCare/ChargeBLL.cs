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

        public ChargeBLL(string connectionString = "")
            : base(connectionString)
        {
            this._dal = new ChargeDAL(this.DatabaseFactory);
        }

        public IQueryable<ChargeDto> Get(int patientId, int facilityId = 0)
        {
            var query = this._dal.GetAll().Where(e => e.PatientId == patientId);
            if(facilityId > 0) query = query.Where(e => e.FacilityId == facilityId);
            var charges = query.Select(e => new ChargeDto
                    {
                        Id = e.Id,
                        CPTCode = e.CPTCode,
                        Diagnostic = e.Diagnostic,
                        Drugs = e.Drugs,
                        ICDCode1 = e.ICDCode1,
                        ICDCode2 = e.ICDCode2,
                        ICDCode3 = e.ICDCode3,
                        ICDCode4 = e.ICDCode4,
                        Note = e.Note,
                        Quality = e.Quality
                    });

            return charges;
        }

        public void Update(ChargeDto dto, int facilityId, int userId)
        {
            var entity = this._dal.GetAll().FirstOrDefault(e => e.Id == dto.Id && e.FacilityId == facilityId);
            
            if(entity == null) throw new Exception("Đối tượng không tồn tại");

            entity.CPTCode = dto.CPTCode;
            entity.Diagnostic = dto.Diagnostic;
            entity.Drugs = dto.Drugs;
            entity.ICDCode1 = dto.ICDCode1;
            entity.ICDCode2 = dto.ICDCode2;
            entity.ICDCode3 = dto.ICDCode3;
            entity.ICDCode4 = dto.ICDCode4;
            entity.Note = dto.Note;
            entity.Quality = dto.Quality;
            entity.UserId = userId;

            this.SaveChanges();
        }

        public void Insert(ChargeDto dto, int patientId, int facilityId, int userId)
        {
            var entity = new Charge();
            entity.PatientId = patientId;
            entity.FacilityId = facilityId;
            entity.CPTCode = dto.CPTCode;
            entity.Diagnostic = dto.Diagnostic;
            entity.Drugs = dto.Drugs;
            entity.ICDCode1 = dto.ICDCode1;
            entity.ICDCode2 = dto.ICDCode2;
            entity.ICDCode3 = dto.ICDCode3;
            entity.ICDCode4 = dto.ICDCode4;
            entity.Note = dto.Note;
            entity.Quality = dto.Quality;
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
