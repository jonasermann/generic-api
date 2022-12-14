using GenericApi.Models;

namespace GenericApi.Repositories;

public interface IGenericRepository
{
    Task<List<DTO>> Get<T, DTO>() where T : class where DTO : class;

    Task Add<T, PDTO>(PDTO pdto) where T : class where PDTO : class;

    Task Put<T, PDTO>(int id, PDTO pdto) where T : class where PDTO : class;

    Task Delete<T>(int id) where T : class;
}
