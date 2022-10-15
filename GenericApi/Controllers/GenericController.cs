using Microsoft.AspNetCore.Mvc;
using GenericApi.Models;
using GenericApi.Repositories;

namespace GenericApi.Controllers;

[ApiController]
public class GenericController : ControllerBase
{
    private IGenericRepository _repo;
    public GenericController(IGenericRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("Persons")]
    public async Task<List<PersonDTO>> GetPersons() => await _repo.Get<Person, PersonDTO>();

    [HttpGet("Jobs")]
    public async Task<List<JobDTO>> GetJobs() => await _repo.Get<Job, JobDTO>();

    [HttpPost("Persons")]
    public async Task AddPerson(PersonCreateDTO personCreateDTO) => await _repo.Add<Person, PersonCreateDTO>(personCreateDTO);

    [HttpPost("Jobs")]
    public async Task AddJob(JobCreateDTO jobCreateDTO) => await _repo.Add<Job, JobCreateDTO>(jobCreateDTO);
}