using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.DTO
{
    public class Patient_VisitingDTO
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime TimeOfVisit { get; set; }
        public string Diagnosis { get; set; }
        public string Specialization { get; set; }
    }
}
