using AutoMapper;
using DEPI_EF_Session14.Domain;
using DEPI_WebApi_Session2.Dtos.Patient;
using DEPI_WebApi_Session2.Infrestraction.Data;
using DEPI_WebApi_Session2.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DEPI_Session1_WedAPI.Controllers
{
    [Route("api/Patient")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<PatientController> _logger;
        public PatientController(ApplicationDBContext context, IMapper mapper, ILogger<PatientController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet("getAll")]
        public async Task<IActionResult> GetallPatient(int pageNumber, int PageSize, string? searchTerm)
        {
            List<Patient> patients;
            var totalRecords = 0;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                patients = await _context.Patients
                    .Where(p => p.Name.Contains(searchTerm))
                    .Skip((pageNumber - 1) * PageSize)
                    .Take(PageSize)
                    .ToListAsync();
                totalRecords = await _context.Patients.CountAsync(p => p.Name.Contains(searchTerm));
            }
            else
            {
                patients = await _context.Patients
                    .Skip((pageNumber - 1) * PageSize)
                    .Take(PageSize)
                    .ToListAsync();
                totalRecords = await _context.Patients.CountAsync();
            }
            if (patients == null || !patients.Any())
            {
                _logger.LogWarning("No patients found in the database.");
                return NotFound("No patients found.");
            }
            var patientResponse = _mapper.Map<List<GetPatientResponse>>(patients);
            var PaginationResponse = new PaginatedList<GetPatientResponse>()
            {
                pageNumber = pageNumber,
                PageSize = PageSize,
                TotalRecords = totalRecords,
                TotalPage = (int)Math.Ceiling((double)totalRecords / PageSize),
                Data = patientResponse
            };

            


            var result =  GenericResult<PaginatedList<GetPatientResponse>>.Success(PaginationResponse);
            
            return Ok(result);
        }

        //[HttpGet("getById/{id}")]
        //public async Task<IActionResult> GetPatientById(int id)
        //{
        //    var patient = await _context.Patients.FindAsync(id);
        //    if (patient == null)
        //    {
        //        _logger.LogWarning($"Patient with ID {id} not found.");
        //        return NotFound("Patient not found.");
        //    }
        //    return Ok(_mapper.Map<GetPatientResponse>(patient));
        //}

        [HttpPost("create")]
        public async Task<IActionResult> CreatePatient(CreatePatientRequest request)
        {
            var patient = _mapper.Map<Patient>(request);
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return Created();
        }

        [HttpPut("update/{id}")]
       public async Task<IActionResult>  UpdatePatient(UpdatePatientRequest request)
        {
            var result = await _context.Patients.FindAsync(request.Id);

            if (result == null)
            {
                _logger.LogWarning($"Patient with ID {request.Id} not found for update.");
                return NotFound("Patient not found.");
            }
            result = _mapper.Map(request, result);

            _context.Patients.Update(result);
            await _context.SaveChangesAsync();

            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                _logger.LogWarning($"Patient with ID {id} not found for deletion.");
                return NotFound("Patient not found.");
            }
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
