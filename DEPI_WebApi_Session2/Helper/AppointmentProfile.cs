using AutoMapper;
using DEPI_EF_Session14.Domain;
using DEPI_WebApi_Session2.Dtos.Appointment;

namespace DEPI_WebApi_Session2.Helper
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, GetAppointmentresponse>().ReverseMap();
            CreateMap<CreateAppiontmentRequest, Appointment>().ReverseMap();
            CreateMap<UpdateAppointmentRequest, Appointment>().ReverseMap();
        }
    }
}
