using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Data.Repository.IRepository;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Data.Repository;
using Microsoft.AspNetCore.JsonPatch;
using EmployeeAPI.Dtos.Employee;

namespace EmployeeAPI.Controllers
{
    [Produces( "application/json" )]
    [Route( "api/[Controller]" )]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork,IMapper mapper ) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns></returns>
        //GET api/Employee
        [Authorize(Policy = Policies.Admin)]
        [HttpGet]
        public async Task<ActionResult<List<EmployeeReadDto>>> GetAllEmployees()
        {
            var allEmployees = await _unitOfWork.employee.GetAll();
            return Ok(_mapper.Map<List<EmployeeReadDto>>(allEmployees));
        }

        /// <summary>
        /// Get specific employee by id
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns></returns>
        //GET api/Employee/{id}
        [Authorize(Policy = Policies.Admin)]
        [HttpGet("{id}", Name = "GetEmployeeById")]
        public async Task <ActionResult<EmployeeReadDto>> GetEmployeeById(int id)
        {
            var employee = await _unitOfWork.employee.Get(id);

            if (employee != null)
            {
                return Ok(_mapper.Map<EmployeeReadDto>(employee));
            }

            return NotFound();
        }

        /// <summary>
        /// Add a new employee
        /// </summary>
        /// <param name="employeeCreateDto">Employee data</param>
        /// <returns></returns>
        //POST api/Employee
        [Authorize(Policy = Policies.Admin)]
        [HttpPost]
        public async Task <ActionResult<EmployeeReadDto>> AddEmployee(EmployeeCreateDto employeeCreateDto)
        {
            var employeeModel = _mapper.Map<Employee>(employeeCreateDto);

            _unitOfWork.employee.Create(employeeModel);
            await _unitOfWork.Save();

            var employeeReadDto = _mapper.Map<EmployeeReadDto>(employeeModel);

            return (CreatedAtRoute(nameof(GetEmployeeById), new { id = employeeModel.Id }, employeeReadDto));
        }

        /// <summary>
        /// Update all employee data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employeeUpdateDto">All employee object data</param>
        /// <returns></returns>
        //PUT api/Employee/{id}
        [Authorize(Policy = Policies.Admin)]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployee(int id, EmployeeUpdateDto employeeUpdateDto)
        {
            var employeeModel = _unitOfWork.employee.Get(id).Result;

            if (employeeModel == null)
            {
                return NotFound();
            }

            _mapper.Map(employeeUpdateDto, employeeModel);

            _unitOfWork.employee.Update(employeeModel);
            await _unitOfWork.Save();

            return NoContent();
        }

        /// <summary>
        /// Update specific data of the employee data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jsonPatchDocument">Json patch data contain (op,path, and value)</param>
        /// <returns></returns>
        //PATCH api/Employee/{id}
        [Authorize(Policy = Policies.Admin)]
        [HttpPatch("{id}")]
        public async Task <ActionResult> PatchEmployee(int id, JsonPatchDocument<EmployeeUpdateDto> jsonPatchDocument)
        {

            var employeeModel = _unitOfWork.employee.Get(id).Result;
            if (employeeModel == null)
            {
                return NotFound();
            }

            var employeeDtoToPatch = _mapper.Map<EmployeeUpdateDto>(employeeModel);

            jsonPatchDocument.ApplyTo(employeeDtoToPatch, ModelState);

            if (!TryValidateModel(employeeDtoToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(employeeDtoToPatch, employeeModel);

            _unitOfWork.employee.Update(employeeModel);
           await _unitOfWork.Save();

            return NoContent();
        }

        /// <summary>
        /// Delete employee
        /// </summary>
        /// <param name="id">The employee id</param>
        /// <returns></returns>
        //DELETE api/Employee/{id}
        [Authorize(Policy = Policies.Admin)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var employeeModel = _unitOfWork.employee.Get(id).Result;
            if (employeeModel == null)
            {
                return NotFound();
            }

            _unitOfWork.employee.Delete(employeeModel);
           await _unitOfWork.Save();

            return NoContent();

        }
    }
}
