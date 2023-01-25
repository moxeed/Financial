using Financial.Common;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Finanacial.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assebmlies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => (a.FullName ?? "").StartsWith(nameof(Financial)));

            var entities = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(T => typeof(Entity)
                .IsAssignableFrom(T) && T.IsClass && !T.IsAbstract);

            foreach (var entity in entities)
            {
                modelBuilder.Entity(entity).ToTable(entity.Name);
            }

            modelBuilder.Ignore<Entity>();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
