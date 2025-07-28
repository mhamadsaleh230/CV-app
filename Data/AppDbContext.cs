using Microsoft.EntityFrameworkCore;

namespace Hw5forExam.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<CvData> cvs { get; set; } = null!;
}
