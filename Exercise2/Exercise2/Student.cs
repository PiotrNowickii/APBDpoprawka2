using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise2
{
    internal class Student
    {
        /*private int _id;
        public int getID() { return _id; }
        public void setID(int id) { _id = id; }*/
        public string indexNumber { get; set; } = null!;
        public string fname { get; set; } = null!;
        public string lname { get; set; } = null!;
        public string birthdate { get; set; } = null!;
        public string email { get; set; } = null!;
        public string mothersName { get; set; } = null!;
        public string fathersName { get; set; } = null!;
        public Studies studies { get; set; } = null!;
    }
}
