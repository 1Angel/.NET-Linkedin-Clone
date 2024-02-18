using LinkedinClone.Repositories.Interfaces;

namespace LinkedinClone.Repositories
{
    public class UserProvider : iUserProvider
    {
        private readonly IHttpContextAccessor _context;
        public UserProvider(IHttpContextAccessor context)
        {

            _context = context;

        }
        public string UserId()
        {
            var userid = _context.HttpContext.User.Claims.Where(x=>x.Type =="Id").FirstOrDefault()?.Value;
            return userid;
        }
    }
}
