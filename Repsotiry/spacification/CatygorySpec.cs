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
    public class CatygorySpec:BaseSpacfication<Catygory>
    {
        public CatygorySpec()
        {
        }

        public CatygorySpec(int id) : base(f => f.Id == id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            includes.Add(f => f.books);
            AddThenInclude(q => q.Include(f => f.books).ThenInclude(b => b.Aurthor));
            AddThenInclude(q => q.Include(f => f.books).ThenInclude(b => b.catygories));
        }
    }
}
