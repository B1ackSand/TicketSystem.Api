using Microsoft.EntityFrameworkCore;
using Routine.Api.Entities;
using TicketSystem.Api.Entities;

// 数据库上下文，将数据视为对象并负责与之进行交互的主类（？）
// 由视角来看是初始化数据库所用
namespace TicketSystem.Api.Data
{
    public class TicketDbContext : DbContext
    {
        //推荐定义一个从 DbContext 派生的类，并且定义一个公开的 DbSet 属性用于表示上下文中指定的实体集合
        public TicketDbContext(DbContextOptions<TicketDbContext> options)
            : base(options)
        {

        }
        //可以创建类型为 DbSet<T> 的属性, 泛型类型参数 T 将是一种类型的实体
        //每个 DbSet 将映射到数据库中的一个表
        public DbSet<Booker>? Bookers { get; set; }
        public DbSet<Train>? Trains { get; set; }
        public DbSet<Station>? Stations { get; set; }

        //在完成对派生上下文的模型的初始化后，并在该模型已锁定并用于初始化上下文之前，进一步提前配置上下文
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booker>()
                .Property(x => x.FirstName).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Booker>()
                .Property(x => x.BookerWx).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Booker>()
                .Property(x => x.BookerPwd).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Booker>()
                .Property(x => x.PhoneNum).HasMaxLength(11);
            modelBuilder.Entity<Booker>()
                .Property(x => x.IsDeleted).IsRequired().HasMaxLength(10);
            //modelBuilder.Entity<Booker>().Property<bool>("isDeleted");
            //modelBuilder.Entity<Booker>()
                //.HasQueryFilter(m => EF.Property<bool>(m, "isDeleted") == false);

            modelBuilder.Entity<Train>()
                .Property(x => x.TrainName).IsRequired().HasMaxLength(5);
            modelBuilder.Entity<Train>()
                .Property(x => x.TypeOfTrain).IsRequired().HasMaxLength(10);

            modelBuilder.Entity<Station>()
                .Property(x => x.StationName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Station>()
                .Property(x => x.IsTerminal).IsRequired().HasDefaultValue(false);


            //定义x对x关系
            /*modelBuilder.Entity<Employee>()
                .HasOne(x => x.Company)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);*/

            //数据预载
            modelBuilder.Entity<Booker>().HasData(
                new Booker
                {
                    Id = Guid.Parse("99e5b121-ef55-4e35-8d72-89d5622b73d1"),
                    BookerWx = "1",
                    BookerPwd = "123456",
                    UserName = "黑沙",
                    FirstName = "李",
                    LastName = "黑沙",
                    Gender = Gender.男,
                    DateOfBirth = new DateTime(2000,01,09),
                    PhoneNum = "12345678901",
                    TimeOfRegister = DateTime.Now,
                    IsDeleted = false
                });

            modelBuilder.Entity<Train>().HasData(
                new Train
                {
                    Id = Guid.Parse("cc2a984d-cd07-4329-9b22-84a5c0185ea7"),
                    TrainName = "K48",
                    TypeOfTrain = "K"
                },
                new Train
                {
                    Id = Guid.Parse("e185afad-aa89-4d4e-bba0-391ce821ae9d"),
                    TrainName = "K11",
                    TypeOfTrain = "K"
                },
                new Train
                {
                    Id = Guid.Parse("99e5b121-ef55-4e35-8d72-89d5622b73db"),
                    TrainName = "D1868",
                    TypeOfTrain = "D"
                });

            modelBuilder.Entity<Station>().HasData(
                new Station
                {
                    Id = Guid.Parse("4b501cb3-d168-4cc0-b375-48fb33f318a4"),
                    StationName = "广州站",
                    IsTerminal = true
                },
                new Station
                {
                    Id = Guid.Parse("7eaa532c-1be5-472c-a738-94fd26e5fad6"),
                    StationName = "重庆站",
                    IsTerminal = false
                },
                new Station
                {
                    Id = Guid.Parse("72457e73-ea34-4e02-b575-8d384e82a481"),
                    StationName = "北京站",
                    IsTerminal = true
                },
                new Station
                {
                    Id = Guid.Parse("b091b148-8fc7-4ce5-a6c5-c61dbbb3f91f"),
                    StationName = "上海站",
                    IsTerminal = true
                },
                new Station
                {
                    Id = Guid.Parse("0846ff99-37ac-4849-804b-1eefac46d651"),
                    StationName = "成都站",
                    IsTerminal = false
                },
                new Station
                {
                    Id = Guid.Parse("07c4638c-48b7-4783-88a5-58f47e2a0458"),
                    StationName = "哈尔滨站",
                    IsTerminal = true
                });
        }
    }
}
