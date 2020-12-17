using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public ICollection<Doctor> Doctors { get; set; }

        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }

        public ICollection<Patient_Visiting> Visitings { get; set; }
    }
}
