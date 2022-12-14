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

    [HttpPost("Persons")]
    public async Task AddPerson(PersonPDTO personPDTO) => await _repo.Add<Person, PersonPDTO>(personPDTO);

    [HttpPut("Persons/{id}")]
    public async Task PutPerson(int id, PersonPDTO personPDTO) => await _repo.Put<Person, PersonPDTO>(id, personPDTO);

    [HttpDelete("Persons/{id}")]
    public async Task DeletePerson(int id) => await _repo.Delete<Person>(id);



    [HttpGet("Jobs")]
    public async Task<List<JobDTO>> GetJobs() => await _repo.Get<Job, JobDTO>();

    [HttpPost("Jobs")]
    public async Task AddJob(JobPDTO jobPDTO) => await _repo.Add<Job, JobPDTO>(jobPDTO);

    [HttpPut("Jobs/{id}")]
    public async Task PutJob(int id, JobPDTO jobPDTO) => await _repo.Put<Job, JobPDTO>(id, jobPDTO);

    [HttpDelete("Jobs/{id}")]
    public async Task DeleteJob(int id) => await _repo.Delete<Job>(id);
}