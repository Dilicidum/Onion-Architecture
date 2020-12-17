using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Entities
{
    public enum Specialization
    {
        Anatomical_Pathology,
        Anesthesiology,
        Cardiology,
        Cardiovascular_OR_Thoracic_Surgery,
        Clinical_Immunology_OR_Allergy,
        Critical_Care_Medicine,
        Dermatology,
        Diagnostic_Radiology,
        Emergency_Medicine,
        Endocrinology_AND_Metabolism,
        Family_Medicine,
        Gastroenterology,
        General_Internal_Medicine,
        General_Surgery,
        General_OR_Clinical_Pathology,
        Geriatric_Medicine,
        Hematology,
        Medical_Biochemistry,
        Oncology,
        Neurology,
        Neurosurgery,
        Obstetrics_OR_Gynecology,
        Ophthalmology,
        Otolaryngology,
        Orthopedic_Surgery,
        Pediatrics,
        Psychiatry,
    }

    public enum Post
    {
        Intern,
        Regular_doctor,
        Department_head,
    }

    public class Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfApplying { get; set; }
        public double WorkExperience { get; set; }
        
        public double Sallary { get; set; }
        
        public TimeSpan TimeToTakePatient { get; set; }
        public Specialization Specialization { get; set; }
        public Post Post { get; set; }
        public ICollection<Doctor_Schedule> Doctor_Schedules { get; set; }
        public ICollection<Patient_Visiting> Appointments { get; set; }
        
        [NotMapped]
        public double TimeToTakePatientMapped
        {
            get
            {
                return this.TimeToTakePatient.TotalMinutes;
            }
            set
            {
                this.TimeToTakePatient = TimeSpan.FromMinutes(value);
            }
        }

        [NotMapped]
        public string SpecializationMapped
        {
            get
            {
                var specialization = Specializations[this.Specialization];
                return specialization;
            }
            set
            {
                var specialization = Specializations.FirstOrDefault(x => x.Value == value).Key;
                this.Specialization = specialization;
            }
        }
        [NotMapped]
        public string PostMapped
        {
            get
            {
                var post = Specializations[this.Specialization];
                return post;
            }
            set
            {
                var post = Posts.FirstOrDefault(x => x.Value == value).Key;
                this.Post = post;
            }
        }
        [NotMapped]
        public static Dictionary<Post, string> Posts = new Dictionary<Post,
            string>()
        {
            { Post.Department_head,"Department head" },
            { Post.Regular_doctor,"Regular doctor"},
            { Post.Intern,"Intern"},
        };
        [NotMapped]
        public static Dictionary<Specialization, string> Specializations = new Dictionary<Specialization,
            string>()
        {
            {Specialization.Anatomical_Pathology,"Anatomical Pathology" },
            {Specialization.Anesthesiology,"Anesthesiology" },
            {Specialization.Cardiology,"Cardiology"},
            {Specialization.Cardiovascular_OR_Thoracic_Surgery,"Cardiovascular or Thoracic Surgery" },
            {Specialization.Clinical_Immunology_OR_Allergy,"Clinical Immunology or Allergy" },
            {Specialization.Critical_Care_Medicine,"Critical Care Medicine" },
            {Specialization.Dermatology,"Dermatology" },
            {Specialization.Diagnostic_Radiology,"Diagnostic Radiology" },
            {Specialization.Emergency_Medicine,"Emergency Medicine" },
            {Specialization.Endocrinology_AND_Metabolism,"Endocrinology and Metabolism" },
            {Specialization.Family_Medicine,"Family Medicine" },
            {Specialization.Gastroenterology,"Gastroenterology" },
            {Specialization.General_Internal_Medicine,"General Internal Medicine" },
            {Specialization.General_OR_Clinical_Pathology,"General or Clinical Pathology" },
            {Specialization.General_Surgery,"General Surgery" },
            {Specialization.Geriatric_Medicine,"Geriatric Medicine" },
            {Specialization.Hematology,"Hematology" },
            {Specialization.Medical_Biochemistry,"Medical Biochemistry" },
            {Specialization.Neurology,"Neurology" },
            {Specialization.Neurosurgery,"Neurosurgery" },
            {Specialization.Obstetrics_OR_Gynecology,"Obstetrics or Gynecology" },
            {Specialization.Oncology,"Oncology" },
            {Specialization.Ophthalmology,"Ophthalmology" },
            {Specialization.Orthopedic_Surgery,"Orthopedic Surgery" },
            {Specialization.Otolaryngology,"Otolaryngology" },
            {Specialization.Pediatrics,"Pediatrics" },
            {Specialization.Psychiatry,"Psychiatry" }
        };
    }
}
