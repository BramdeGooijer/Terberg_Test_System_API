using ErrorOr;

namespace TestDataApi.ServiceErrors;

public static class Errors
{
    public static class Report
    {
        public static Error NotFound => Error.NotFound(
            "Report.NotFound",
            "Report not found"
        );
    }
}