namespace DoctorAppointment.API.Models
{
    public class DoctorAvailability
    {
        public int AvailabilityId { get; set; }

        public int DoctorId { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public string? Mode { get; set; }

        public Doctor? Doctor { get; set; }
    }
}
