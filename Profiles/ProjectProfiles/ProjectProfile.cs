using AutoMapper;
using BugTrackingSystem.Models.Entities;
using BugTrackingSystem.ViewModels.ProjectViewModels;

namespace BugTrackingSystem.Profiles.ProjectProfiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectViewModel>()
                .ForMember(
                    dest => dest.Name,
                    src => src.MapFrom(p => p.Name))
                .ForMember(
                    dest => dest.Description,
                    src => src.MapFrom(p => p.Description))
                .ForMember(
                    dest => dest.PersonalSpace,
                    src => src.MapFrom(p => p.PersonalSpace))
                .ForMember(
                    dest => dest.Bugs,
                    src => src.MapFrom(p => p.Bugs))
                .ForMember(
                    dest => dest.CreatedRoles,
                    src => src.MapFrom(p => p.CreatedRoles));

            CreateMap<Project, DetailsViewModel>()
                .ForMember(
                    dest => dest.Name,
                    src => src.MapFrom(p=>p.Name))
                .ForMember(
                    dest => dest.Description,
                    src => src.MapFrom(p => p.Description))
                .ForMember(
                    dest => dest.Creator,
                    src => src.MapFrom(p => p.PersonalSpace.User))
                .ForMember(
                    dest => dest.BugCount,
                    src => src.MapFrom(p=>p.Bugs.Count))
                .ForMember(
                    dest=> dest.CreatedRoles,
                    src => src.MapFrom(p=>p.CreatedRoles));
        }
    }
}
