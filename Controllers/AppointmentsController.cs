using DoctorAppointment.API.Data;
using DoctorAppointment.API.DTOs;
using DoctorAppointment.API.Models;
using DoctorAppointment.API.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.API.Controllers
{
    [ApiController]
    [Route("api/appointments")]
    public class AppointmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public AppointmentsController(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // BOOK APPOINTMENT
        [HttpPost("book")]
        public async Task<IActionResult> BookAppointment(AppointmentDto dto)
        {
            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.DoctorId == dto.DoctorId);

            if (doctor == null)
                return BadRequest("Doctor not found");

            var patient = await _context.Users
                .FirstOrDefaultAsync(p => p.UserId == dto.PatientId);

            if (patient == null)
                return BadRequest("Patient not found");

            // Prevent double booking
            var slotExists = await _context.Appointments.AnyAsync(a =>
                a.DoctorId == dto.DoctorId &&
                a.AppointmentDate == dto.AppointmentDate &&
                a.SlotTime == dto.SlotTime);

            if (slotExists)
                return BadRequest("This slot is already booked");

            var fee = dto.Mode == "Online"
                ? doctor.ConsultationFeeOnline
                : doctor.ConsultationFeeOffline;

            var appointment = new Appointment
            {
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
                AppointmentDate = dto.AppointmentDate,
                SlotTime = dto.SlotTime,
                Mode = dto.Mode,
                Status = "Booked",
                Fee = fee
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            // Send email
            _emailService.SendEmail(
                patient.Email,
                "Appointment Confirmation",
                $"Your appointment with Dr. {doctor.DoctorName} on {dto.AppointmentDate:dd MMM yyyy} at {dto.SlotTime} is confirmed."
            );

            return Ok(appointment);
        }

        // COMPLETE APPOINTMENT
        [HttpPut("complete/{id}")]
        public async Task<IActionResult> CompleteAppointment(int id)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);

            if (appointment == null)
                return NotFound("Appointment not found");

            appointment.Status = "Completed";

            await _context.SaveChangesAsync();

            _emailService.SendEmail(
                appointment.Patient.Email,
                "Appointment Completed",
                $"Your consultation with Dr. {appointment.Doctor.DoctorName} has been completed. Thank you for visiting."
            );

            return Ok(appointment);
        }
    }
}