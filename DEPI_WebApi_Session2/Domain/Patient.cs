using System;
using System.Collections.Generic;
using System.Text;

namespace DEPI_EF_Session14.Domain
{
    public class Patient
    {
        public int PatientId { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
