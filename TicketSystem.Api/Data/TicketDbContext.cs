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
        public DbSet<Line>? Lines { get; set; }
        public DbSet<Order>? Orders { get; set; }

        //在完成对派生上下文的模型的初始化后，并在该模型已锁定并用于初始化上下文之前，进一步提前配置上下文
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booker>().Property(x => x.BookerId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Booker>()
                .Property(x => x.FirstName).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Booker>()
            .Property(x => x.CardId).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Booker>()
                .Property(x => x.BookerPwd).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Booker>()
                .Property(x => x.PhoneNum).HasMaxLength(11);
            modelBuilder.Entity<Booker>()
                .Property(x => x.IsDeleted).IsRequired().HasMaxLength(10);

            modelBuilder.Entity<Train>().Property(x => x.TrainId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Train>()
                .Property(x => x.TrainName).IsRequired().HasMaxLength(5);

            //唯一索引
            modelBuilder.Entity<Train>().HasIndex(x => x.TrainName).IsUnique();
            modelBuilder.Entity<Train>()
                .Property(x => x.TypeOfTrain).IsRequired().HasMaxLength(10);


            modelBuilder.Entity<Station>().Property(x => x.StationId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Station>()
                .Property(x => x.StationName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Station>()
                .Property(x => x.IsTerminal).IsRequired().HasDefaultValue(false);

            modelBuilder.Entity<Line>().Property(x => x.LineId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Line>()
                .Property(x => x.StartTerminal).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Line>()
                .Property(x => x.StopStation).IsRequired().HasMaxLength(500);
            modelBuilder.Entity<Line>()
                .Property(x => x.EndTerminal).IsRequired().HasMaxLength(20);

            //定义x对x关系
            modelBuilder.Entity<Train>()
                .HasOne(x => x.Line)
                .WithMany(x => x.Trains)
                .HasForeignKey(x => x.LineId)
                .OnDelete(DeleteBehavior.Cascade);

            // modelBuilder.Entity<Line>()
            //     .HasOne(x => x.Station)
            //     .WithMany(x => x.Lines)
            //     .HasForeignKey(x => x.StartTerminal)
            //     .OnDelete(DeleteBehavior.Cascade);

            //需要手动配
            // modelBuilder.Entity<Line>()
            //     .HasOne(x => x.Station)
            //     .WithMany(x => x.Lines)
            //     .HasForeignKey(x => x.EndTerminal)
            //     .OnDelete(DeleteBehavior.Cascade);


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
                    BookerId = 1,
                    CardId = "453009200001013710",
                    BookerPwd = "123456",
                    UserName = "黑沙",
                    FirstName = "李",
                    LastName = "黑沙",
                    Gender = Gender.男,
                    DateOfBirth = new DateTime(2000, 01, 09),
                    PhoneNum = "13600291522",
                    TimeOfRegister = DateTime.Now,
                    IsDeleted = false
                });

            modelBuilder.Entity<Train>().HasData(
                new Train
                {
                    TrainId = 1,
                    LineId = 1,
                    TrainName = "D112",
                    TypeOfTrain = "D",
                    Time = new TimeOnly(14, 30)
                },
                new Train
                {
                    TrainId = 2,
                    LineId = 2,
                    TrainName = "D1849",
                    TypeOfTrain = "D",
                    Time = new TimeOnly(12, 30)
                },
                new Train
                {
                    TrainId = 3,
                    LineId = 3,
                    TrainName = "K528",
                    TypeOfTrain = "K",
                    Time = new TimeOnly(8, 50)
                },
                new Train
                {
                    TrainId = 4,
                    LineId = 4,
                    TrainName = "G1204",
                    TypeOfTrain = "G",
                    Time = new TimeOnly(19, 12)
                },
                new Train
                {
                    TrainId = 5,
                    LineId = 5,
                    TrainName = "D636",
                    TypeOfTrain = "D",
                    Time = new TimeOnly(11, 45)
                },
                new Train
                {
                    TrainId = 6,
                    LineId = 6,
                    TrainName = "K527",
                    TypeOfTrain = "K",
                    Time = new TimeOnly(7, 10)
                },
                new Train
                {
                    TrainId = 7,
                    LineId = 7,
                    TrainName = "K728",
                    TypeOfTrain = "K",
                    Time = new TimeOnly(15, 55)
                },
                new Train
                {
                    TrainId = 8,
                    LineId = 8,
                    TrainName = "G1202",
                    TypeOfTrain = "G",
                    Time = new TimeOnly(14, 3)
                },
                new Train
                {
                    TrainId = 9,
                    LineId = 9,
                    TrainName = "K518",
                    TypeOfTrain = "K",
                    Time = new TimeOnly(8, 20)
                },
                new Train
                {
                    TrainId = 10,
                    LineId = 10,
                    TrainName = "K488",
                    TypeOfTrain = "K",
                    Time = new TimeOnly(10, 10)
                },
                new Train
                {
                    TrainId = 11,
                    LineId = 11,
                    TrainName = "G2195",
                    TypeOfTrain = "G",
                    Time = new TimeOnly(17, 00)
                },
                new Train
                {
                    TrainId = 12,
                    LineId = 12,
                    TrainName = "K546",
                    TypeOfTrain = "K",
                    Time = new TimeOnly(18, 40)
                });

            modelBuilder.Entity<Station>().HasData(
                new Station
                {
                    StationId = 1,
                    StationName = "广州站",
                    IsTerminal = true
                },
                new Station
                {
                    StationId = 2,
                    StationName = "重庆站",
                    IsTerminal = false
                },
                new Station
                {
                    StationId = 3,
                    StationName = "北京站",
                    IsTerminal = false
                },
                new Station
                {
                    StationId = 4,
                    StationName = "上海站",
                    IsTerminal = true
                },
                new Station
                {
                    StationId = 5,
                    StationName = "成都站",
                    IsTerminal = true
                },
                new Station
                {
                    StationId = 6,
                    StationName = "哈尔滨站",
                    IsTerminal = true
                },
                new Station
                {
                    StationId = 7,
                    StationName = "武汉站",
                    IsTerminal = false
                });

            modelBuilder.Entity<Line>().HasData(
                new Line
                {
                    LineId = 1,
                    StartTerminal = "广州站",
                    EndTerminal = "哈尔滨站",
                    StopStation = "广州站,武汉站,北京站,哈尔滨站"
                },
                new Line
                {
                    LineId = 2,
                    StartTerminal = "广州站",
                    EndTerminal = "成都站",
                    StopStation = "广州站,重庆站,成都站"
                },
                new Line
                {
                    LineId = 3,
                    StartTerminal = "广州站",
                    EndTerminal = "上海站",
                    StopStation = "广州站,武汉站,上海站"
                },

                new Line
                {
                    LineId = 4,
                    StartTerminal = "上海站",
                    EndTerminal = "哈尔滨站",
                    StopStation = "上海站,北京站,哈尔滨站"
                },
                new Line
                {
                    LineId = 5,
                    StartTerminal = "上海站",
                    EndTerminal = "成都站",
                    StopStation = "上海站,武汉站,重庆站,成都站"
                },
                new Line
                {
                    LineId = 6,
                    StartTerminal = "上海站",
                    EndTerminal = "广州站",
                    StopStation = "上海站,武汉站,广州站"
                },

                new Line
                {
                    LineId = 7,
                    StartTerminal = "哈尔滨站",
                    EndTerminal = "广州站",
                    StopStation = "哈尔滨站,北京站,武汉站,广州站"
                },
                new Line
                {
                    LineId = 8,
                    StartTerminal = "哈尔滨站",
                    EndTerminal = "上海站",
                    StopStation = "哈尔滨站,北京站,上海站"
                },
                new Line
                {
                    LineId = 9,
                    StartTerminal = "哈尔滨站",
                    EndTerminal = "成都站",
                    StopStation = "哈尔滨站,北京站,武汉站,重庆站,成都站"
                },

                new Line
                {
                    LineId = 10,
                    StartTerminal = "成都站",
                    EndTerminal = "广州站",
                    StopStation = "成都站,重庆站,广州站"
                },
                new Line
                {
                    LineId = 11,
                    StartTerminal = "成都站",
                    EndTerminal = "上海站",
                    StopStation = "成都站,重庆站,武汉站,上海站"
                },
                new Line
                {
                    LineId = 12,
                    StartTerminal = "成都站",
                    EndTerminal = "哈尔滨站",
                    StopStation = "成都站,重庆站,武汉站,北京站,哈尔滨站"
                });
        }
    }
}
