namespace VIT.BusinessLogicLayer.HealthCare
{
    using System.Linq;

    using VIT.DataAccessLayer.HealthCare;
    using VIT.DataTransferObject.HealthCare;
    using VIT.Entity.HealthCare;

    public class FacilityBLL : BLLBase
    {
        private readonly IFacilityDAL _facilityDAL;
        private readonly IUserDAL _userDAL;

        public FacilityBLL(string connectionString = "")
            : base(connectionString)
        {
            this._facilityDAL = new FacilityDAL(this.DatabaseFactory);
            this._userDAL = new UserDAL(this.DatabaseFactory);
        }

        public IQueryable<AutoCompletedIntDto> GetAll(int patientId)
        {
            var facilities = this._facilityDAL.GetAll()
                .Where(e => e.Charges.Any(c => c.PatientId == patientId))
                .Select(e => new AutoCompletedIntDto
                    {
                        label = e.Name,
                        value = e.Id
                    });
            return facilities;
        }

        public string GetFacilityName(int id)
        {
            var facility = this._facilityDAL.GetAll().FirstOrDefault(e => e.Id == id);
            return facility != null ? facility.Name : string.Empty;
        }

        public int Regist(string facilityName, string userName, string fullName, string passWord)
        {
            var facility = new Facility();
            facility.Name = facilityName;
            this._facilityDAL.Add(facility);

            var user = new User();
            user.FacilityId = facility.Id;
            user.UserName = userName;
            user.Password = passWord;
            user.FullName = fullName;
            this._userDAL.Add(user);

            this.SaveChanges();

            return facility.Id;
        }

        public void Update(int facilityId, string facilityName)
        {
            var facility = this._facilityDAL.GetAll().FirstOrDefault(e => e.Id == facilityId);
            if (facility != null)
            {
                facility.Name = facilityName;

                this.SaveChanges();
            }
        }
    }
}
