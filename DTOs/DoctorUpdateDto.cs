namespace DoctorAppointment.API.DTOs
{
    public class DoctorUpdateDto
    {
        public string? DoctorName { get; set; }

        public string? Email { get; set; }

        public int SpecialtyId { get; set; }

        public decimal ConsultationFeeOnline { get; set; }

        public decimal ConsultationFeeOffline { get; set; }

        public string? ClinicAddress { get; set; }
    }
}
