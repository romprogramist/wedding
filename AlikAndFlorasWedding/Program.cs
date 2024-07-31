using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using AlikAndFlorasWedding.Data;
using AlikAndFlorasWedding.Middleware;
using AlikAndFlorasWedding.Services;
using AlikAndFlorasWedding.Services.ApplicationService;
using AlikAndFlorasWedding.Services.CategoryService;
using AlikAndFlorasWedding.Services.EmailService;
using AlikAndFlorasWedding.Services.ProductService;
using AlikAndFlorasWedding.Services.ReviewService;
using AlikAndFlorasWedding.Services.UserService;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => {
    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));
builder.Services.AddControllersWithViews().AddNewtonsoftJson();
builder.Services.AddDbContext<DataContext>();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AuthSettings:Token").Value!))
        };
    });

builder.Services.AddWebOptimizer(pipeline =>
{
    //css bundles
    pipeline.AddCssBundle("/css/layout-bundle.css", 
        "/css/layout.css");
    pipeline.AddCssBundle("/css/home-bundle.css", 
        "/css/home.css");
    pipeline.AddCssBundle("/css/review-bundle.css", 
        "/css/review.css");
    pipeline.AddCssBundle("/css/admin-bundle.css", 
        "/css/admin.css");

    // js bundles
    pipeline.AddJavaScriptBundle("/js/layout-bundle.js", 
        "/js/phone-mask.js",
        "/js/api-request.js",
        "/js/loader.js",
        "/js/modal.js",
        "/js/helpers.js",
        "/js/form-request.js",
        "/js/layout.js");
    pipeline.AddJavaScriptBundle("/js/home-bundle.js",
        "/js/home.js");
    pipeline.AddJavaScriptBundle("/js/user-bundle.js",
        "/js/user.js");
    pipeline.AddJavaScriptBundle("/js/admin-review-bundle.js",
        "/js/admin/review.js");
    pipeline.AddJavaScriptBundle("/js/admin-layout-bundle.js", 
        "/js/phone-mask.js",
        "/js/api-request.js",
        "/js/loader.js",
        "/js/modal.js",
        "/js/helpers.js",
        "/js/form-request.js",
        "/js/admin/layout.js");
    pipeline.AddJavaScriptBundle("/js/admin-product-bundle.js",
        "/js/admin/product.js");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // uncomment for automatic applying migration
    
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    try
    {
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Problem with migration data");
    }
    
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    
    app.UseWebOptimizer();
}

// app.UseHttpsRedirection();
app.UseCors();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "area",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseUtm();

app.Run();