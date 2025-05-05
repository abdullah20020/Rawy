using core.Models;
using core.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Rawy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IGenaricReposteryUSers<BaseUser> userrepo;

        UserAccountController(IGenaricReposteryUSers<BaseUser> Userrepo)
        {
            userrepo = Userrepo;
        }

        //[HttpGet] 
        //public IActionResult Get() {
    }
}
