using core.Models;
using core.Prametars;
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
        public AuthorSpecfication(Bookspecpram specpram) : base(b => 
        string.IsNullOrEmpty(specpram.Search) || b.Name.ToLower().Contains(specpram.Search))
        {
            includes.Add(f => f.Books);

            // for pegition 
            if (!string.IsNullOrEmpty(specpram.sort))
            {
                switch (specpram.sort)
                {
                    case "name":
                        Orderby(b => b.Name);
                        break;
                 
                    default:
                        Orderby(b => b.Books.Count);
                        break;

                }

            }
        }

        public AuthorSpecfication(int id) : base(f => f.Id == id)
        {
            includes.Add(f => f.Books);
        }

  
    }
}
