using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linqjointables.MODEL
{
    public class Recmodel
    {
        public int RecordID { get; set; }
        public int StudentID { get; set; }  // Foreign key
        public int Tamil { get; set; }
        public int English { get; set; }
        public int Maths { get; set; }
        public int Science { get; set; }
        public int Social { get; set; }

        public int TotalMarks { get; set; } // Add TotalMarks

    }
}
