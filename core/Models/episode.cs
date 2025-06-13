using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Models
{
    public class episode : BaseClass
    {

        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Title { get; set; }

        public int ProdcastId { get; set; }
        public Prodcast Prodcast { get; set; }
        public Record record { get; set; }
 

    }
}
