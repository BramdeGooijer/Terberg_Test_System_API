using ErrorOr;
using TestDataApi.Models;

namespace TestDataApi.Services.Reports;

public interface IReportService
{
    ErrorOr<Created> CreateReport(Report report);
    ErrorOr<Report> GetReport(Guid id);
    List<Report> GetAllReports();
}