namespace DoctorAppointment.API.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }

        public string? DoctorName { get; set; }

        public string? Email { get; set; }

        public int SpecialtyId { get; set; }

        public decimal ConsultationFeeOnline { get; set; }

        public decimal ConsultationFeeOffline { get; set; }

        public string? ClinicAddress { get; set; }

        public Specialty? Specialty { get; set; }

        public ICollection<DoctorAvailability>? Availabilities { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
