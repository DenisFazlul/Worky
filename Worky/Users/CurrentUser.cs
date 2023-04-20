using System.Security.Claims;

namespace Worky.Users
{
    public class CurrentUser
    {
        public int Id { get; set; }
        public CurrentUser(ClaimsPrincipal user)
        {
            Parce(user);
        }
        private void Parce(ClaimsPrincipal c)
        {
            Claim cId = c.Claims.Where(i => i.Type == "id").FirstOrDefault();
            this.Id =Convert.ToInt32( cId.Value);
        }
    }
}
