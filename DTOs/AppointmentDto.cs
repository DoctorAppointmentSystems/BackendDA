namespace DoctorAppointment.API.DTOs
{
    public class AppointmentDto
    {
        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string? SlotTime { get; set; }

        public string? Mode { get; set; }
    }
}
