using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using TicketSystem.Api.Data;
using TicketSystem.Api.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddControllers(setup =>
{
    //当请求类型和服务器所支持类型不一致时候返回406（例如请求xml 服务器仅支持json）
    setup.ReturnHttpNotAcceptable = true;
})
    .ConfigureApiBehaviorOptions(setup =>
    {
        //自定义错误报告
        /*setup.InvalidModelStateResponseFactory = context =>
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
        };*/
    })
    .AddNewtonsoftJson(setup =>
    {
        setup.SerializerSettings.ContractResolver =
            new CamelCasePropertyNamesContractResolver();
        setup.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    })
    //添加xml序列化器
    .AddXmlSerializerFormatters()
    .AddXmlDataContractSerializerFormatters();

//注册AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//注册服务
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//注册数据库上下文
builder.Services.AddDbContext<TicketDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
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

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//迁移配置
using (var scope = app.Services.CreateScope())
{
    try
    {
        var dbContext = scope.ServiceProvider.GetService<TicketDbContext>();

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
