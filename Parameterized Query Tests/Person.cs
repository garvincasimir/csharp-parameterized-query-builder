using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parameterized_Query_Tests
{
    public class Person
    {
        [Key]
        public Guid PersonID { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Children { get; set; }
    }
}
