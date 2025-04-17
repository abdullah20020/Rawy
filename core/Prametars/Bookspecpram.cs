using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Prametars
{
    public class Bookspecpram
    {
        private const int Maxmum = 10;
        private int pageSize = 5;
        public int PageIndex { get; set; } = 1;
        public int pagesize
        { 
        get => pageSize;
        set { pageSize = value > Maxmum ? Maxmum : value; }
        }

        private string? searchname { get; set; }

        public string? Search
        {
            get => searchname; set
            {

                searchname = value?.ToLower();
            }
        }
        public string? sort { get; set; }
        public int? authorId { get; set; }

    }
}
