using System.Numerics;

namespace DoctorAppointment.API.Models
{
    public class Specialty
    {
        public int SpecialtyId { get; set; }

        public string? SpecialtyName { get; set; }

        public string? Description { get; set; }

        public ICollection<Doctor>? Doctors { get; set; }
    }
}
