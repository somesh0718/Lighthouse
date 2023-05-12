using Microsoft.EntityFrameworkCore;

namespace Igmite.Lighthouse.DAL.EF
{
    /// <summary>
    /// A factory for data services.
    /// </summary>
    public static class ContextFactory
    {
        /// <summary>
        /// Create a new instance of the IgmiteDbContext.
        /// </summary>
        /// <returns>A new instance of the IgmiteDbContext.</returns>
        public static IgmiteDbContext CreateContext()
        {
            return new IgmiteDbContext(new DbContextOptions<IgmiteDbContext>());
        }
    }
}
