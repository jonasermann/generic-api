using GenericApi.Models;

namespace GenericApi.Repositories;

public interface IGenericRepository
{
    Task<List<DTO>> Get<T, DTO>() where T : class where DTO : class;

    Task Add<T, PDTO>(PDTO pdto) where T : class where PDTO : class;
}
