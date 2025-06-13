using core.Models;
using core.Spacification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repsotiry.spacification
{
    public class RecordSpec : BaseSpacfication<Record>
    {
        public RecordSpec()
        {
            AddIncludes();
        }

        public RecordSpec(int id) : base(f => f.Id == id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            includes.Add(PL => PL.User);
     


        }
    }
}
