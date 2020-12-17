using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.DTO
{
    public class PatientDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        //public ICollection<DoctorDTO> Doctors { get; set; }

        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }

        //public ICollection<Patient_VisitingDTO> Visitings { get; set; }
    }
}
