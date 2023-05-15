using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welcome.Others;

namespace DataLayer.Model
{
    public class DatabaseGrade
    {   public int Id { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public int GradeValue { get; set; }
        public SubjectEnum Subject { get; set; }

        // Define relationships with other entities
        public DatabaseUser Student { get; set; }
    }

}
