using AutoMapper;
using DEPI_EF_Session14.Domain;
using DEPI_WebApi_Session2.Dtos.Doctor;

namespace DEPI_WebApi_Session2.Helper
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<CreateDoctorRequest, Doctor>().ReverseMap();
            CreateMap<UpdateDoctorRequest, Doctor>().ReverseMap();
            CreateMap<Doctor, GetDoctorResponse>().ReverseMap();
        }
    }
}
