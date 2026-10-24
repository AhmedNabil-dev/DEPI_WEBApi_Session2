using AutoMapper;
using DEPI_EF_Session14.Domain;
using DEPI_WebApi_Session2.Dtos.Patient;

namespace DEPI_WebApi_Session2.Helper
{
    public class patientProfile : Profile 
    {
        public patientProfile()
        {
            CreateMap<CreatePatientRequest, Patient>().ReverseMap();
            CreateMap<UpdatePatientRequest, Patient>().ReverseMap();
            CreateMap<Patient, PatientResponse>().ReverseMap();
        }
    }
}
