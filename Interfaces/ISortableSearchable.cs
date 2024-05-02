using BugTrackingSystem.Enums;

namespace BugTrackingSystem.Interfaces
{
    public interface ISortableSearchable
    {
        string SortColumn { get; set; }
        SortDirection SortDirection { get; set; }
        string SearchTerm { get; set; }
    }
}
