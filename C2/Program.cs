using C2.Infrastructure.Connexions;
using C2.Infrastructure.DAO;
using C2.Domain.IDAO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); // Use your connection string name

builder.Services.AddScoped<ConnectionDB>(provider =>
    ConnectionDB.GetInstance(connectionString)); // Changed to Scoped

builder.Services.AddScoped<IProprietaireDAO, ProprietaireDAOImpl>();
builder.Services.AddScoped<ILaveriesDAO, LaveriesDAOImpl>();
builder.Services.AddScoped<IMachineDAO, MachineDAOImpl>();
builder.Services.AddScoped<ICycleDAO, CycleDAOImpl>();

// Add and configure CORS


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Apply the CORS policy
app.UseCors("AllowAllOrigins"); // Apply the "AllowAllOrigins" policy

app.UseAuthorization();
app.MapControllers();
app.Run();
