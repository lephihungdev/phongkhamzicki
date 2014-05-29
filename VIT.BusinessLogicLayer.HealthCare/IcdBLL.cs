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
                        Code = e.Code,
                        Description = e.Description,
                        Active = e.Active
                    });

            if (!string.IsNullOrEmpty(key)) query = query.Where(e => e.Code.StartsWith(key));

            return query;
        }

        public void Save(IcdDto dto)
        {
            var entity = this._dal.GetAll().FirstOrDefault(e => e.Code == dto.Code);

            if (entity == null)
            {
                entity = new ICD();
                entity.Code = dto.Code;
                this._dal.Add(entity);
            }
            else
            {
                this._dal.Update(entity);
            }
            
            entity.Description = dto.Description;
            entity.Active = dto.Active;
            
            this.SaveChanges();
        }

        public void Delete(string code)
        {
            var entity = this._dal.GetAll().FirstOrDefault(e => e.Code == code);

            if (entity == null) throw new Exception("Đối tượng không tồn tại");

            this._dal.Delete(entity);

            this.SaveChanges();
        }
    }
}
