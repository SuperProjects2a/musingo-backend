using Microsoft.EntityFrameworkCore;
using musingo_backend.Data;
using musingo_backend.Models;

namespace musingo_backend.Repositories;


public interface IReportRepository
{
    public Task<Report?> AddReport(Report report);
    public Task<Report?> UpdateReport(Report report);
    public Task<Report?> GetReportById(int id);
    public Task<bool> IsReportedByUser(int userId,int offerId);
    public Task<ICollection<Report>> GetReportsByOffer(int offerId);
}

public class ReportRepository: Repository<Report>, IReportRepository
{
    public ReportRepository(RepositoryContext context) : base(context)
    {
    }

    public async Task<Report?> AddReport(Report report)
    {
        return await AddAsync(report);
    }

    public async Task<Report?> UpdateReport(Report report)
    {
        return await UpdateAsync(report);
    }

    public async Task<Report?> GetReportById(int id)
    {
        return await GetAll().Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<bool> IsReportedByUser(int userId, int offerId)
    {
        return await GetAll()
            .Include(x => x.Offer)
            .Include(x => x.Reporter)
            .AnyAsync(x => x.Offer.Id == offerId && x.Reporter.Id == userId);
    }

    public async Task<ICollection<Report>> GetReportsByOffer(int offerId)
    {
        return await GetAll()
            .Include(x => x.Offer)
            .Include(x=>x.Reporter)
            .Where(x => x.Offer.Id == offerId)
            .ToListAsync();
    }
}