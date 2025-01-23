using System.ComponentModel;
using System.Net;
using System.Xml.Linq;

namespace WPF_finalhw
{
    class Skill
    {

        private string name;
     
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
    

        public Skill(string n)
        {
            Name = n;
        }

        public Skill()
        {
            Name = "";
        }

        public override string ToString()
        {
            return Name; 
        }

    }
}
