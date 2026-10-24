namespace DEPI_WebApi_Session2.Dtos.Patient
{
    public record UpdatePatientRequest(int Id, string Name, DateTime DateOfBirth)
    {
    }
}
