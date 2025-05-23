﻿using core.Models;
using core.Spacification;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Repsotiry.spacification
{
    public static class spacificationEvalator<T> where T : BaseClass
    {
        public static IQueryable<T> GetQuery(IQueryable<T> input, Ispacfiaction<T> spac)
        {
            var Query = input;

            if (spac.cretaria is not null)
                Query = input.Where(spac.cretaria);

            if (spac.OrderBy is not null)
            {
                Query = Query.OrderBy(spac.OrderBy);
            }

            if (spac.OrderBydecync is not null)
            {
                Query = Query.OrderByDescending(spac.OrderBydecync);
            }
            if (spac.Ispigation)
                Query = Query.Skip(spac.PageIndex).Take(spac.PageSize);
            Query = spac.includes.Aggregate(Query, (q1, q2) => q1.Include(q2));


            foreach (var thenInclude in spac.GetThenIncludes())
            {
                Query = thenInclude(Query); 
            }


            return Query;


            
        }
    }
}