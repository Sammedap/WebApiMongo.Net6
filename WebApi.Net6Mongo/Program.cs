using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UserManagerService.UserManagerService;
using UserModel.UserModel;
using WebApiDemo.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<UserStoreDatabaseSettings>(
    builder.Configuration.GetSection(nameof(UserStoreDatabaseSettings)));

builder.Services.AddSingleton<IUserStoreDatabaseSettings>(sp =>
sp.GetRequiredService<IOptions<UserStoreDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
   new MongoClient(builder.Configuration.GetValue<string>("UserStoreDatabaseSettings:ConnectionString")));

//builder.Services.AddScoped<IUserService<IDocument>, UserService<IDocument>>();
builder.Services.AddScoped(typeof(IUserService<>), typeof(UserService<>));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.AddLog4Net(new Log4NetProviderOptions("log4net.config"));

var app = builder.Build();

app.UseMiddleware(typeof(ExceptionHandlingMiddleware));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    //Displays sensitive information
    //app.UseDeveloperExceptionPage();

    app.UseExceptionHandler("/error");
}
else
{
    //to catch any type of unhandled exception application-wide.
    //app.UseExceptionHandler("Error/Controller");

}
//app.Run(context => { throw new Exception("error"); });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
