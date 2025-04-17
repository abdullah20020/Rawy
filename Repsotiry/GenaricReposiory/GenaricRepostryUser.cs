using core.Models;
using core.Repository;
using core.Spacification;
using Microsoft.EntityFrameworkCore;
using Repsotiry.Data;
using Repsotiry.spacification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repsotiry.GenaricReposiory
{
    public class GenaricRepostryusers<T> : IGenaricReposteryUSers<T> where T : BaseUser
    {
        private readonly RawyDbcontext rawyDbcontext;

        public GenaricRepostryusers(RawyDbcontext rawyDbcontext )
        {
            this.rawyDbcontext = rawyDbcontext;
        }
        public async Task<IReadOnlyList<T>> getallAsync( )
        {
      
          return await rawyDbcontext.Set<T>().ToListAsync();
        }

      
        public async Task<T> getbyidasync(string id)
        {
            return await rawyDbcontext.Set<T>().FirstOrDefaultAsync();

        }

        public Task<T> getbyidasync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task setrecord(T record)
        {
           await rawyDbcontext.AddAsync<T>(record);
        }
    }
}
