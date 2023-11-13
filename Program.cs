using tapr_2023_equipe1_historicoaluno_dotnet.Models.Context;
using tapr_2023_equipe1_historicoaluno_dotnet.Services;
using tapr_2023_equipe1_historicoaluno_dotnet.Services.DiplomaServ;




var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RepositoryDbContext>();


builder.Services.AddScoped<IHistoricoService, HistoricoService>();
builder.Services.AddScoped<IDiplomaService, DiplomaService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
