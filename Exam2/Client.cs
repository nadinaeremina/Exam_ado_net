using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace Exam2
{
    [Table(Name = "Clients")]
    public class Client
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column]
        public string First_name { get; set; }
        [Column]
        public string Last_name { get; set; }
        [Column]
        public string Middle_name { get; set; }
        [Column]
        public string Email { get; set; }
        [Column]
        public DateTime Birthday { get; set; }
    }
}
