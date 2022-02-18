using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Routine.Api.Data;
using Routine.Api.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddControllers(setup =>
{
    //当请求类型和服务器所支持类型不一致时候返回406（例如请求xml 服务器仅支持json）
    setup.ReturnHttpNotAcceptable = true;
    //内容协商，以支持xml格式请求(旧写法)
    //setup.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
    //setup.InputFormatters...


})
    .ConfigureApiBehaviorOptions(setup =>
    {
        //自定义错误报告
        setup.InvalidModelStateResponseFactory = context =>
        {
            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Type = "http://www.baidu.com",
                Title = "错误",
                Status = StatusCodes.Status422UnprocessableEntity,
                Detail = "请看详细信息",
                Instance = context.HttpContext.Request.Path
            };

            problemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);
            return new UnprocessableEntityObjectResult(problemDetails)
            {
                ContentTypes = { "application/problem+json" }
            };
        };
    })
    .AddNewtonsoftJson(setup =>
    {
        setup.SerializerSettings.ContractResolver =
            new CamelCasePropertyNamesContractResolver();
    })
    //添加xml序列化器
    .AddXmlSerializerFormatters()
    .AddXmlDataContractSerializerFormatters();

//注册AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//注册服务
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 注册数据库上下文
builder.Services.AddDbContext<RoutineDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
// 开发环境
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// 生产环境
else
{
    app.UseExceptionHandler(appBuilder =>
    {
        appBuilder.Run(async context =>
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Unexpected Error!");
        });
    });
}

app.UseAuthorization();

app.MapControllers();

//迁移配置
using (var scope = app.Services.CreateScope())
{
    try
    {
        var dbContext = scope.ServiceProvider.GetService<RoutineDbContext>();

        dbContext.Database.EnsureDeleted();
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Database Migration Error!");
    }
}

app.Run();
