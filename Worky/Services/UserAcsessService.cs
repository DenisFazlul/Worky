using Worky.Data;
using Worky.Data.Project;
using Worky.Project;

namespace Worky.Services
{
    public class UserAcsessService
    {
        IIviteCollection invites;
        IProjectDb projects;
        public UserAcsessService(Data.IIviteCollection iDb,IProjectDb pDb)
        {
            invites = iDb;
            projects = pDb;

        }
        public bool IsUserAccsessToProject(Users.User user,Project.Project project)
        {
            
            if(project.UserId==user.Id)
            {
                return true;
            }
            List<Invite> userinvites= invites.GetInvitesForProject(project.Id);

            Invite existInvite = userinvites.Where(i => i.UserId == user.Id).FirstOrDefault();
            if(existInvite!=null)
            {
                return true;
            }
            else
            {
                return false;
            }

             
        }
    }
}
