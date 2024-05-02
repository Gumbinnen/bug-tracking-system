using BugTrackingSystem.Database;
using BugTrackingSystem.Interfaces.Repository;

namespace BugTrackingSystem.Repositories
{
    public sealed class BugRepository : IBugRepository
    {
        private readonly ApplicationDBContext context;
        public BugRepository(ApplicationDBContext context)
        {
            this.context = context;
        }
    }
}
