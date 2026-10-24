using System;
using System.Collections.Generic;
using System.Text;

namespace DEPI_EF_Session14.Domain
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }
         public DateTime AppointmentDate { get; set; }

        // Navigation properties

        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }
}
