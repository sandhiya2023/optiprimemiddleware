using aravindMiddleware.API;
using aravindMiddleware.API.Services;
using aravindMiddleware.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AravindMiddlewareAPIDetails", Version = "v1" });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.SchemaFilter<SampleRequestSchemaFilter>();
});



#pragma warning disable ASP5001 // Type or member is obsolete
builder.Services.AddMvc(option => option.EnableEndpointRouting = false)
#pragma warning disable CS0618 // Type or member is obsolete
    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning restore ASP5001 // Type or member is obsolete
    .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
    .AddSessionStateTempDataProvider();

var secretKey = builder.Configuration.GetValue<string>("KeySettings:Secret");
var key = Encoding.ASCII.GetBytes(secretKey);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// configure DI for application services
builder.Services.AddScoped<IUserServices, UserServices>();

var app = builder.Build();
app.UseDeveloperExceptionPage();
app.UseCors("MyPolicy");
// Configure the HTTP request pipeline.



app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("./v1/swagger.json", "AravindMiddlewareAPIDetails v1"));
app.UseHttpsRedirection();

CommonConstraints.ConnectionString = builder.Configuration.GetConnectionString("mysqlConnection");
CommonConstraints.PasswordEncryptKey = builder.Configuration.GetValue<string>("KeySettings:PasswordEncryptKey");


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

//The below commands are added for Fast Reports implementation
app.UseDefaultFiles();

try
{
    app.UseFastReport();
}
catch
{ }

app.UseStaticFiles();