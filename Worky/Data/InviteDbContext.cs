using Microsoft.EntityFrameworkCore;
using Worky.Project;

namespace Worky.Data
{
    public class InviteDbContext : DbContext, Data.IIviteCollection
    {
        //Add-Migration s -context InviteDbContext
        /////Update-Database -context  InviteDbContext
        DbSet<Invite> invites { get; set; }
        public InviteDbContext(DbContextOptions<InviteDbContext> options)
              : base(options)
        {
            Database.EnsureCreated();
        }
        public void AddInvite(Invite invite)
        {
            invites.Add(invite);
            this.SaveChanges();
        }

        public void Delete(Invite invite)
        {
            this.invites.Remove(invites.Where(i => i.Id == invite.Id).FirstOrDefault());
            this.SaveChanges();
        }
        public void RemoveProjectInvites(int ProijectId)
        {
            foreach(Invite i in invites.Where(i=>i.ProjectId==ProijectId))
            {
                Delete(i);
            }
        }

        public List<Invite> GetInvites(int UserId)
        {
            return this.invites.Where(i => i.UserId == UserId).ToList();
        }

        public List<Invite> GetInvitesForProject(int id)
        {
            return this.invites.Where(i => i.ProjectId == id).ToList();
        }

        public Invite GetInvite(int Id)
        {
            return this.invites.Where(i => i.Id == Id).FirstOrDefault();
        }
    }
}
