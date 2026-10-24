using DEPI_EF_Session14.Domain;
using DEPI_WebApi_Session2.Infrestraction.Data;

namespace DEPI_WebApi_Session2.Seeding
{
    public static  class StoreContextSeeding
    {
        public static async Task SeedDataAsync(ApplicationDBContext context)
        {
            if(!context.Doctor.Any())
            {
                var doctordata = await File.ReadAllTextAsync("Seeding/DoctorSeedData.json");
                var doctors = System.Text.Json.JsonSerializer.Deserialize<List<Doctor>>(doctordata);
                context.Doctor.AddRange(doctors);
                await context.SaveChangesAsync();
            }

            if(!context.Patients.Any())
            {
                var patientdata = await File.ReadAllTextAsync("Seeding/PatientSeedData.json");
                var patients = System.Text.Json.JsonSerializer.Deserialize<List<Patient>>(patientdata);
                context.Patients.AddRange(patients);
                await context.SaveChangesAsync();
            }

            if(!context.Appointments.Any())
            {
                var appointmentdata = await File.ReadAllTextAsync("Seeding/AppointmentSeedData.json");
                var appointments = System.Text.Json.JsonSerializer.Deserialize<List<Appointment>>(appointmentdata);
                context.Appointments.AddRange(appointments);
                await context.SaveChangesAsync();
            }

        }
    }
}
