using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise2
{
    internal class Uczelnia
    {
        public string createdAt { get; set; } = null!;
        public string author { get; set; } = null!;
        public IEnumerable<Student> students { get; set; } = null!;
        public List<ActiveStudies> activeStudies { get; set;} = null!;
    }
}
