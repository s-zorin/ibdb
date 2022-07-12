using Microsoft.AspNetCore.Builder;

namespace Ibdb.Shared
{
    public static class ApplicaionBuilderExtensions
    {
        public static void UseSharedExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler("/errors/500");
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
        }
    }
}
