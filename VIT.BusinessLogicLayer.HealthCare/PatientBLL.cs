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
        private readonly IClinicalDAL _clinicalDAL;

        public PatientBLL(string connectionString = "")
            : base(connectionString)
        {
            this._dal = new PatientDAL(this.DatabaseFactory);
            this._chargeDAL = new ChargeDAL(this.DatabaseFactory);
            this._clinicalDAL = new ClinicalDAL(this.DatabaseFactory);
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
        public IQueryable<PatientDto> Search(string key, bool? hasCharge, int facilityId = 0)
        {
            var query = this._dal.GetAll();

            if (hasCharge == true) query = query.Where(e => e.Charges.Count > 0);
            else if (hasCharge == false) query = query.Where(e => e.Charges.Count == 0);

            if (facilityId > 0) query = query.Where(e => e.FacilityId == facilityId || e.Charges.Any(c => c.FacilityId == facilityId));

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
            entity.FacilityId = facilityId;
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

        public ClinicalDto GetClinical(int patientId, int? chargeId)
        {
            var query = this._clinicalDAL.GetAll()
                .Where(e => e.PatientId == patientId)
                .Select(e => new ClinicalDto
                {
                    Id = e.Id,
                    PatientId = e.PatientId,
                    ChargeId = e.ChargeId,
                    CanNang = e.CanNang,
                    ChieuCao = e.ChieuCao,
                    HuyetAp = e.HuyetAp,
                    NhipTim = e.NhipTim,
                    NhipTho = e.NhipTho,
                    TimMet = e.TimMet,
                    TimNangNguc = e.TimNangNguc,
                    TimKhoTho = e.TimKhoTho,
                    PhoiHo = e.PhoiHo,
                    BaoTuDayHoi = e.BaoTuDayHoi,
                    DaNgua = e.DaNgua,
                    DaLupusDo = e.DaLupusDo,
                    DaVayNen = e.DaVayNen,
                    DaNamLangBeng = e.DaNamLangBeng,
                    DaCham = e.DaCham,
                    DaToDia = e.DaToDia,
                    DaBachBien = e.DaBachBien,
                    DaZona = e.DaZona,
                    DauSayXam = e.DauSayXam,
                    DauDau = e.DauDau,
                    DauChayMatSong = e.DauChayMatSong,
                    DauTocRung = e.DauTocRung,
                    LungDauBaVai = e.LungDauBaVai,
                    LungCungCoGay = e.LungCungCoGay,
                    LungLoiSauLung = e.LungLoiSauLung,
                    LungDauLung = e.LungDauLung,
                    NgucBungTucNguc = e.NgucBungTucNguc,
                    NgucBungDauLienSuon = e.NgucBungDauLienSuon,
                    NgucBungDauBung = e.NgucBungDauBung,
                    NgucBungDauQuanhRon = e.NgucBungDauQuanhRon,
                    NgucBungLoiHong = e.NgucBungLoiHong,
                    TayChanTeMoi = e.TayChanTeMoi,
                    TayChanDauBapVe = e.TayChanDauBapVe,
                    TayChanPhu = e.TayChanPhu,
                    TayChanRaMoHoi = e.TayChanRaMoHoi,
                    TayChanThonGot = e.TayChanThonGot,
                    IaBinhThuong = e.IaBinhThuong,
                    IaBon = e.IaBon,
                    IaLong = e.IaLong,
                    IaRaMau = e.IaRaMau,
                    TieuIt = e.TieuIt,
                    TieuGat = e.TieuGat,
                    TieuCoMu = e.TieuCoMu,
                    TieuRaMau = e.TieuRaMau,
                    TieuNuocTrong = e.TieuNuocTrong,
                    TieuNuocVang = e.TieuNuocVang,
                    TieuNuocDuc = e.TieuNuocDuc,
                    TieuDemMayLan = e.TieuDemMayLan,
                    AnMet = e.AnMet,
                    AnChanAn = e.AnChanAn,
                    AnDau = e.AnDau,
                    AnKhoTho = e.AnKhoTho,
                    AnBinhHoi = e.AnBinhHoi,
                    AnKhoTieu = e.AnKhoTieu,
                    NguDuoc = e.NguDuoc,
                    NguKho = e.NguKho,
                    NguNangNguc = e.NguNangNguc,
                    NguRutChan = e.NguRutChan,
                    NguChuotRut = e.NguChuotRut,
                    UTai = e.UTai,
                    HayQuen = e.HayQuen,
                    MunNhot = e.MunNhot,
                    Nam = e.Nam,
                    TanNhang = e.TanNhang,
                    DangMangThai = e.DangMangThai,
                    MoiSinh = e.MoiSinh,
                    KinhNguyetTre = e.KinhNguyetTre,
                    KinhNguyetGianDoan = e.KinhNguyetGianDoan,
                    KinhNguyetSom = e.KinhNguyetSom,
                    KinhNguyetRongKinh = e.KinhNguyetRongKinh,
                    KinhNguyetManKinh = e.KinhNguyetManKinh,
                    HuyetTrang = e.HuyetTrang,

                    PatientName = e.Patient.FirstName + " " + e.Patient.LastName
                });

            var dto = query.FirstOrDefault(e => e.ChargeId == chargeId);
            if (dto != null) return dto;

            dto = query.OrderByDescending(e => e.Id).FirstOrDefault();
            if (dto == null)
            {
                var patientName = this._dal.GetAll().Where(e => e.Id == patientId).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault();
                dto = new ClinicalDto { PatientId = patientId, PatientName = patientName };
            }

            return dto;
        }
    }
}
