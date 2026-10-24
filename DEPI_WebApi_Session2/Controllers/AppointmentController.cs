using AutoMapper;
using DEPI_EF_Session14.Domain;
using DEPI_WebApi_Session2.Dtos.Appointment;
using DEPI_WebApi_Session2.Infrestraction.Data;
using DEPI_WebApi_Session2.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DEPI_Session1_WedAPI.Controllers
{
    [Route("api/Appointment")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<AppointmentController> _logger;
        public AppointmentController(ApplicationDBContext context, IMapper mapper, ILogger<AppointmentController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }   
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllAppointments(int pageNumber, int PageSize, string? searchTerm)
        {
            List<Appointment> appointments;
            var totalRecords = 0;
            if(!string.IsNullOrEmpty(searchTerm))
            {
                appointments = await _context.Appointments
                    .Where(a => a.PatientId.ToString().Contains(searchTerm) || a.DoctorId.ToString().Contains(searchTerm))
                    .Skip((pageNumber - 1) * PageSize)
                    .Take(PageSize)
                    .ToListAsync();
                totalRecords = await _context.Appointments
                    .CountAsync(a => a.PatientId.ToString().Contains(searchTerm) || a.DoctorId.ToString().Contains(searchTerm));
            }
            else
            {
                appointments = await _context.Appointments
                    .Skip((pageNumber - 1) * PageSize)
                    .Take(PageSize)
                    .ToListAsync();
                totalRecords = await _context.Appointments.CountAsync();
            }
            var response = new PaginatedList<GetAppointmentresponse>
            {
                pageNumber = pageNumber,
                PageSize = PageSize,
                TotalRecords = totalRecords,
                TotalPage = (int)Math.Ceiling((double)totalRecords / PageSize),
                Data = _mapper.Map<List<GetAppointmentresponse>>(appointments)
            };


            var result = GenericResult<PaginatedList<GetAppointmentresponse>>.Success(response);
            return Ok(result);
        }

        //[HttpGet("getById/{id}")]
        //public async Task<IActionResult> GetAppointmentById(int id)
        //{
        //    var appointment = await _context.Appointments.FindAsync(id);
        //    if (appointment == null)
        //    {
        //        _logger.LogWarning($"Appointment with ID {id} not found.");
        //        return NotFound("Appointment not found.");
        //    }
        //    return Ok(_mapper.Map<GetAppointmentresponse>(appointment));
        //}
        
        [HttpPost("create")]
        public async Task<IActionResult> CreateAppointment(CreateAppiontmentRequest request)
        {
            var appointment = _mapper.Map<Appointment>(request);
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return Created();
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAppointment( UpdateAppointmentRequest request)
        {
            var appointment = await _context.Appointments.FindAsync(request.Id);
            if (appointment == null)
            {

                _logger.LogWarning($"Appointment with ID {request.Id} not found for update.");
                return NotFound("Appointment not found.");
            }
            appointment = _mapper.Map(request, appointment);    
            await _context.SaveChangesAsync();
            return Ok(appointment);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                _logger.LogWarning($"Appointment with ID {id} not found for deletion.");
                return NotFound("Appointment not found.");
            }
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
