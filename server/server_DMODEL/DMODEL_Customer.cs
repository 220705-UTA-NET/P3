namespace Server_DataModels
{
    public class DMODEL_Customer
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string password { get; set; }
        public DMODEL_Customer(int id, string firstname, string lastname, string email, string phonenumber, string password)
        {
            this.id = id;
            this.firstName = firstname;
            this.lastName = lastname;
            this.email = email;
            this.phoneNumber = phonenumber;
            this.password = password;

        }
    }
}
