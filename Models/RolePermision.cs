namespace BugTrackingSystem.Models
{
    public class RolePermission
    {
        public int Id { get; set; }
        public int RoleID { get; set; }
        public int PermissionID { get; set; }
        public Role Role { get; set; }
        public Permission Permission { get; set; }
    }
}
