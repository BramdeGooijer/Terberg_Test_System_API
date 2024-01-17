using ErrorOr;
using Microsoft.EntityFrameworkCore;
using TestDataApi.Models;
using TestDataApi.Persistence;
using TestDataApi.ServiceErrors;

namespace TestDataApi.Services.Reports;

public class ReportService : IReportService
{
    private readonly ApplicationDbContext _dbContext;

    public ReportService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ErrorOr<Created> CreateReport(Report report)
    {
        _dbContext.Add(report);
        _dbContext.SaveChanges();
        
        return Result.Created;
    }

    public ErrorOr<Report> GetReport(Guid id)
    {
        if (_dbContext.Reports.Include(r => r.Tests).FirstOrDefault(r => r.Id == id) is Report report)
        {
            return report;
        }

        return Errors.Report.NotFound;
    }

    public List<Report> GetAllReports()
    {
        return _dbContext.Reports.Include(r => r.Tests).ToList();
    }
}