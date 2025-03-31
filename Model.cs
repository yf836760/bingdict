//dotnet tool install --global dotnet-ef  安装EF Core命令行工具
//dotnet ef migrations add InitialCreate  创建迁移
//dotnet ef database update               应用迁移以创建数据库
//dotnet publish -r linux-x64 --self-contained true -p:PublishSingleFile=true -c Release 发布
using Microsoft.EntityFrameworkCore;

namespace Trans.Model;

public class WordContext : DbContext
{
    private readonly string _connectionString;

    public WordContext(string connectionString = null)
    {
        _connectionString = connectionString ?? "Host=localhost;Port=5432;Database=worddb;Username=user;Password=password";
    }
    public DbSet<Word> Words { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }
}

public class Word
    {
        public int WordId { get; set; }
        public required string PreTrans { get; set; }
        public required string Transed { get; set; }
        public int Frequency { get; set; }
    }