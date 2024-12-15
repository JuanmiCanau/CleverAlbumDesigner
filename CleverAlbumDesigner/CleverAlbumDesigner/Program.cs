using Amazon.S3;
using Amazon.SecretsManager;
using CleverAlbumDesigner.Data;
using CleverAlbumDesigner.Managers.Interfaces;
using CleverAlbumDesigner.Managers;
using CleverAlbumDesigner.Repositories.Interfaces;
using CleverAlbumDesigner.Repositories;
using CleverAlbumDesigner.Services;
using CleverAlbumDesigner.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ISecretsManagerService>(new SecretsManagerService(new AmazonSecretsManagerClient(Amazon.RegionEndpoint.EUNorth1), "CleverAlbumConnectionString"));
builder.Services.AddSingleton<IAmazonS3>(provider =>
{    
    return new AmazonS3Client(Amazon.RegionEndpoint.EUNorth1);
});

builder.Services.AddSingleton<IStorageService, S3Service>(provider =>
{    
    var s3Client = provider.GetRequiredService<IAmazonS3>();
    var bucketName = "clever-album-bucket";
    return new S3Service(s3Client, bucketName);
});

builder.Services.AddScoped<IPhotoManager, PhotoManager>();
builder.Services.AddScoped<IAlbumManager, AlbumManager>();
builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();
builder.Services.AddScoped<IThemeRepository, ThemeRepository>();
builder.Services.AddScoped<IColorRepository, ColorRepository>();
builder.Services.AddScoped<IThemeColorRepository, ThemeColorRepository>();
builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Calling AWS Secrets Manager in order to obtain the connection string
var secretsManagerService = new SecretsManagerService(new AmazonSecretsManagerClient(Amazon.RegionEndpoint.EUNorth1), "CleverAlbumConnectionString");
var connectionString = await secretsManagerService.GetConnectionString();

//Adding DBContext using the connection string we just retrieved from aws secrets manager
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();
app.UseDefaultFiles(); 
app.UseStaticFiles();
app.MapControllers();

app.Run();
