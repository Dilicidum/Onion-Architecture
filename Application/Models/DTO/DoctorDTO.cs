using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.DTO
{
    public class DoctorDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfApplying { get; set; }
        public double WorkExperience { get; set; }
        public string Post { get; set; }
        public string Specialization { get; set; }
        public double Sallary { get; set; }
        public double TimeToTakePatient { get; set; }
        
        public List<FreeTime> FreeTimes { get; set; }
        //public ICollection<Doctor_Schedule> Doctor_Schedules { get; set; }
        //public ICollection<Patient_Visiting> Appointments { get; set; }

        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }
    }
}
