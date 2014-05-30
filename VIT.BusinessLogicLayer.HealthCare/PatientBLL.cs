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
        /// bệnh nhân mới tạo hoặc đă từng khám chữa bệnh ở đây
        /// </returns>
        public IQueryable<PatientDto> Gets(int facilityId, DateTime fromDate, DateTime toDate)
        {
            toDate = toDate.AddDays(1);
            var query = this._dal.GetAll()
                .Where(e => e.FacilityId == facilityId || e.Charges.Any(c => c.FacilityId == facilityId))
                .Where(e => e.Charges.Any(c => fromDate <= c.DateService && c.DateService <= toDate))
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

        /// <returns>
        /// get thông tin bệnh nhân
        /// </returns>
        public PatientDto GetById(int patientId)
        {
            var query = this._dal.GetAll()
                .Where(e => e.Id == patientId)
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

            return query.FirstOrDefault();
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
        public IQueryable<PatientDto> Search(string key, bool? hasCharge, bool? allFacility, int facilityId = 0)
        {
            var query = this._dal.GetAll();

            if (hasCharge == true) query = query.Where(e => e.Charges.Count > 0);
            else if (hasCharge == false) query = query.Where(e => e.Charges.Count == 0);

            if (allFacility == false) query = query.Where(e => e.FacilityId == facilityId || e.Charges.Any(c => c.FacilityId == facilityId));

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
            entity.Email = dto.Email;
            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;
            entity.Phone = dto.Phone;
            entity.Sex = dto.Sex;

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
                    IaBon = e.IaBon,
                    IaLong = e.IaLong,
                    IaRaMau = e.IaRaMau,
                    TieuIt = e.TieuIt,
                    TieuGat = e.TieuGat,
                    TieuCoMu = e.TieuCoMu,
                    TieuRaMau = e.TieuRaMau,
                    TieuNuocVang = e.TieuNuocVang,
                    TieuNuocDuc = e.TieuNuocDuc,
                    TieuDemMayLan = e.TieuDemMayLan,
                    AnMet = e.AnMet,
                    AnChanAn = e.AnChanAn,
                    AnDau = e.AnDau,
                    AnBinhHoi = e.AnBinhHoi,
                    AnKhoTieu = e.AnKhoTieu,
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

            if (chargeId == null) query = query.Where(e => !e.ChargeId.HasValue);
            else query = query.Where(e => e.ChargeId == chargeId);

            var dto = query.FirstOrDefault();
            if (dto != null) return dto;

            // thong tin sau cung
            // dto = query.OrderByDescending(e => e.Id).FirstOrDefault();
            if (dto == null)
            {
                var patientName = this._dal.GetAll().Where(e => e.Id == patientId).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault();
                dto = new ClinicalDto { PatientId = patientId, PatientName = patientName };
            }
            else
            {
                dto.Id = 0;
                dto.ChargeId = null;
            }

            return dto;
        }

        public void SaveClinical(ClinicalDto dto)
        {
            var query = this._clinicalDAL.GetAll().Where(e => e.PatientId == dto.PatientId);

            if (dto.ChargeId == null) query = query.Where(e => !e.ChargeId.HasValue);
            else query = query.Where(e => e.ChargeId == dto.ChargeId && e.Id == dto.Id);

            var entity = query.FirstOrDefault();

            if (entity == null)
            {
                entity = new Clinical();
                entity.PatientId = dto.PatientId;
                entity.ChargeId = dto.ChargeId;
                this._clinicalDAL.Add(entity);
            }
            else this._clinicalDAL.Update(entity);
            
            entity.CanNang = dto.CanNang;
            entity.ChieuCao = dto.ChieuCao;
            entity.HuyetAp = dto.HuyetAp;
            entity.NhipTim = dto.NhipTim;
            entity.NhipTho = dto.NhipTho;
            entity.TimMet = dto.TimMet;
            entity.TimNangNguc = dto.TimNangNguc;
            entity.TimKhoTho = dto.TimKhoTho;
            entity.PhoiHo = dto.PhoiHo;
            entity.BaoTuDayHoi = dto.BaoTuDayHoi;
            entity.DaNgua = dto.DaNgua;
            entity.DaLupusDo = dto.DaLupusDo;
            entity.DaVayNen = dto.DaVayNen;
            entity.DaNamLangBeng = dto.DaNamLangBeng;
            entity.DaCham = dto.DaCham;
            entity.DaToDia = dto.DaToDia;
            entity.DaBachBien = dto.DaBachBien;
            entity.DaZona = dto.DaZona;
            entity.DauSayXam = dto.DauSayXam;
            entity.DauDau = dto.DauDau;
            entity.DauChayMatSong = dto.DauChayMatSong;
            entity.DauTocRung = dto.DauTocRung;
            entity.LungDauBaVai = dto.LungDauBaVai;
            entity.LungCungCoGay = dto.LungCungCoGay;
            entity.LungLoiSauLung = dto.LungLoiSauLung;
            entity.LungDauLung = dto.LungDauLung;
            entity.NgucBungTucNguc = dto.NgucBungTucNguc;
            entity.NgucBungDauLienSuon = dto.NgucBungDauLienSuon;
            entity.NgucBungDauBung = dto.NgucBungDauBung;
            entity.NgucBungDauQuanhRon = dto.NgucBungDauQuanhRon;
            entity.NgucBungLoiHong = dto.NgucBungLoiHong;
            entity.TayChanTeMoi = dto.TayChanTeMoi;
            entity.TayChanDauBapVe = dto.TayChanDauBapVe;
            entity.TayChanPhu = dto.TayChanPhu;
            entity.TayChanRaMoHoi = dto.TayChanRaMoHoi;
            entity.TayChanThonGot = dto.TayChanThonGot;
            entity.IaBon = dto.IaBon;
            entity.IaLong = dto.IaLong;
            entity.IaRaMau = dto.IaRaMau;
            entity.TieuIt = dto.TieuIt;
            entity.TieuGat = dto.TieuGat;
            entity.TieuCoMu = dto.TieuCoMu;
            entity.TieuRaMau = dto.TieuRaMau;
            entity.TieuNuocVang = dto.TieuNuocVang;
            entity.TieuNuocDuc = dto.TieuNuocDuc;
            entity.TieuDemMayLan = dto.TieuDemMayLan;
            entity.AnMet = dto.AnMet;
            entity.AnChanAn = dto.AnChanAn;
            entity.AnDau = dto.AnDau;
            entity.AnBinhHoi = dto.AnBinhHoi;
            entity.AnKhoTieu = dto.AnKhoTieu;
            entity.NguKho = dto.NguKho;
            entity.NguNangNguc = dto.NguNangNguc;
            entity.NguRutChan = dto.NguRutChan;
            entity.NguChuotRut = dto.NguChuotRut;
            entity.UTai = dto.UTai;
            entity.HayQuen = dto.HayQuen;
            entity.MunNhot = dto.MunNhot;
            entity.Nam = dto.Nam;
            entity.TanNhang = dto.TanNhang;
            entity.DangMangThai = dto.DangMangThai;
            entity.MoiSinh = dto.MoiSinh;
            entity.KinhNguyetTre = dto.KinhNguyetTre;
            entity.KinhNguyetGianDoan = dto.KinhNguyetGianDoan;
            entity.KinhNguyetSom = dto.KinhNguyetSom;
            entity.KinhNguyetRongKinh = dto.KinhNguyetRongKinh;
            entity.KinhNguyetManKinh = dto.KinhNguyetManKinh;
            entity.HuyetTrang = dto.HuyetTrang;

            this.SaveChanges();
        }
    }
}
