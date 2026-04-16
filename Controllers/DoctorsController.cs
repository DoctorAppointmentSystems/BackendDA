using DoctorAppointment.API.Data;
using DoctorAppointment.API.DTOs;
using DoctorAppointment.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DoctorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET ALL DOCTORS
        [HttpGet]
        public IActionResult GetDoctors()
        {
            var doctors = _context.Doctors
                .Include(d => d.Specialty)
                .ToList();

            return Ok(doctors);
        }

        // GET DOCTOR BY ID
        [HttpGet("{id}")]
        public IActionResult GetDoctor(int id)
        {
            var doctor = _context.Doctors
                .Include(d => d.Specialty)
                .FirstOrDefault(d => d.DoctorId == id);

            if (doctor == null)
                return NotFound("Doctor not found");

            return Ok(doctor);
        }

        // ADD NEW DOCTOR
        [HttpPost]
        public IActionResult AddDoctor([FromBody] DoctorCreateDto dto)
        {
            var doctor = new Doctor
            {
                DoctorName = dto.DoctorName,
                Email = dto.Email,
                SpecialtyId = dto.SpecialtyId,
                ConsultationFeeOnline = dto.ConsultationFeeOnline,
                ConsultationFeeOffline = dto.ConsultationFeeOffline,
                ClinicAddress = dto.ClinicAddress
            };

            _context.Doctors.Add(doctor);
            _context.SaveChanges();

            return Ok(doctor);
        }

        // UPDATE DOCTOR
        [HttpPut("{id}")]
        public IActionResult UpdateDoctor(int id, [FromBody] DoctorUpdateDto dto)
        {
            var doctor = _context.Doctors.Find(id);

            if (doctor == null)
                return NotFound("Doctor not found");

            doctor.DoctorName = dto.DoctorName;
            doctor.Email = dto.Email;
            doctor.SpecialtyId = dto.SpecialtyId;
            doctor.ConsultationFeeOnline = dto.ConsultationFeeOnline;
            doctor.ConsultationFeeOffline = dto.ConsultationFeeOffline;
            doctor.ClinicAddress = dto.ClinicAddress;

            _context.SaveChanges();

            return Ok(doctor);
        }

        // DELETE DOCTOR
        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(int id)
        {
            var doctor = _context.Doctors.Find(id);

            if (doctor == null)
                return NotFound("Doctor not found");

            _context.Doctors.Remove(doctor);
            _context.SaveChanges();

            return Ok("Doctor deleted successfully");
        }
    }
}