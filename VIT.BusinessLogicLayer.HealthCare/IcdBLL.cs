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

        public IQueryable<IcdDto> Get(string key = "")
        {
            var query = this._dal.GetAll().OrderBy(e => e.Code)
                .Select(e => new IcdDto
                    {
                        Id = e.Id,
                        Code = e.Code,
                        Description = e.Description,
                        Active = e.Active
                    });

            if (!string.IsNullOrEmpty(key)) query = query.Where(e => e.Code.StartsWith(key));

            return query;
        }

        public void Update(IcdDto dto)
        {
            var entity = this._dal.GetAll().FirstOrDefault(e => e.Id == dto.Id);
            
            if(entity == null) throw new Exception("Đối tượng không tồn tại");

            entity.Description = dto.Description;
            entity.Active = dto.Active;

            this.SaveChanges();
        }

        public void Insert(IcdDto dto)
        {
            var exist = this._dal.GetAll().Any(e => e.Code == dto.Code);

            if (exist) throw new Exception("Đối tượng đã tồn tại");

            var entity = new ICD();
            entity.Code = dto.Code;
            entity.Description = dto.Description;
            entity.Active = dto.Active;
            this._dal.Add(entity);
            
            this.SaveChanges();
            dto.Id = entity.Id;
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
