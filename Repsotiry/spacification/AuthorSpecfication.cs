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
    public class AuthorSpecfication:BaseSpacfication<Aurthor>
    {
        public AuthorSpecfication()
        {
            includes.Add(f => f.Books);
        }

        public AuthorSpecfication(int id) : base(f => f.Id == id)
        {
            includes.Add(f => f.Books);
        }

  
    }
}
