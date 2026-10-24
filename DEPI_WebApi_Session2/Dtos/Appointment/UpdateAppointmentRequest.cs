namespace DEPI_WebApi_Session2.Dtos.Appointment
{
    public record UpdateAppointmentRequest(int Id, DateTime AppointmentDate, int DoctorId, int PatientId)
    {
    }
}
