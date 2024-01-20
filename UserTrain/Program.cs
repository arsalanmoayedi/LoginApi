using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserTrain.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
///MSSQLSERVER2022
builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Context>(context =>
{
    // context.UseSqlServer("Data source=.;initial catalog=innoland_DB;integrated security=true");
     context.UseSqlServer("Data source=158.58.187.148\\MSSQLSERVER2022;initial catalog=innoland_DB;user Id=innoland_DB;password=Mo@yedi1380");
   // context.UseSqlServer(builder.Configuration["ConnectionStrings:DedaultConnection"]);
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
        };
    }
    );
builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", builder => 
{ builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed((host) => true); }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("CorsPolicy");

app.UseAuthorization();
app.UseEndpoints(Endpoint =>
{
    Endpoint.MapControllers();
});
app.MapControllers();

app.Run();
