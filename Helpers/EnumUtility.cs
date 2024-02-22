namespace BugTrackingSystem.Helpers
{
    public class EnumUtility
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
