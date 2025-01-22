using System.ComponentModel;
using System.Net;
using System.Xml.Linq;

namespace WPF_finalhw
{
    class Record
    {

        private string name;
        private int age;
        private string address;
        private string status;
        private string email;

        private List<string> skills = new List<string>();

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;

            }
        }
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;

            }
        }

        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;

            }
        }
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;

            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;

            }
        }


        public List<string> Skills
        {
            get
            {
                return skills;
            }
            set
            {
                skills = value;

            }
        }

        public Record(string n, int ag, string ad, string s, string e, List<string> skills)
        {
            Name = n;
            Age = ag;
            Address = ad;
            Status = s;
            Email = e;
            Skills = new List<string>(skills);
        }

        public Record()
        {
            Name = "";
            Age = 0;
            Address = "";
            Status = "";
            Email = "";
            Skills = new List<string>();
        }
    }
}
