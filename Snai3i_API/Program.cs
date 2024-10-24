
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Snai3i_BLL.Automapping;
using Snai3i_BLL.Manager.Acountmanager;
using Snai3i_BLL.Manager.CraftsManager;
using Snai3i_BLL.Manager.OrdersManager;
using Snai3i_BLL.Manager.ReviewsManager;
using Snai3i_BLL.Manager.ToolsManager;
using Snai3i_BLL.Manager.SizesManager;
using Snai3i_BLL.Manager.SalessManager;
using Snai3i_BLL.Manager.UserManager;
using Snai3i_BLL.Manager.WorkerManager;
using Snai3i_BLL.Manager.AdminstrationManage;
using Snai3i_DAL.Data.Repository.BasketRepositry;
using Snai3i_BLL.Manager.CardManager;
using Snai3i_DAL.Data.Repository.CardRepository;
using Snai3i_DAL.Data.Context;
using Snai3i_DAL.Data.Models;
using Snai3i_DAL.Data.Repository.CraftsRepository;
using Snai3i_DAL.Data.Repository.GenericRepository;
using Snai3i_DAL.Data.Repository.OrdersRepository;
using Snai3i_DAL.Data.Repository.ReviewsRepository;
using Snai3i_DAL.Data.Repository.SizesRepository;
using Snai3i_DAL.Data.Repository.SalesRepository;
using Snai3i_DAL.Data.Repository.ToolsRepository;
using Snai3i_DAL.Data.Repository.UnitOfWork;
using Snai3i_DAL.Data.Repository.UserWorkerTransactionUnitOfWork;
using Snai3i_DAL.Data.Repository.UserReop;
using Snai3i_DAL.Data.Repository.WorkerRepo;
using Snai3i_DAL.Data.service.FileService;
using Snai3i_DAL.Data.service.MapService;
using System.Text;
using StackExchange.Redis;
using Microsoft.Extensions.FileProviders;
using Snai3i_DAL.Data.service.ExceptionMiddleware;


namespace Snai3i_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            // register SnaiiDBContext 
            builder.Services.AddDbContext<SnaiiDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("CS"));
            }
                );
            builder.Services.AddSingleton<IConnectionMultiplexer>(S =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });
            // register identity user // you better write identityrole with the way shown below 

            builder.Services.AddIdentity<ApplicationUser, Microsoft.AspNetCore.Identity.IdentityRole>(
                   options =>
                   {
                       options.Password.RequireNonAlphanumeric = false;
                       options.Password.RequireDigit = false;
                       options.Password.RequireLowercase = false;
                       options.Password.RequireUppercase = false;
                       options.Password.RequiredLength = 1;
                   }
               ).
               AddEntityFrameworkStores<SnaiiDBContext>();


            // register dbcontextaccessor
            builder.Services.AddHttpContextAccessor();

            // register authorization 
            // be careful to add authentication before adding identity itself 
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JWT";
                options.DefaultChallengeScheme = "JWT";

            }
            ).AddJwtBearer("JWT", options =>
            {
                var secretkeyString = builder.Configuration.GetValue<string>("SK");
                var securityKeyBytes = Encoding.ASCII.GetBytes(secretkeyString);
                SecurityKey securitykey = new SymmetricSecurityKey(securityKeyBytes);

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = securitykey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };

            });

            // register genericrepositroy 
            builder.Services.AddScoped( typeof(IGenericRepository<>) , typeof(GenericRepository<>) );
            // to deduce type of entity in geenric repositroy when injected in cotroller 

            // register of sales repository 
            builder.Services.AddScoped<IsalesRepository, SalesRepositoryy>();
            builder.Services.AddScoped<IReviewsRepository, ReviewsRepositoryy>();
            builder.Services.AddScoped<IOrdersRepository, OrdersRepositoryy>();
            builder.Services.AddScoped<ICraftRepo, CraftRepo>();
            builder.Services.AddScoped<IToolRepository, ToolRepository>();
            builder.Services.AddScoped<IUserWorkerTransactionUnitOfWork, UserWorkerTransactionUnitOfWork>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ISizeRepository, SizeRepository>();
            builder.Services.AddScoped<IworkerRepository, WorkerRepository>();
            builder.Services.AddScoped<IuserRepository, UserRepository>();
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddScoped<ICartRebo, CartRebo>();

            builder.Services.AddScoped<Ifileservice, FileService>();
            builder.Services.AddScoped<IMapService, MapService>();
            builder.Services.AddScoped<ICardManager, CardManager>();

            builder.Services.AddScoped<ISalesManager, SalesManager>();   
            builder.Services.AddScoped<IworkerManager, workermanager>();
            builder.Services.AddScoped<IReviewManager, ReviewManager>();
            builder.Services.AddScoped<IToolManager, ToolManager>();
            builder.Services.AddScoped<IOrderManager, OrderManager>();
            builder.Services.AddScoped<ICraftManger, CraftManger>();
            builder.Services.AddScoped<ISizeManager, SizeManager>();
            // register acountmanager 
            builder.Services.AddScoped<IAdministrationManager, AdministrationManager>();
            builder.Services.AddScoped<IAccountManager, AccountManager>();
            builder.Services.AddScoped<IuserManager, userManager>();

            
           
           
            builder.Services.AddHttpClient();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithOrigins("https://localhost:7170") // MVC Client URL
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials(); // SignalR requires credentials for WebSockets
                });
            });


            // register automapper 

            builder.Services.AddAutoMapper(map => map.AddProfile(new MapperProfile()));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseCors("CorsPolicy");
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
           Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
                RequestPath = "/Uploads"
            });
            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionMiddleware>();


            app.UseRouting();


            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
