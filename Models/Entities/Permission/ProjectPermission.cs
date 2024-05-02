using BugTrackingSystem.Enums.PermissionType;

namespace BugTrackingSystem.Models.Entities
{
    public class ProjectPermission : Permission
    {
        public ProjectPermission(ProjectPermissionType projectPermissionType) : base(PermissionType.Project, (int)projectPermissionType, projectPermissionType.ToString())
        {
            Type = PermissionType.Project;
            SubTypeValue = (int)projectPermissionType;
            Name = projectPermissionType.ToString();
        }
    }
}
