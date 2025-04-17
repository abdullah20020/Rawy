using core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Contracts.AuthContracts
{
    public interface IAuthService
    {
        Task<string> CreateTokenAsync(BaseUser user, UserManager<BaseUser> userManager);

    }
}
