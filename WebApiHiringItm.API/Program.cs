using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApiHiringItm.CONTEXT.Context;
using WebApiHiringItm.CORE.Core;
using WebApiHiringItm.CORE.Helpers;
using WebApiHiringItm.IOC.Dependencies;
using WebApiHiringItm.MODEL.Mapper;
using WebApiHiringItm.MODEL.Models;
using WebApiRifa.CORE.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
RegisterDependency.RegistrarDependencias(builder.Services);
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
#region RegisterAddCors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("*")
                          .AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});
#endregion
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
        options => builder.Configuration.Bind("AppSettings", options));
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new Automaping());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<Hiring_V1Context>(options =>
      options
      .UseLazyLoadingProxies()
      .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
      .UseSqlServer(builder.Configuration.GetConnectionString("HiringDatabase"))
      );


builder.Services.AddScoped<IHiring_V1Context>(provider => provider.GetService<Hiring_V1Context>());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(x => x
  .AllowAnyOrigin()
  .AllowAnyMethod()
  .AllowAnyHeader());
}
app.UseMiddleware<JwtMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(MyAllowSpecificOrigins);
app.MapControllers();
app.Run();
