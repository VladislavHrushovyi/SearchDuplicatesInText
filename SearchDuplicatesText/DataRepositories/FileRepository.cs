using Microsoft.EntityFrameworkCore;
using SearchDuplicatesText.Models;
using SearchDuplicatesText.Models.DataBase;

namespace SearchDuplicatesText.DataRepositories;

public class FileRepository : BaseRepository
{
    public FileRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task AddShingleFile(ShingleFile shingleFile)
    {
        await dbContext.SingleFiles.AddAsync(shingleFile);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<ShingleFile>> GetAllShingleFile()
    {
        return await dbContext.SingleFiles.ToListAsync();
    }

    public async Task AddNgramFile(NgramFile ngramFile)
    {
        await dbContext.NgramFiles.AddAsync(ngramFile);
        await dbContext.SaveChangesAsync();
    }
}