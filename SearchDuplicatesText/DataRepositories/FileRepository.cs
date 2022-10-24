using Microsoft.EntityFrameworkCore;
using SearchDuplicatesText.Models;
using SearchDuplicatesText.Models.DataBase;
using SearchDuplicatesText.Models.Responses;

namespace SearchDuplicatesText.DataRepositories;

public class FileRepository : BaseRepository
{
    public FileRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<T> AddFile<T>(T file) where T : BaseModel
    {
        var newFile = await DbContext.Set<T>().AddAsync(file);
        await DbContext.SaveChangesAsync();

        return newFile.Entity;
    }

    public async Task<IEnumerable<T>> GetFiles<T>() where T : BaseModel
    {
        var files = DbContext.Set<T>();

        return await files.ToListAsync();
    }

    public async Task<FileResponse> Delete(string nameFile)
    {
        var ngramFileEntity = DbContext.NgramFiles.Where(f => f.Name == nameFile).FirstAsync();
        var shingleFileEntity = DbContext.SingleFiles.Where(f => f.Name == nameFile).FirstAsync();

        var ngramsRemoved = DbContext.NgramFiles.Remove(ngramFileEntity.GetAwaiter().GetResult());
        var shingleRemoved = DbContext.SingleFiles.Remove(shingleFileEntity.GetAwaiter().GetResult());
        await DbContext.SaveChangesAsync();
        
        return new FileResponse(ngramsRemoved.Entity, shingleRemoved.Entity, new ExpFile()); // є заглушка
    }
    
}