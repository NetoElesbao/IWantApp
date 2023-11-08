



namespace IWantApp.Services
{
    public static class ProblemDetailsExtensions
    {
        public static Dictionary<string, string[]> ConvertToProblemDetails(this IReadOnlyCollection<Notification> notifications)
        {
            return notifications.GroupBy(e => e.Key).ToDictionary(e => e.Key, e => e.Select(e => e.Message).ToArray());
        }

        public static Dictionary<string, string[]> ConvertToProblemDetails(this IEnumerable<IdentityError> identityErrors)
        {
            var dictionary = new Dictionary<string, string[]>() 
            { { "EmployeeeErrors", identityErrors.Select(e => e.Description).ToArray() } };

            return dictionary;
        }

       

    }
}