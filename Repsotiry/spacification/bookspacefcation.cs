using core.Models;
using core.Prametars;
using core.Spacification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repsotiry.spacification
{
    public class bookspacefcation:BaseSpacfication<Book>
    {
        public bookspacefcation(Bookspecpram specpram) : base(b=>!specpram.authorId.HasValue||b.AurthorId== specpram.authorId
        && string.IsNullOrEmpty(specpram.Search) || b.BookTitle.ToLower().Contains(specpram.Search) && b.record.Any(r => r.Okay_Record == true))
        {
            includes.Add(b => b.Aurthor);
            includes.Add(b => b.reviews);
            includes.Add(b => b.catygories);
            includes.Add(b => b.record);
 
            // for pegition 
            if (!string.IsNullOrEmpty(specpram.sort)) {
                switch(specpram.sort)
                {
                    case "name":
                    Orderby(b => b.BookTitle);
                    break;
                    //case "fathy" -> some instructions -->>+-* in java without break;
                    default:Orderby(b => b.catygories);
                    break;
                
                }
          
            }

       //     ApplyPagtion(productpram.PageSize, productpram.PageSize * (productpram.pageIndex - 1));

            ApplyPigation(specpram.pagesize * (specpram.PageIndex-1), specpram.pagesize);
        }
        public bookspacefcation(int id):base(b=>b.Id==id)
        {
            includes.Add(b => b.Aurthor);
           includes.Add(b=>b.reviews);
           includes.Add(b=>b.catygories); 
            includes.Add(b => b.record);


        }
    }
}
