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

    public static T PModel<T, PDTO>(PDTO pdto)
    {
        var model = Activator.CreateInstance<T>();

        var modelProperties = model.GetType().GetProperties();
        var PDTOProperties = pdto.GetType().GetProperties();

        foreach (var PDTOProperty in PDTOProperties)
        {
            foreach (var modelProperty in modelProperties)
            {
                if (PDTOProperty.Name == modelProperty.Name)
                {
                    modelProperty.SetValue(model, PDTOProperty.GetValue(pdto));
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

    public async Task Add<T, PDTO>(PDTO pdto) where T : class where PDTO : class
    {
        var dbSet = _context.Set<T>();
        var model = PModel<T, PDTO>(pdto);
        dbSet.Add(model);
        await _context.SaveChangesAsync();
    }

    public async Task Put<T, PDTO>(int id, PDTO pdto) where T : class where PDTO : class
    {
        var dbSet = _context.Set<T>();
        var model = PModel<T, PDTO>(pdto);
        model.GetType().GetProperty("Id").SetValue(model, id);
        dbSet.Update(model);
        await _context.SaveChangesAsync();
    }

    public async Task Delete<T>(int id) where T : class
    {
        var dbSet = _context.Set<T>();
        var models = await dbSet.ToListAsync();
        var model = models.FirstOrDefault(m => (int) m.GetType().GetProperty("Id").GetValue(m) == id);
        dbSet.Remove(model);
        await _context.SaveChangesAsync();
    }
}
