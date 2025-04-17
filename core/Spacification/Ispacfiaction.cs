using core.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace core.Spacification
{
    public interface Ispacfiaction<T> where T : BaseClass
    {


        Expression<Func<T, bool>> cretaria { get; set; }
        List<Expression<Func<T, object>>> includes { get; set; }
        public List<Func<IQueryable<T>, IQueryable<T>>> ThenIncludes { get; set; }

        Expression<Func<T, object>> OrderBy { get; set; }
        Expression<Func<T, object>> OrderBydecync { get; set; }
        int PageIndex { get; set; }
        int PageSize { get; set; }
        bool Ispigation { get; set; }
        IEnumerable<Func<IQueryable<T>, IQueryable<T>>> GetThenIncludes();

    }
}
