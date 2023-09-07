using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MedicineRest.Models;
using MedicineRest.Models.Db;
using static MedicineRest.Models.securityservic;
//using MedicineRest.Models;
//using static MedicineRest.Models.securityservic;
//using static WebRestApp.Models.SecurityService;



namespace WebRestApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            // Add services to the container.



            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(conf =>
            {
                conf.AddPolicy("policy1", pol => {



                    pol.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
                });
            });



            //Adding authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:audience"],
                    ValidIssuer = builder.Configuration["Jwt:issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]!))
                };
            });

            builder.Services.AddAuthorization(config => {
                config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
                /*config.AddPolicy(Policies.Manager, Policies.ManagerPolicy());
                config.AddPolicy(Policies.Executive, Policies.ExecutivePolicy());*/
            });



            string connectionstring = "Server=localhost;database=MedicineDb;trusted_connection=yes;trustservercertificate=true";
            builder.Services.AddDbContext<ProjectDbContext>(config => config.UseSqlServer(connectionstring));





            builder.Services.AddTransient(typeof(medicineservic));
            builder.Services.AddTransient(typeof(securityservic));
            builder.Services.AddTransient(typeof(dataservice));



            var app = builder.Build();



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();









            app.UseCors("policy1");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();





            app.MapControllers();



            app.Run();
        }
    }
}