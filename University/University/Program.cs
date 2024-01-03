using Microsoft.EntityFrameworkCore;
using University.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DB_CONN")));

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();

builder.Services.AddMvc()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

