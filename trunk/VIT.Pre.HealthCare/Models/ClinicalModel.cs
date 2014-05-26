﻿namespace VIT.Pre.HealthCare.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VIT.DataTransferObject.HealthCare;

    public class ClinicalModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int? ChargeId { get; set; }
        public string CanNang { get; set; }
        public string ChieuCao { get; set; }
        public string HuyetAp { get; set; }
        public string NhipTim { get; set; }
        public string NhipTho { get; set; }
        public bool TimMet { get; set; }
        public bool TimNangNguc { get; set; }
        public bool TimKhoTho { get; set; }
        public bool PhoiHo { get; set; }
        public bool BaoTuDayHoi { get; set; }
        public bool DaNgua { get; set; }
        public bool DaLupusDo { get; set; }
        public bool DaVayNen { get; set; }
        public bool DaNamLangBeng { get; set; }
        public bool DaCham { get; set; }
        public bool DaToDia { get; set; }
        public bool DaBachBien { get; set; }
        public bool DaZona { get; set; }
        public bool DauSayXam { get; set; }
        public bool DauDau { get; set; }
        public bool DauChayMatSong { get; set; }
        public bool DauTocRung { get; set; }
        public bool LungDauBaVai { get; set; }
        public bool LungCungCoGay { get; set; }
        public bool LungLoiSauLung { get; set; }
        public bool LungDauLung { get; set; }
        public bool NgucBungTucNguc { get; set; }
        public bool NgucBungDauLienSuon { get; set; }
        public bool NgucBungDauBung { get; set; }
        public bool NgucBungDauQuanhRon { get; set; }
        public bool NgucBungLoiHong { get; set; }
        public bool TayChanTeMoi { get; set; }
        public bool TayChanDauBapVe { get; set; }
        public bool TayChanPhu { get; set; }
        public bool TayChanRaMoHoi { get; set; }
        public bool TayChanThonGot { get; set; }
        public bool IaBinhThuong { get; set; }
        public bool IaBon { get; set; }
        public bool IaLong { get; set; }
        public bool IaRaMau { get; set; }
        public bool TieuIt { get; set; }
        public bool TieuGat { get; set; }
        public bool TieuCoMu { get; set; }
        public bool TieuRaMau { get; set; }
        public bool TieuNuocTrong { get; set; }
        public bool TieuNuocVang { get; set; }
        public bool TieuNuocDuc { get; set; }
        public int TieuDemMayLan { get; set; }
        public bool AnMet { get; set; }
        public bool AnChanAn { get; set; }
        public bool AnDau { get; set; }
        public bool AnKhoTho { get; set; }
        public bool AnBinhHoi { get; set; }
        public bool AnKhoTieu { get; set; }
        public bool NguDuoc { get; set; }
        public bool NguKho { get; set; }
        public bool NguNangNguc { get; set; }
        public bool NguRutChan { get; set; }
        public bool NguChuotRut { get; set; }
        public bool UTai { get; set; }
        public bool HayQuen { get; set; }
        public bool MunNhot { get; set; }
        public bool Nam { get; set; }
        public bool TanNhang { get; set; }
        public bool DangMangThai { get; set; }
        public int? MoiSinh { get; set; }
        public bool KinhNguyetTre { get; set; }
        public bool KinhNguyetGianDoan { get; set; }
        public bool KinhNguyetSom { get; set; }
        public bool KinhNguyetRongKinh { get; set; }
        public bool KinhNguyetManKinh { get; set; }
        public string HuyetTrang { get; set; }

        public string PatientName { get; set; }
    }
}
