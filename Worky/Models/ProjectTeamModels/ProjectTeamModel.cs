namespace Worky.Models.ProjectTeamModels
{
    public class ProjectTeamModel
    {
        public int ProjectId { get; set; }
        public bool AccsessToAAddUserToProject { get; set; } = false;
        internal List<InviteModel> invites { get; set; }
    }
}
