namespace BugTrackingSystem.ViewModels.AccountViewModels
{
    public class ExternalLoginsViewModel
    {
        public List<ExternalLoginProviderInfo> Providers { get; set; }

        public bool HasAny => Providers?.Count > 0;
    }

    public class ExternalLoginProviderInfo
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string IconCSSClass { get; set; }
    }
}
