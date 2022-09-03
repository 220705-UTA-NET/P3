using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Model
{
    public class SupportDTO
    {
        //message ID
        public int id { get; set; }

        //first name
        public string first_name { get; set; }

        //last name
        public string last_name { get; set; }

        //email
        public string email { get; set; }

        //username
        public string username { get; set; }

        //phone 
        public string phone { get; set; }

        //password
        public string password { get; set; }

        public SupportDTO(int id, string first_name, string last_name, string email, string username, string phone, string password)
        {
            this.id = id;
            this.first_name = first_name;
            this.last_name = last_name;
            this.email = email;
            this.username = username;
            this.phone = phone;
            this.password = password;
        }
    }
}
