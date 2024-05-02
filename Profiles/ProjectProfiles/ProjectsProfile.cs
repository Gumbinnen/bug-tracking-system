using AutoMapper;
using BugTrackingSystem.Models.Entities;
using BugTrackingSystem.ViewModels.ProjectViewModels;

namespace BugTrackingSystem.Profiles.ProjectProfiles
{
    internal class ProjectsProfile : Profile
    {
        public ProjectsProfile()
        {
            CreateMap<IEnumerable<Project>, IndexProjectViewModel>()
                .ForMember(
                    dest => dest.Projects,
                    src => src.MapFrom(p => p)
                );
        }
    }
}
