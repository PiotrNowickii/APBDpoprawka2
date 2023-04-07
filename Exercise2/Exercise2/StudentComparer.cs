using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise2
{
    internal class StudentComparer : IEqualityComparer<Student>
    {
        public bool Equals(Student? x, Student? y)
        {
            if(x == null || y == null) return false;

            return (x.indexNumber == y.indexNumber)
                && (x.fname == y.fname)
                && (x.lname == y.lname)
                && (x.birthdate == y.birthdate)
                && (x.email == y.email)
                && (x.mothersName == y.mothersName)
                && (x.fathersName == y.fathersName);
        }

        public int GetHashCode([DisallowNull] Student obj)
        {
            return obj.indexNumber.GetHashCode();
        }
    }
}
