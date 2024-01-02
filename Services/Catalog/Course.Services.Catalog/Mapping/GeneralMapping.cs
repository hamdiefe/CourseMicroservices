using AutoMapper;
using Course.Services.Catalog.Dtos;
using Course.Services.Catalog.Models;
namespace Course.Services.Catalog.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Course.Services.Catalog.Models.Course, CourseDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();

            CreateMap<Course.Services.Catalog.Models.Course, CourseCreateDto>().ReverseMap();
            CreateMap<Course.Services.Catalog.Models.Course, CourseUpdateDto>().ReverseMap();

            CreateMap<Category, CategoryCreateDto>().ReverseMap();
        }
    }
}
