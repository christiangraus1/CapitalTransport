using AutoMapper;
using Github.AutoMapper;
using Github.BusinessLayer;
using Github.BusinessLayer.Entities;
using Github.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IGithubBusinessLayer, GithubBusinessLayer>(client =>
{
    client.BaseAddress = new Uri("https://www.github.com");
});

builder.Services.AddScoped<IGithubBusinessLayer, GithubBusinessLayer>();

var config = new MapperConfiguration(cfg => {

    cfg.AddProfile(new GithubMapping());
});

var mapper = config.CreateMapper();

builder.Services.AddScoped<IMapper>(e => mapper);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var securityConfig = new SecuritySettings();
configuration.GetSection("Security").Bind(securityConfig);

builder.Services.AddSingleton<SecuritySettings>(securityConfig);

var assemblies = AppDomain.CurrentDomain.GetAssemblies();
var bus = assemblies.Where(e => e.FullName?.Contains("Github.BusinessLayer") ?? false).First();
var web = assemblies.Where(e => e.FullName?.Contains("Github.Web") ?? false).First();
builder.Services.AddKeyedSingleton<IMapper>("map", mapper);

builder.Services.AddScoped<IGithubBusinessLayer, GithubBusinessLayer>();

var app = builder.Build();

// Endpoints configuration
app.MapCustomEndpoints();

app.Run();
