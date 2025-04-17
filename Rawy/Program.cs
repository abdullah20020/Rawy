using core.Models;
using core.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Rawy.Extensions;
using Rawy.Helpers;
using Repsotiry.Data;
using Repsotiry.GenaricReposiory;
using Repsotiry.seeds;



namespace Rawy
{
    public class Program
    {

        //public static async Task Main(string[] args)
        //{
        //    var builder = WebApplication.CreateBuilder(args);

        //    // Add services to the container.
        //    builder.Services.AddControllers();

        //    // Configure Swagger for development environment
        //    builder.Services.AddEndpointsApiExplorer();
        //    builder.Services.AddSwaggerGen();

        //    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        //    // Register the RawyDbContext with the connection string
        //    builder.Services.AddDbContext<RawyDbcontext>(options =>
        //        options.UseSqlServer(connectionString));

        //    // Add Identity configuration using the custom BaseUser class
        //    builder.Services.AddIdentity<BaseUser, IdentityRole<Guid>>(options =>
        //    {
        //        // Add any Identity options you need
        //    })
        //    .AddEntityFrameworkStores<RawyDbcontext>()
        //    .AddDefaultTokenProviders();

        //    // Add other services
        //    builder.Services.AddIdentityServices(builder.Configuration);
        //    builder.Services.AddAutoMapper(typeof(MappingProfile));
        //    builder.Services.AddScoped(typeof(IGenaricrepostry<>), typeof(GenaricRepostry<>));
        //    builder.Services.AddScoped(typeof(IGenaricReposteryUSers<>), typeof(GenaricRepostryusers<>));

        //    var app = builder.Build();

        //    // Configure the HTTP request pipeline
        //    if (app.Environment.IsDevelopment())
        //    {
        //        app.UseSwagger();
        //        app.UseSwaggerUI();
        //    }

        //    app.UseHttpsRedirection();
        //    app.UseAuthorization();
        //    app.UseStaticFiles();
        //    app.MapControllers();
        //    app.Run();

        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();  
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            //var IDentyconnectionString = builder.Configuration.GetConnectionString("IdintetyConnection");
            // //Register the DbContext with the connection string

            // builder.Services.AddDbContext<IdentityDbContexts>(options =>
            //  options.UseSqlServer(IDentyconnectionString));

            builder.Services.AddDbContext<RawyDbcontext>(options =>
                options.UseSqlServer(connectionString));



            builder.Services.AddIdentityServices(builder.Configuration);


            //   builder.Services.AddIdentityServices(builder.Configuration);

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddScoped(typeof(IGenaricrepostry<>), typeof(GenaricRepostry<>));
            builder.Services.AddScoped(typeof(IGenaricReposteryUSers<>), typeof(GenaricRepostryusers<>));



            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<BaseUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await AppDbInitializer.SeedAdminAsync(userManager, roleManager);
            }

            //using var scope = app.Services.CreateScope();
            //var services = scope.ServiceProvider;
            //var _dbcontext=services.GetRequiredService<RawyDbcontext>();
            //var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            //try
            //{ 
            // await _dbcontext.Database.GetAppliedMigrationsAsync();
            //}
            //catch (Exception ex)
            //{
            //    var logger = loggerFactory.CreateLogger<Program>();
            //    logger.LogError(ex, "an error has been occured during apply the migration");
            //}




            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();

            //var builder = WebApplication.CreateBuilder(args);

            //// إضافة الخدمات اللازمة
            //builder.Services.AddControllers();

            //// إعدادات Swagger
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen(c =>
            //{
            //    // إعداد التوثيق باستخدام JWT في واجهة Swagger
            //    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            //    {
            //        In = ParameterLocation.Header,
            //        Description = "Please enter your JWT token with Bearer prefix",
            //        Name = "Authorization",
            //        Type = SecuritySchemeType.ApiKey
            //    });

            //    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            //{
            //    {
            //        new OpenApiSecurityScheme
            //        {
            //            Reference = new OpenApiReference
            //            {
            //                Type = ReferenceType.SecurityScheme,
            //                Id = "Bearer"
            //            }
            //        },
            //        new string[] { }
            //    }
            //});
            //});

            //// الاتصال بقاعدة البيانات
            //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            //builder.Services.AddDbContext<RawyDbcontext>(options =>
            //    options.UseSqlServer(connectionString));

            //// إعدادات الـ Identity (تضمين التوثيق باستخدام JWT)
            //builder.Services.AddIdentityServices(builder.Configuration);

            //// إعدادات الـ AutoMapper (لربط الـ DTOs بالـ Models)
            //builder.Services.AddAutoMapper(typeof(MappingProfile));

            //// إعدادات الـ Repositories
            //builder.Services.AddScoped(typeof(IGenaricrepostry<>), typeof(GenaricRepostry<>));
            //builder.Services.AddScoped(typeof(IGenaricReposteryUSers<>), typeof(GenaricRepostryusers<>));

            //var app = builder.Build();

            //// تفعيل Swagger في بيئة التطوير
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}


            //app.UseHttpsRedirection();

            //app.UseAuthentication();
            //app.UseAuthorization();

            //app.MapControllers();
            //app.Run();
        }
    }
}