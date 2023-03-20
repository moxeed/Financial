using Finanacial.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration.GetConnectionString("Default"));

var app = builder.Build();

var dbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
dbContext.Database.EnsureDeleted();
dbContext.Database.EnsureCreated();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();