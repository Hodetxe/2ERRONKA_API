    using _1Erronka_API;
    using _1Erronka_API.Repositorioak;
using System.ComponentModel;
using System.Text.Json.Serialization;




    var builder = WebApplication.CreateBuilder(args);

    var aspnetcoreUrls = Environment.GetEnvironmentVariable("ASPNETCORE_URLS");
    if (string.IsNullOrWhiteSpace(aspnetcoreUrls))
    {
        builder.WebHost.UseUrls("http://0.0.0.0:5000");
    }

    // Add services to the container.

    // CORS konfigurazioa gehitu => Web-etik errorea ez emateko
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
        {
            policy
                .AllowAnyOrigin()     //.WithOrigins("http://localhost:8000") Jakiteko zein IPtatik etorri daitekeen
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    });


    builder.Services.AddControllers();

    
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddSingleton(NHibernateHelper.SessionFactory);
    builder.Services.AddScoped<NHibernate.ISession>(sp => sp.GetRequiredService<NHibernate.ISessionFactory>().GetCurrentSession());
    builder.Services.AddTransient<ErreserbaRepository>();
    builder.Services.AddTransient<ProduktuaRepository>();
    builder.Services.AddTransient<EskariaRepository>();
    builder.Services.AddTransient<EskariaProduktuaRepository>();
    builder.Services.AddTransient<MahaiaRepository>();
    builder.Services.AddTransient<LangileaRepository>();
    builder.Services.AddScoped<OsagaiaRepository>();
    builder.Services.AddScoped<ProduktuaOsagaiaRepository>();
    builder.Services.AddScoped<HornitzaileaRepository>();
    builder.Services.AddScoped<MaterialaRepository>();
    builder.Services.AddScoped<ErosketaRepository>();
    builder.Services.AddScoped<RolaRepository>();






var app = builder.Build();


    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    //app.UseHttpsRedirection();

    app.UseCors();

    app.UseAuthorization();

    app.UseMiddleware<NHibernateSessionMiddleware>();
    app.MapControllers();

    app.Run();


