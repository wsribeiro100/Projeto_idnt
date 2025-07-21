using IDNT.TravelRoutes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTravelRouteModule();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "redis:6379";
    options.InstanceName = "TravelRoutes:";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
   //Deixei fora intensionalmente para ter acesso ao Swagger
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
