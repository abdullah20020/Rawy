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
    public class GenaricRepostry<T> : IGenaricrepostry<T> where T : BaseClass
    {
        private readonly RawyDbcontext rawyDbcontext;

        public GenaricRepostry(RawyDbcontext rawyDbcontext )
        {
            this.rawyDbcontext = rawyDbcontext;
        }

        public async Task<IReadOnlyList<T>> getallwithspacAsync(Ispacfiaction<T> spac)
        {
            return await Applicationsystem(spac).ToListAsync();
        }

        public async Task<T> getbyidasync(int id)
        {
            return await rawyDbcontext.Set<T>().FindAsync(id);

        }

        public async Task<T> getbyidwithspacAsync(Ispacfiaction<T> spac)
        {
            return await Applicationsystem(spac).FirstOrDefaultAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await rawyDbcontext.Set<T>().FindAsync(id);
        }
 
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await rawyDbcontext.Set<T>().ToListAsync();
        }
        public async Task<ICollection<T>> GetBooksByIdsAsync(ICollection<int> Ids)
        {
            return await rawyDbcontext.Set<T>()
                .Where(b => Ids.Contains(b.Id))
                .ToListAsync();
        }
        public async Task<T> set(T entity)
        {
            await rawyDbcontext.AddAsync(entity); 
            await rawyDbcontext.SaveChangesAsync(); 
            return entity;
        }
        public async Task<T> UpdateAsync(T entity)
        {
            rawyDbcontext.Update(entity);
            await rawyDbcontext.SaveChangesAsync();
            return entity;
        }
        public async Task DeleteAsync(T entity)
        {
            rawyDbcontext.Set<T>().Remove(entity);
            await rawyDbcontext.SaveChangesAsync();
        }
        private IQueryable<T> Applicationsystem(Ispacfiaction<T> spac) {
            return spacificationEvalator<T>.GetQuery(rawyDbcontext.Set<T>(), spac);
        }
    }
}
