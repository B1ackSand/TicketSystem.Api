using Microsoft.EntityFrameworkCore;
using Routine.Api.Entities;

// 数据库上下文，将数据视为对象并负责与之进行交互的主类（？）
// 由视角来看是初始化数据库所用
namespace Routine.Api.Data
{
    public class RoutineDbContext : DbContext
    {
        //推荐定义一个从 DbContext 派生的类，并且定义一个公开的 DbSet 属性用于表示上下文中指定的实体集合
        public RoutineDbContext(DbContextOptions<RoutineDbContext> options)
            : base(options)
        {

        }

        //可以创建类型为 DbSet<T> 的属性, 泛型类型参数 T 将是一种类型的实体
        //每个 DbSet 将映射到数据库中的一个表
        public DbSet<Company>? Companies { get; set; }
        public DbSet<Employee>? Employees { get; set; }

        //在完成对派生上下文的模型的初始化后，并在该模型已锁定并用于初始化上下文之前，进一步提前配置上下文
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .Property(x => x.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Company>()
                .Property(x => x.Introduction).HasMaxLength(500);

            modelBuilder.Entity<Employee>()
                .Property(x => x.EmployeeNo).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<Employee>()
                .Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>()
                .Property(x => x.LastName).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<Employee>()
                .HasOne(x => x.Company)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = Guid.Parse("cc2a984d-cd07-4329-9b22-84a5c0185ea7"),
                    Name = "Microsoft",
                    Introduction = "Great Company"
                },
                new Company
                {
                    Id = Guid.Parse("e185afad-aa89-4d4e-bba0-391ce821ae9d"),
                    Name = "Google",
                    Introduction = "Evil 111",
                },
                new Company
                {
                    Id = Guid.Parse("99e5b121-ef55-4e35-8d72-89d5622b73db"),
                    Name = "Alipapa",
                    Introduction = "Fubao Company"
                }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = Guid.Parse("4b501cb3-d168-4cc0-b375-48fb33f318a4"),
                    CompanyId = Guid.Parse("cc2a984d-cd07-4329-9b22-84a5c0185ea7"),
                    DateOfBirth = new DateTime(1976, 1, 2),
                    EmployeeNo = "MSFT231",
                    FirstName = "Nick",
                    LastName = "Carter",
                    Gender = Gender.男
                },
                new Employee
                {
                    Id = Guid.Parse("7eaa532c-1be5-472c-a738-94fd26e5fad6"),
                    CompanyId = Guid.Parse("cc2a984d-cd07-4329-9b22-84a5c0185ea7"),
                    DateOfBirth = new DateTime(1981, 12, 5),
                    EmployeeNo = "MSFT245",
                    FirstName = "Vince",
                    LastName = "Carter",
                    Gender = Gender.男
                },
                new Employee
                {
                    Id = Guid.Parse("72457e73-ea34-4e02-b575-8d384e82a481"),
                    CompanyId = Guid.Parse("e185afad-aa89-4d4e-bba0-391ce821ae9d"),
                    DateOfBirth = new DateTime(1986, 11, 4),
                    EmployeeNo = "G003",
                    FirstName = "Mary",
                    LastName = "King",
                    Gender = Gender.女
                },

                 new Employee
                 {
                     Id = Guid.Parse("7644b71d-d74e-43e2-ac32-8cbadd7b1c3a"),
                     CompanyId = Guid.Parse("e185afad-aa89-4d4e-bba0-391ce821ae9d"),
                     DateOfBirth = new DateTime(1977, 4, 6),
                     EmployeeNo = "G097",
                     FirstName = "Kevin",
                     LastName = "Richardson",
                     Gender = Gender.男

                 },
                new Employee
                {
                    Id = Guid.Parse("679dfd33-32e4-4393-b061-f7abb8956f53"),
                    CompanyId = Guid.Parse("99e5b121-ef55-4e35-8d72-89d5622b73db"),
                    DateOfBirth = new DateTime(1967, 1, 24),
                    EmployeeNo = "A009",
                    FirstName = "卡",
                    LastName = "里",
                    Gender = Gender.女
                },

                new Employee
                {
                    Id = Guid.Parse("1861341e-b42b-410c-ae21-cf11f36fc574"),
                    CompanyId = Guid.Parse("99e5b121-ef55-4e35-8d72-89d5622b73db"),
                    DateOfBirth = new DateTime(1957, 3, 8),
                    EmployeeNo = "A404",
                    FirstName = "Not",
                    LastName = "Man",
                    Gender = Gender.男
                });
        }
    }
}
