using Microsoft.EntityFrameworkCore;
using SearchDuplicatesText.Models;
using SearchDuplicatesText.Models.DataBase;

namespace SearchDuplicatesText.DataRepositories;

public class FileRepository : BaseRepository
{
    public FileRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<ShingleFile> AddShingleFile(ShingleFile shingleFile)
    {
        var shingleFileEntity = await DbContext.SingleFiles.AddAsync(shingleFile);
        await DbContext.SaveChangesAsync();

        return shingleFileEntity.Entity;
    }

    public async Task<List<ShingleFile>> GetAllShingleFile()
    {
        return await DbContext.SingleFiles.ToListAsync();
    }

    public async Task<List<NgramFile>> GetAllNgramFiles()
    {
        return await DbContext.NgramFiles.ToListAsync();
    }

    public async Task<NgramFile> AddNgramFile(NgramFile ngramFile)
    {
        var ngramFileEntity = await DbContext.NgramFiles.AddAsync(ngramFile);
        await DbContext.SaveChangesAsync();
        
        return ngramFileEntity.Entity;
    }

    public async Task<FileResponse> Delete(string nameFile)
    {
        var ngramFileEntity = DbContext.NgramFiles.Where(f => f.Name == nameFile).FirstAsync();
        var shingleFileEntity = DbContext.SingleFiles.Where(f => f.Name == nameFile).FirstAsync();

        var ngramsRemoved = DbContext.NgramFiles.Remove(ngramFileEntity.GetAwaiter().GetResult());
        var shingleRemoved = DbContext.SingleFiles.Remove(shingleFileEntity.GetAwaiter().GetResult());
        await DbContext.SaveChangesAsync();
        
        return new FileResponse(ngramsRemoved.Entity, shingleRemoved.Entity);
    }
    
}