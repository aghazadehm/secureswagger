using SecureSwagger.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.Configure<CookiePolicyOptions>(options =>
{
  options.CheckConsentNeeded = context => true;
  options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.AddAuthentication(options =>
{
   options.DefaultScheme = "Cookies";
   options.DefaultChallengeScheme = "oidc";
})
.AddCookie("Cookies")
.AddOpenIdConnect("oidc", options =>
{
  options.SignInScheme = "Cookies";
  options.Authority = builder.Configuration["Service:SSO:Endpoint"];
  options.ClientId = "InformingApi"; // builder.Configuration["jwt:Audience"];
  options.ResponseType = "code";
  options.Prompt = "login";
  options.GetClaimsFromUserInfoEndpoint = true;
  options.SaveTokens = true;
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "SecureSwagger", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseAuthentication();
app.UseSwaggerAuthorized();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SecureSwagger v1"));
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
