namespace DoctorAppointment.API.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string? SlotTime { get; set; }

        public string? Mode { get; set; }

        public string? Status { get; set; }

        public decimal Fee { get; set; }

        public User? Patient { get; set; }

        public Doctor? Doctor { get; set; }
    }
}
