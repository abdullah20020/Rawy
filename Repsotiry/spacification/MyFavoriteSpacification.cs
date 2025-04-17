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
    public class MyFavoriteSpacification:BaseSpacfication<Favorite>
    {
       public MyFavoriteSpacification()
        {
            AddIncludes();
        }

        public MyFavoriteSpacification(int id) : base(f => f.Id == id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            includes.Add(f => f.Books);
            AddThenInclude(q => q.Include(f => f.Books).ThenInclude(b => b.Aurthor));
            AddThenInclude(q => q.Include(f => f.Books).ThenInclude(b => b.catygories));
        }

    }
}
