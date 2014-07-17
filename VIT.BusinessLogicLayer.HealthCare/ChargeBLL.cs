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
        private readonly IChargeInstrumentDAL _chargeInstrumentDAL;
        private readonly IICDDAL _icdDAL;
        private readonly IInstrumentDAL _instrumentDAL;
        private readonly IClinicalDAL _clinicalDAL;

        public ChargeBLL(string connectionString = "")
            : base(connectionString)
        {
            this._dal = new ChargeDAL(this.DatabaseFactory);
            this._chargeDrugDAL = new ChargeDrugDAL(this.DatabaseFactory);
            this._chargeInstrumentDAL = new ChargeInstrumentDAL(this.DatabaseFactory);
            this._icdDAL = new ICDDAL(this.DatabaseFactory);
            this._instrumentDAL = new InstrumentDAL(this.DatabaseFactory);
            this._clinicalDAL = new ClinicalDAL(this.DatabaseFactory);
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
                        Treatments = e.Treatments,
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

        public ChargeInfoDto GetInfo(int chargeId)
        {
            var query = this._dal.GetAll().Where(e => e.Id == chargeId);
            var charge = query.Select(e => new ChargeInfoDto
            {
                Id = e.Id,
                PatientId = e.PatientId,
                PatientName = e.Patient.FirstName + " " + e.Patient.LastName,
                DoctorName = e.Doctor.FirstName + " " + e.Doctor.LastName,
                Treatments = e.Treatments,
                Diagnostic = e.Diagnostic + "|" + (e.ICDCode1 ?? string.Empty) + "|" + (e.ICDCode2 ?? string.Empty) + "|" + (e.ICDCode3 ?? string.Empty) + "|" + (e.ICDCode4 ?? string.Empty),
                Note = e.Note,
                Days = e.Days,
                DateService = e.DateService
            })
            .FirstOrDefault();

            if (charge != null)
            {
                if (!string.IsNullOrEmpty(charge.Diagnostic))
                {
                    var icds = charge.Diagnostic.Split('|');
                    charge.Diagnostic = icds[0];
                    for (var i = 1; i < 5; i++)
                    {
                        var code = icds[i];
                        if (!string.IsNullOrEmpty(code))
                        {
                            charge.Diagnostic += string.IsNullOrEmpty(charge.Diagnostic) ? string.Empty : ", ";
                            charge.Diagnostic +=
                                this._icdDAL.GetAll().Where(e => e.Code == code).Select(e => e.Description).FirstOrDefault();
                        }
                    }
                }
            }

            return charge;
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
        public void RemoveChargeDrug(int id, int patientId, int chargeId)
        {
            var query = this._chargeDrugDAL.GetAll().Where(e => e.Id == id && e.PatientId == patientId);

            if (chargeId > 0) query = query.Where(e => e.ChargeId == chargeId);

            var entity = query.FirstOrDefault();

            if (entity == null) throw new Exception("Đối tượng không tồn tại");

            this._chargeDrugDAL.Delete(entity);

            this.SaveChanges();
        }
        public void AddChargeDrug(ChargeDrugDto dto)
        {
            var entity = new ChargeDrug
            {
                PatientId = dto.PatientId,
                DrugId = dto.DrugId,
                ChargeId = dto.ChargeId,
                Quality = dto.Quality,
                Note = dto.Note
            };

            if (entity.ChargeId == 0) entity.ChargeId = null;

            this._chargeDrugDAL.Add(entity);

            this.SaveChanges();
            dto.Id = entity.Id;
        }

        public IQueryable<ChargeInstrumentDto> GetInstruments(int patientId, int chargeId = 0)
        {
            var query = this._chargeInstrumentDAL.GetAll().Where(e => e.PatientId == patientId);
            if (chargeId == 0) query = query.Where(e => e.ChargeId == null);
            else query = query.Where(e => e.ChargeId == chargeId);
            var drugs = query.Select(e => new ChargeInstrumentDto
            {
                Id = e.Id,
                InstrumentId = e.InstrumentsId,
                InstrumentName = e.Instrument.Name,
                Quality = e.Quality,
                Note = e.Note
            });

            return drugs;
        }
        public void RemoveChargeInstrument(int id, int patientId, int chargeId)
        {
            var query = this._chargeInstrumentDAL.GetAll().Where(e => e.Id == id && e.PatientId == patientId);

            if (chargeId > 0) query = query.Where(e => e.ChargeId == chargeId);

            var entity = query.FirstOrDefault();

            if (entity == null) throw new Exception("Đối tượng không tồn tại");

            this._chargeInstrumentDAL.Delete(entity);

            this.SaveChanges();
        }
        public void AddChargeInstrument(ChargeInstrumentDto dto)
        {
            var entity = new ChargeInstrument
            {
                PatientId = dto.PatientId,
                InstrumentsId = dto.InstrumentId,
                ChargeId = dto.ChargeId,
                Quality = dto.Quality,
                Note = dto.Note
            };

            if (entity.ChargeId == 0) entity.ChargeId = null;

            this._chargeInstrumentDAL.Add(entity);

            this.SaveChanges();
            dto.Id = entity.Id;
        }       

        public void Update(ChargeDto dto, int patientId, int facilityId, int userId)
        {
            var entity = this._dal.GetAll().FirstOrDefault(e => e.Id == dto.Id && e.PatientId == patientId && e.FacilityId == facilityId);
            
            if(entity == null) throw new Exception("Đối tượng không tồn tại");

            entity.DoctorId = dto.DoctorId;
            entity.DateOnset = dto.DateOnset;
            entity.DateService = dto.DateService;
            entity.Treatments = dto.Treatments;
            entity.Diagnostic = dto.Diagnostic;
            entity.ICDCode1 = dto.ICDCode1;
            entity.ICDCode2 = dto.ICDCode2;
            entity.ICDCode3 = dto.ICDCode3;
            entity.ICDCode4 = dto.ICDCode4;
            entity.Note = dto.Note;
            entity.Days = dto.Days;
            entity.UserId = userId;

            this._dal.Update(entity);

            this.SaveChanges();
        }

        public void Insert(ChargeDto dto, int patientId, int facilityId, int userId)
        {
            var entity = new Charge
                {
                    PatientId = patientId,
                    FacilityId = facilityId,
                    DoctorId = dto.DoctorId,
                    DateOnset = dto.DateOnset,
                    DateService = dto.DateService,
                    Treatments = dto.Treatments,
                    Diagnostic = dto.Diagnostic,
                    ICDCode1 = dto.ICDCode1,
                    ICDCode2 = dto.ICDCode2,
                    ICDCode3 = dto.ICDCode3,
                    ICDCode4 = dto.ICDCode4,
                    Note = dto.Note,
                    Days = dto.Days,
                    UserId = userId
                };

            this._dal.Add(entity);

            this.SaveChanges();
            dto.Id = entity.Id;

            var drugs = this._chargeDrugDAL.GetAll().Where(e => !e.ChargeId.HasValue && e.PatientId == patientId).ToList();
            foreach (var chargeDrug in drugs)
            {
                chargeDrug.ChargeId = dto.Id;
                this._chargeDrugDAL.Update(chargeDrug);
            }

            var instruments = this._chargeInstrumentDAL.GetAll().Where(e => !e.ChargeId.HasValue && e.PatientId == patientId).ToList();
            foreach (var chargeInstrument in instruments)
            {
                chargeInstrument.ChargeId = dto.Id;
                this._chargeInstrumentDAL.Update(chargeInstrument);
            }

            var clinical = this._clinicalDAL.GetAll().FirstOrDefault(e => e.PatientId == patientId && !e.ChargeId.HasValue);
            if (clinical != null)
            {
                clinical.ChargeId = entity.Id;
                this._clinicalDAL.Update(clinical);
            }
            
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
