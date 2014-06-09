namespace VIT.Pre.HealthCare.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using VIT.BusinessLogicLayer.HealthCare;
    using VIT.DataTransferObject.HealthCare;
    using VIT.Library;
    using VIT.Library.Web.ObjectData;
    using VIT.Pre.HealthCare.Models;

    public class PatientController : Controller
    {
        private readonly PatientBLL _patientBLL;
        private readonly FacilityBLL _facilityBLL;

        public PatientController()
        {
            this._patientBLL = new PatientBLL();
            this._facilityBLL = new FacilityBLL();
        }

        public ActionResult Search(string key, int patientId = 0, bool allfacility = false, bool? hasCharge = false)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;
            
            var model = new PatientModel();
            if (patientId > 0)
            {
                var dto = this._patientBLL.GetById(patientId);
                model.Address = dto.Address;
                model.BirthYear = dto.BirthYear;
                model.Email = dto.Email;
                model.FirstName = dto.FirstName;
                model.LastName = dto.LastName;
                model.Phone = dto.Phone;
                model.Id = dto.Id;
                model.Sex = dto.Sex;
            }

            //cheat code
            if (!string.IsNullOrEmpty(key))
            {
                allfacility = true;
                hasCharge = null;
            }
            else hasCharge = allfacility = false;

            var patients = this._patientBLL.Search(key, hasCharge, allfacility, user.CompanyId).OrderBy(e => e.LastName).ToList();
            model.Patients = patients;
            model.Sexs = this._patientBLL.GetSexs();

            return this.View(model);
        }

        public ActionResult Delete(int id)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");

            try
            {
                this._patientBLL.Delete(id, user.CompanyId);
            }
            catch (Exception exception)
            {
                ViewBag.ErrorLabel = exception.Message;
            }

            return this.RedirectToAction("Search", "Patient");
        }

        [HttpPost]
        public ActionResult Search(PatientModel model)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var dto = new PatientDto
            {
                Address = model.Address,
                BirthYear = model.BirthYear,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone,
                Id = model.Id,
                Sex = model.Sex
            };

            try
            {
                if (model.Id > 0) this._patientBLL.Update(dto, user.CompanyId);
                else
                {
                    this._patientBLL.Insert(dto, user.CompanyId);
                    model.Id = dto.Id;
                }
            }
            catch (Exception exception)
            {
                ViewBag.ErrorLabel = exception.Message;
            }

            var patients = this._patientBLL.Search(string.Empty, false, false, user.CompanyId).OrderBy(e => e.LastName).ToList();
            
            model.Patients = patients;
            model.Sexs = this._patientBLL.GetSexs();

            return this.View(model);
        }

        public ActionResult Clinical(int patientId)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var dto = this._patientBLL.GetClinical(patientId, null);

            var model = new ClinicalModel
            {
                Id = dto.Id,
                PatientId = dto.PatientId,
                ChargeId = dto.ChargeId,
                CanNang = dto.CanNang,
                ChieuCao = dto.ChieuCao,
                HuyetAp = dto.HuyetAp,
                NhipTim = dto.NhipTim,
                NhipTho = dto.NhipTho,
                TimMet = dto.TimMet,
                TimNangNguc = dto.TimNangNguc,
                TimKhoTho = dto.TimKhoTho,
                PhoiHo = dto.PhoiHo,
                BaoTuDayHoi = dto.BaoTuDayHoi,
                DaNgua = dto.DaNgua,
                DaLupusDo = dto.DaLupusDo,
                DaVayNen = dto.DaVayNen,
                DaNamLangBeng = dto.DaNamLangBeng,
                DaCham = dto.DaCham,
                DaToDia = dto.DaToDia,
                DaBachBien = dto.DaBachBien,
                DaZona = dto.DaZona,
                DauSayXam = dto.DauSayXam,
                DauDau = dto.DauDau,
                DauChayMatSong = dto.DauChayMatSong,
                DauTocRung = dto.DauTocRung,
                LungDauBaVai = dto.LungDauBaVai,
                LungCungCoGay = dto.LungCungCoGay,
                LungLoiSauLung = dto.LungLoiSauLung,
                LungDauLung = dto.LungDauLung,
                NgucBungTucNguc = dto.NgucBungTucNguc,
                NgucBungDauLienSuon = dto.NgucBungDauLienSuon,
                NgucBungDauBung = dto.NgucBungDauBung,
                NgucBungDauQuanhRon = dto.NgucBungDauQuanhRon,
                NgucBungLoiHong = dto.NgucBungLoiHong,
                TayChanTeMoi = dto.TayChanTeMoi,
                TayChanDauBapVe = dto.TayChanDauBapVe,
                TayChanPhu = dto.TayChanPhu,
                TayChanRaMoHoi = dto.TayChanRaMoHoi,
                TayChanThonGot = dto.TayChanThonGot,
                IaBon = dto.IaBon,
                IaLong = dto.IaLong,
                IaRaMau = dto.IaRaMau,
                TieuIt = dto.TieuIt,
                TieuGat = dto.TieuGat,
                TieuCoMu = dto.TieuCoMu,
                TieuRaMau = dto.TieuRaMau,
                TieuNuocVang = dto.TieuNuocVang,
                TieuNuocDuc = dto.TieuNuocDuc,
                TieuDemMayLan = dto.TieuDemMayLan,
                AnMet = dto.AnMet,
                AnChanAn = dto.AnChanAn,
                AnDau = dto.AnDau,
                AnBinhHoi = dto.AnBinhHoi,
                AnKhoTieu = dto.AnKhoTieu,
                NguKho = dto.NguKho,
                NguNangNguc = dto.NguNangNguc,
                NguRutChan = dto.NguRutChan,
                NguChuotRut = dto.NguChuotRut,
                UTai = dto.UTai,
                HayQuen = dto.HayQuen,
                MunNhot = dto.MunNhot,
                Nam = dto.Nam,
                TanNhang = dto.TanNhang,
                DangMangThai = dto.DangMangThai,
                MoiSinh = dto.MoiSinh,
                KinhNguyetTre = dto.KinhNguyetTre,
                KinhNguyetGianDoan = dto.KinhNguyetGianDoan,
                KinhNguyetSom = dto.KinhNguyetSom,
                KinhNguyetRongKinh = dto.KinhNguyetRongKinh,
                KinhNguyetManKinh = dto.KinhNguyetManKinh,
                HuyetTrang = dto.HuyetTrang,

                PatientName = dto.PatientName
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Clinical(ClinicalModel model)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var dto = new ClinicalDto
            {
                Id = model.Id,
                PatientId = model.PatientId,
                ChargeId = model.ChargeId,
                CanNang = model.CanNang,
                ChieuCao = model.ChieuCao,
                HuyetAp = model.HuyetAp,
                NhipTim = model.NhipTim,
                NhipTho = model.NhipTho,
                TimMet = model.TimMet,
                TimNangNguc = model.TimNangNguc,
                TimKhoTho = model.TimKhoTho,
                PhoiHo = model.PhoiHo,
                BaoTuDayHoi = model.BaoTuDayHoi,
                DaNgua = model.DaNgua,
                DaLupusDo = model.DaLupusDo,
                DaVayNen = model.DaVayNen,
                DaNamLangBeng = model.DaNamLangBeng,
                DaCham = model.DaCham,
                DaToDia = model.DaToDia,
                DaBachBien = model.DaBachBien,
                DaZona = model.DaZona,
                DauSayXam = model.DauSayXam,
                DauDau = model.DauDau,
                DauChayMatSong = model.DauChayMatSong,
                DauTocRung = model.DauTocRung,
                LungDauBaVai = model.LungDauBaVai,
                LungCungCoGay = model.LungCungCoGay,
                LungLoiSauLung = model.LungLoiSauLung,
                LungDauLung = model.LungDauLung,
                NgucBungTucNguc = model.NgucBungTucNguc,
                NgucBungDauLienSuon = model.NgucBungDauLienSuon,
                NgucBungDauBung = model.NgucBungDauBung,
                NgucBungDauQuanhRon = model.NgucBungDauQuanhRon,
                NgucBungLoiHong = model.NgucBungLoiHong,
                TayChanTeMoi = model.TayChanTeMoi,
                TayChanDauBapVe = model.TayChanDauBapVe,
                TayChanPhu = model.TayChanPhu,
                TayChanRaMoHoi = model.TayChanRaMoHoi,
                TayChanThonGot = model.TayChanThonGot,
                IaBon = model.IaBon,
                IaLong = model.IaLong,
                IaRaMau = model.IaRaMau,
                TieuIt = model.TieuIt,
                TieuGat = model.TieuGat,
                TieuCoMu = model.TieuCoMu,
                TieuRaMau = model.TieuRaMau,
                TieuNuocVang = model.TieuNuocVang,
                TieuNuocDuc = model.TieuNuocDuc,
                TieuDemMayLan = model.TieuDemMayLan,
                AnMet = model.AnMet,
                AnChanAn = model.AnChanAn,
                AnDau = model.AnDau,
                AnBinhHoi = model.AnBinhHoi,
                AnKhoTieu = model.AnKhoTieu,
                NguKho = model.NguKho,
                NguNangNguc = model.NguNangNguc,
                NguRutChan = model.NguRutChan,
                NguChuotRut = model.NguChuotRut,
                UTai = model.UTai,
                HayQuen = model.HayQuen,
                MunNhot = model.MunNhot,
                Nam = model.Nam,
                TanNhang = model.TanNhang,
                DangMangThai = model.DangMangThai,
                MoiSinh = model.MoiSinh,
                KinhNguyetTre = model.KinhNguyetTre,
                KinhNguyetGianDoan = model.KinhNguyetGianDoan,
                KinhNguyetSom = model.KinhNguyetSom,
                KinhNguyetRongKinh = model.KinhNguyetRongKinh,
                KinhNguyetManKinh = model.KinhNguyetManKinh,
                HuyetTrang = model.HuyetTrang,
            };

            this._patientBLL.SaveClinical(dto);

            return View(model);
        }

        public ActionResult Report()
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            var model = new PatientReportModel();
            model.FromDate = DateTime.Now;
            model.ToDate = DateTime.Now;
            model.Patients = new List<PatientDto>();

            return this.View(model);
        }

        [HttpPost]
        public ActionResult Report(PatientReportModel model)
        {
            var user = this.Session[SettingsManager.Constants.SessionUser] as UserData;
            if (user == null) return this.RedirectToAction("Login", "Login");
            this.ViewBag.FacilityName = this._facilityBLL.GetFacilityName(user.CompanyId);
            this.ViewBag.UserName = user.UserName;

            model.Patients = this._patientBLL.Gets(user.CompanyId, model.FromDate, model.ToDate).OrderBy(e => e.LastName).ToList(); ;
            return this.View(model);
        }
    }
}
