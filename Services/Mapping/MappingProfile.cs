using AutoMapper;
using BugTrackingSystem.Models.Entities;
using BugTrackingSystem.ViewModels.ProjectViewModels;

namespace BugTrackingSystem.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Project, ProjectViewModel>();
        }
    }
}