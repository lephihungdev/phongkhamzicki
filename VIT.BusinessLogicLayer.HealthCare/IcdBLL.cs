namespace VIT.BusinessLogicLayer.HealthCare
{
    using System;
    using System.Linq;

    using VIT.DataAccessLayer.HealthCare;
    using VIT.DataTransferObject.HealthCare;
    using VIT.Entity.HealthCare;

    public class IcdBLL : BLLBase
    {
        private readonly IICDDAL _dal;

        public IcdBLL(string connectionString = "")
            : base(connectionString)
        {
            this._dal = new ICDDAL(this.DatabaseFactory);
        }

        public IQueryable<CodeDto> Get()
        {
            var query = this._dal.GetAll()
                .Select(e => new CodeDto
                    {
                        Id = e.Id,
                        Code = e.Code,
                        Description = e.Description
                    });

            return query;
        }

        public void Update(CodeDto dto)
        {
            var entity = this._dal.GetAll().FirstOrDefault(e => e.Id == dto.Id);
            
            if(entity == null) throw new Exception("Đối tượng không tồn tại");

            entity.Description = dto.Description;

            this.SaveChanges();
        }

        public void Insert(CodeDto dto)
        {
            var exist = this._dal.GetAll().Any(e => e.Code == dto.Code);

            if (exist) throw new Exception("Đối tượng đã tồn tại");

            var entity = new ICD();
            entity.Code = dto.Code;
            entity.Description = dto.Description;
            this._dal.Add(entity);

            this.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = this._dal.GetAll().FirstOrDefault(e => e.Id == id);

            if (entity == null) throw new Exception("Đối tượng không tồn tại");

            this._dal.Delete(entity);

            this.SaveChanges();
        }
    }
}
