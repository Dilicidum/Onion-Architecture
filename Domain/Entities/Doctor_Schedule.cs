using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Doctor_Schedule
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan BreakTimeStart { get; set; }
        public TimeSpan BreakEndTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
