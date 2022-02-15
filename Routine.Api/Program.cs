using Microsoft.EntityFrameworkCore;
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
    //setup.Input...
    
    //添加xml格式化器
}).AddXmlDataContractSerializerFormatters();

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
        appBuilder.Run(async context=>
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
