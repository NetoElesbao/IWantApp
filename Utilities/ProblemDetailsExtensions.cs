




using System.Diagnostics.Contracts;
using Flunt.Notifications;
using IWantApp.Models.Products;
using Microsoft.AspNetCore.Identity;

namespace IWantApp.Utilities
{
    public static class ProblemDetailsExtensions
    {
        public static Dictionary<string, string[]> ConvertToProblemDetails(this IReadOnlyCollection<Notification> notifications)
        {
            return notifications.GroupBy(e => e.Key).ToDictionary(e => e.Key, e => e.Select(e => e.Message).ToArray());
        }
        public static Dictionary<string, string[]> ConvertToProblemDetails(this IEnumerable<IdentityError> errors)
        {
            return new Dictionary<string, string[]> { { "EmployeeErrors", errors.Select(e => e.Description).ToArray() } };
        }


    }
}