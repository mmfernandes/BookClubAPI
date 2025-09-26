var builder = WebApplication.CreateBuilder(args);

// Adiciona suporte a controllers
builder.Services.AddControllers();

// Adiciona Swagger (documentação da API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Usa Swagger em ambiente de desenvolvimento
app.UseSwagger();
app.UseSwaggerUI();

// Aceita requisições HTTP
app.UseHttpsRedirection();

app.UseAuthorization();

// Mapeia os controllers
app.MapControllers();

app.Run();
