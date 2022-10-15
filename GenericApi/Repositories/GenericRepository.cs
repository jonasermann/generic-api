using Microsoft.EntityFrameworkCore;
using GenericApi.Data;
using GenericApi.Models;

namespace GenericApi.Repositories;

public class GenericRepository : IGenericRepository
{
    private GenericAppContext _context;

    public GenericRepository(GenericAppContext context)
    {
        _context = context;
    }

    public static DTO ConvertToDTO<T, DTO>(T model) where T : class where DTO : class
    {
        var dto = Activator.CreateInstance<DTO>();

        var modelProperties = model.GetType().GetProperties();
        var dtoProperties = dto.GetType().GetProperties();

        foreach (var dtoProperty in dtoProperties)
        {
            foreach (var modelProperty in modelProperties)
            {
                if (dtoProperty.Name == modelProperty.Name)
                {
                    dtoProperty.SetValue(dto, modelProperty.GetValue(model));
                }
            }
        }

        return dto;
    }

    public static T CreateModel<T, CreateDTO>(CreateDTO createDTO)
    {
        var model = Activator.CreateInstance<T>();

        var modelProperties = model.GetType().GetProperties();
        var createDTOProperties = createDTO.GetType().GetProperties();

        foreach (var createDTOProperty in createDTOProperties)
        {
            foreach (var modelProperty in modelProperties)
            {
                if (createDTOProperty.Name == modelProperty.Name)
                {
                    modelProperty.SetValue(model, createDTOProperty.GetValue(createDTO));
                }
            }
        }

        return model;
    }

    public async Task<List<DTO>> Get<T, DTO>() where T : class where DTO : class
    {
        var dbSet = _context.Set<T>();
        return await dbSet.Select(m => ConvertToDTO<T, DTO>(m)).ToListAsync();
    }

    public async Task Add<T, CreateDTO>(CreateDTO createDTO) where T : class where CreateDTO : class
    {
        var dbSet = _context.Set<T>();
        var model = CreateModel<T, CreateDTO>(createDTO);
        dbSet.Add(model);
        await _context.SaveChangesAsync();
    }
}
