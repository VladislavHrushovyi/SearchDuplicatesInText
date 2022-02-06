using Microsoft.EntityFrameworkCore;
using SearchDuplicatesText.Models.DataBase;

namespace SearchDuplicatesText.DataRepositories;

public class FileRepository : BaseRepository
{
    public FileRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task AddShingleFile(ShingleFile shingleFile)
    {
        await DbContext.SingleFiles.AddAsync(shingleFile);
        await DbContext.SaveChangesAsync();
    }

    public async Task<List<ShingleFile>> GetAllShingleFile()
    {
        return await DbContext.SingleFiles.ToListAsync();
    }

    public async Task<List<NgramFile>> GetAllNgramFiles()
    {
        return await DbContext.NgramFiles.ToListAsync();
    }

    public async Task AddNgramFile(NgramFile ngramFile)
    {
        await DbContext.NgramFiles.AddAsync(ngramFile);
        await DbContext.SaveChangesAsync();
    }
}