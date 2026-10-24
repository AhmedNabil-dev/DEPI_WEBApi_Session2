using AutoMapper;
using DEPI_EF_Session14.Domain;
using DEPI_WebApi_Session2.Dtos.Doctor;
using DEPI_WebApi_Session2.Infrestraction.Data;
using DEPI_WebApi_Session2.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using System.Runtime.CompilerServices;

namespace DEPI_Session1_WedAPI.Controllers
{
    [Route("api/Doctor")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<DoctorController> _logger;

        public DoctorController(ApplicationDBContext context, IMapper mapper, ILogger<DoctorController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetallDoctors(int pageNumber, int PageSize, string? searchTerm)
        {
            List<Doctor> Doctors;
            var totalCount =0;
            if (searchTerm is not null)
            {
                 Doctors = await _context.Doctor
                                   .Where(d => d.Name.Contains(searchTerm))
                                   .Skip((pageNumber - 1) * PageSize)
                                   .Take(PageSize)
                                   .ToListAsync();
                 totalCount = await _context.Doctor.CountAsync(d => d.Name.Contains(searchTerm));
            }
            else
            {
                 Doctors = await _context.Doctor
                                   .Skip((pageNumber - 1) * PageSize)
                                   .Take(PageSize)
                                   .ToListAsync();
                 totalCount = await _context.Doctor.CountAsync();
            }
            _logger.LogInformation("Get All Doctors Endpoint", Doctors);

            var Doctorresponse = _mapper.Map<List<Doctor>,List<GetDoctorResponse>>(Doctors);
            var paginatedList = new PaginatedList<GetDoctorResponse>()
            {
                PageSize = PageSize,
                pageNumber = pageNumber,
                TotalRecords = totalCount,
                TotalPage = (int)Math.Ceiling(totalCount / ((double)pageNumber * PageSize)),
                Data =  Doctorresponse 
            };

            if (Doctors == null || !Doctors.Any())
            {
                _logger.LogWarning("No doctors found.");
                return NotFound("No doctors found.");
            }
            var result = GenericResult<PaginatedList<GetDoctorResponse>>.Success(paginatedList);
            return Ok(result);
        }

        //[HttpGet("getById/{id}")]
        //public async Task<IActionResult> GetallDoctorWithID(int id)
        //{
        //    var result = await _context.Doctor.FindAsync(id);

        //    if (result == null)
        //    {
        //        _logger.LogWarning($"Doctor with ID {id} not found.");
        //        return NotFound("No doctors found.");
        //    }

        //    return Ok(_mapper.Map<GetDoctorResponse>(result));
        //}


        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateDoctorRequest request)
        {
            var result = _mapper.Map<Doctor>(request);
           

            await  _context.Doctor.AddAsync(result);
            await _context.SaveChangesAsync();

            return Created(); 
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> update(UpdateDoctorRequest request)
        {

            var result = await _context.Doctor.FindAsync(request.Id);
            if (result == null)
            {
                _logger.LogWarning($"Doctor with ID {request.Id} not found for update.");
                return NotFound("No doctors found.");
            }
         
            result = _mapper.Map(request, result);


                   _context.Doctor.Update(result);
            await _context.SaveChangesAsync();

            return Ok("Doctor updated successfully.");
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var result = await _context.Doctor.FindAsync(id);
            if (result == null)
            {
                _logger.LogWarning($"Doctor with ID {id} not found for deletion.");
                return NotFound("No doctors found.");
            }

                   _context.Doctor.Remove(result);
            await _context.SaveChangesAsync();

            return Ok("Doctor deleted successfully.");
        }


    }
}
