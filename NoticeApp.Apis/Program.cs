using Microsoft.EntityFrameworkCore;
using NoticeApp.Models;

namespace NoticeApp.Apis
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            #region CORS
            // [CORS][1] CORS 사용 등록

            // [CORS][1][1] 기본: 모두 허용
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            // [CORS][1][2] 참고: 모두 허용
            builder.Services.AddCors(o => o.AddPolicy("AllowAllpolicy", options =>
            {
                options.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            // [CORS][1][3] 참고: 특정 도메인만 허용
            builder.Services.AddCors(o => o.AddPolicy("AllowSpecific", options =>
            {
                options.WithOrigins("https://localhost:44356")
                .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE")
                .WithHeaders("accept", "content-type", "origin", "X-TotalRecordCount");
            }));

            #endregion

            // NoticeAppDbContext.cs Inject: New DbContext Add
            builder.Services.AddEntityFrameworkInMemoryDatabase().AddDbContext<NoticeAppDbContext>(options => options.UseSqlServer(@"Server=192.168.45.73,1433;Database=ArticleApp;User Id=sa2;Password=wegg2650;"));

            // 공지사항(NoticeApp) 관련 의존성(종속성) 주입 관련 코드만 따로 모아서 관리
            // INoticeRepositoryAsync.cs Inject: DI Container에 서비스(리포지토리) 등록
            builder.Services.AddTransient<INoticeRepositoryAsync, NoticeRepositoryAsync>();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // [CORS][2] 사용 등록
            app.UseCors("AllowAnyOrigin");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }


    }
}
