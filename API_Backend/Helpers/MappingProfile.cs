using API_Backend.Models;
using API_Backend.Models.Dtos;
using AutoMapper;

namespace API_Backend.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Reclamation, ReclamationDetailDto>()
                .ForMember(dest => dest.UtilisateurNom, opt => opt.MapFrom(src => src.Utilisateur.Nom))
                .ForMember(dest => dest.ArticleNom, opt => opt.MapFrom(src => src.Article.Nom))
                .ForMember(dest => dest.InterventionIds, opt => opt.MapFrom(src => src.Interventions.Select(i => i.InterventionId).Distinct()));
            CreateMap<ReclamationDto, Reclamation>()
                .ForMember(dest => dest.Utilisateur, opt => opt.Ignore());

            CreateMap<Intervention, InterventionDetailDto>()
                .ForMember(dest => dest.UtilisateurNom, opt => opt.MapFrom(src => src.Utilisateur.Nom))
                .ForMember(dest => dest.ReclamationDesc, opt => opt.MapFrom(src => src.Reclamation.Description));
            CreateMap<InterventionDto, Intervention>()
                .ForMember(dest => dest.Utilisateur, opt => opt.Ignore());
                //.ForMember(dest => dest.Reclamation, opt => opt.Ignore());
        }
    }
}
