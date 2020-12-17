using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Patient_Visiting
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime TimeOfVisit { get; set; }
        public TimeSpan Start_TimeVisit { get; set; }
        public TimeSpan End_TimeVisit { get; set; }
        public string Diagnosis { get; set; }
        public string Specialization { get; set; }
    }
}
