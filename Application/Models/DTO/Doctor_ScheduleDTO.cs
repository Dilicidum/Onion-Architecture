using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.DTO
{
    public class Doctor_ScheduleDTO
    {
        public int DoctorId { get; set; }
        public DoctorDTO Doctor { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan BreakTimeStart { get; set; }
        public TimeSpan BreakEndTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
