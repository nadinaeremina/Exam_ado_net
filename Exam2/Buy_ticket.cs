using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace Exam2
{
    [Table(Name = "Buy_ticket")]
    public class Buy_ticket
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column]
        public decimal Value { get; set; }
        [Column]
        public DateTime date_of_bought { get; set; }
        [Column]
        public int client_Id { get; set; }
        [Column]
        public int event_name_ID { get; set; }
    }
}
