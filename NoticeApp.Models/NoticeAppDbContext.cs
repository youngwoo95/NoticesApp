using Microsoft.EntityFrameworkCore;

namespace NoticeApp.Models
{
    /// <summary>
    /// [5] DbContext Class
    /// </summary>
    public class NoticeAppDbContext : DbContext
    {
        public NoticeAppDbContext()
        {
            // Empty
        }

        public NoticeAppDbContext(DbContextOptions<NoticeAppDbContext> options) : base(options)
        {
            // 공식과 같은 코드
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // 닷넷 프레임워크 기반에서 호출되는 코드 영역 : 
            // App.Config 또는 Web.Config의 연결 문자열 사용
            /*
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                optionsBuilder.UseSqlServer(connectionString);
            }
            */

            optionsBuilder.UseSqlServer(@"Server=192.168.45.73,1433;Database=NoticeApp;User Id=sa2;Password=wegg2650;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //[A] Articles 테이블의 Created 열은 자동으로 GetDate() 제약 조건을 부여하기
            modelBuilder.Entity<Notice>().Property(m => m.Created).HasDefaultValueSql("GetDate()");
        }

        public DbSet<Notice> Notices { get; set; }

    }
}
