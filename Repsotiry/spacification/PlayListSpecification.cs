using core.Models;
using core.Spacification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repsotiry.spacification
{
    public class PlayListSpecification: BaseSpacfication<Playlist>
    {
        public PlayListSpecification()
        {
            AddIncludes();
        }

        public PlayListSpecification(int id) : base(f => f.Id == id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            includes.Add(PL => PL.records);
            AddThenInclude(q => q.Include(PL=>PL.records).ThenInclude(b => b.Book));
       
        }
    }
}
