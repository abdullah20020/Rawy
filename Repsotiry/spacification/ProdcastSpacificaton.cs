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
    public class ProdcastSpacificaton : BaseSpacfication<Prodcast>
    {
        public ProdcastSpacificaton()
        {
            AddIncludes();
        }

        public ProdcastSpacificaton(int id) : base(f => f.Id == id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            includes.Add(PL => PL.reviews);
            includes.Add(PL => PL.episode);
       

        }
    }
}
