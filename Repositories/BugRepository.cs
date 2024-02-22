using BugTrackingSystem.Database;
using BugTrackingSystem.Interfaces;

namespace BugTrackingSystem.Repositories
{
    public class BugRepository : IBugRepository
    {
        private readonly ApplicationDBContext context;
        public BugRepository(ApplicationDBContext context)
        {
            this.context = context;
        }
    }
}
