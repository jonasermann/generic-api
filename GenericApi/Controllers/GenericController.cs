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
}