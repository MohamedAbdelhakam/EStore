
using EStore.Core.AppContexts;
using EStore.Core.Models;
using EStore.Repositories.Interfaces;
using EStore.Repositories.UnitOfWork;
using EStore.Services.Helper.EmailHelper;
using EStore.Services.MailService;

using settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserServices.Cart;
using EStore.Services.UserServices.Cart;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EStore.Services.Authentication;
using AdminCategoryServices;
using EStore.Services.AdminServices.RepositoryServices.Products;
using EStore.Services.AdminServices.RepositoryServices.Order;
using UserServices.Orders;
using EStore.Services.UserServices.Orders;

namespace EStore
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

            builder.Services.AddIdentity<ApplicationUser,ApplicationRole>()
                .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("Cs"))
            );

            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("EmailSettings"));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IAuthenticationUserServices, AuthenticationUserServices>();

            builder.Services.AddTransient<IMailService, MailService>();

            builder.Services.AddTransient<IEmailHelper, EmailHelper>();
            builder.Services.AddScoped<ICartSercvice, CartService>();
            builder.Services.AddScoped<ICategoryServices, CategoryServices>();
            builder.Services.AddScoped<IProductServices, ProductServices>();
            builder.Services.AddScoped<ICartSercvice, CartService>();
            builder.Services.AddScoped<IOrderAdminService, OrderAdminService>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            builder.Services.AddAuthentication(
                 opt => {
                     opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                     opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    }
                ).AddJwtBearer(options => 
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = builder.Configuration["Jwt:Iss"],
                        ValidAudience = builder.Configuration["Jwt:Aud"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });
            builder.Services.AddAuthorization();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CORSPOLICY",
                    policy =>
                    {
                        policy.AllowAnyOrigin().
                        AllowAnyHeader().
                        AllowAnyMethod();
                    });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("CORSPOLICY");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
