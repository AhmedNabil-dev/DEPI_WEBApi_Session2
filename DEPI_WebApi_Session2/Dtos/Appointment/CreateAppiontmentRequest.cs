namespace DEPI_WebApi_Session2.Dtos.Appointment
{
    public record CreateAppiontmentRequest(DateTime AppointmentDate,int DoctorId
        
        ,int PatientId)
    {
    }
}
