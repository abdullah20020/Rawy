using core.Models;
using core.Spacification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Repository
{
    public interface IGenaricReposteryUSers<T>where T:BaseUser
    {
        Task<IReadOnlyList<T>> getallAsync();
        Task<T> getbyidasync(int id);
        Task setrecord(T record);

    }
}
