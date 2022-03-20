using System.ComponentModel.DataAnnotations.Schema;
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
            modelBuilder.Entity<Booker>().Property(x=>x.BookerId).ValueGeneratedOnAdd();
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
            //modelBuilder.Entity<Booker>().Property<bool>("isDeleted");
            //modelBuilder.Entity<Booker>()
            //.HasQueryFilter(m => EF.Property<bool>(m, "isDeleted") == false);

            modelBuilder.Entity<Train>()
                .Property(x => x.TrainName).IsRequired().HasMaxLength(5);
            //唯一索引
            modelBuilder.Entity<Train>().HasIndex(x => x.TrainName).IsUnique();
            modelBuilder.Entity<Train>()
                .Property(x => x.TypeOfTrain).IsRequired().HasMaxLength(10);

            modelBuilder.Entity<Station>()
                .Property(x => x.StationName).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<Station>()
                .Property(x => x.IsTerminal).IsRequired().HasDefaultValue(false);


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

            modelBuilder.Entity<Line>()
                .HasOne(x => x.Station)
                .WithMany(x => x.Lines)
                .HasForeignKey(x => x.StartTerminal)
                .HasPrincipalKey(x => x.StationName);

            //需要手动配
            modelBuilder.Entity<Line>()
                .HasOne(x => x.Station)
                .WithMany(x => x.Lines)
                .HasForeignKey(x => x.EndTerminal)
                .HasPrincipalKey(x => x.StationName);


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
                    PhoneNum = "12345678901",
                    TimeOfRegister = DateTime.Now,
                    IsDeleted = false
                });

            modelBuilder.Entity<Train>().HasData(
                new Train
                {
                    TrainId = Guid.Parse("cc2a984d-cd07-4329-9b22-84a5c0185ea7"),
                    LineId = Guid.Parse("92d0ada0-2cd0-4cc9-b03d-3eccf17ab1a5"),
                    TrainName = "Z112",
                    TypeOfTrain = "Z",
                    Time = new TimeOnly(14, 30)
                },
                new Train
                {
                    TrainId = Guid.Parse("e185afad-aa89-4d4e-bba0-391ce821ae9d"),
                    LineId = Guid.Parse("18c9ecbb-dc2c-43e8-ba77-9a6cef3ac9bc"),
                    TrainName = "D1849",
                    TypeOfTrain = "D",
                    Time = new TimeOnly(12, 30)
                },
                new Train
                {
                    TrainId = Guid.Parse("99e5b121-ef55-4e35-8d72-89d5622b73db"),
                    LineId = Guid.Parse("cbead21b-0681-4a1a-853f-d5b61fd48f54"),
                    TrainName = "K528",
                    TypeOfTrain = "K",
                    Time = new TimeOnly(8, 50)
                },
                new Train
                {
                    TrainId = Guid.Parse("40843c33-3050-437d-9749-73c7823be7a1"),
                    LineId = Guid.Parse("10687777-24de-4a07-a677-633031ae1009"),
                    TrainName = "G1204",
                    TypeOfTrain = "G",
                    Time = new TimeOnly(19, 12)
                },
                new Train
                {
                    TrainId = Guid.Parse("146dae5c-7912-45bc-9e5c-60cfc5d77b6a"),
                    LineId = Guid.Parse("e7ff44ba-c4f9-40c8-a5a0-9ddc557f6093"),
                    TrainName = "D636",
                    TypeOfTrain = "D",
                    Time = new TimeOnly(11, 45)
                },
                new Train
                {
                    TrainId = Guid.Parse("f4abb3d9-873b-44ff-90cd-860a36fc259f"),
                    LineId = Guid.Parse("ee3e7e33-2c85-46c9-98e5-b4bf10f32576"),
                    TrainName = "K527",
                    TypeOfTrain = "K",
                    Time = new TimeOnly(7, 10)
                },
                new Train
                {
                    TrainId = Guid.Parse("5d0c96b6-b3eb-497d-8c4c-f12e05fb5e29"),
                    LineId = Guid.Parse("b2187869-2f9f-4ea0-99b4-b8e5c8f34f3d"),
                    TrainName = "K728",
                    TypeOfTrain = "K",
                    Time = new TimeOnly(15, 55)
                },
                new Train
                {
                    TrainId = Guid.Parse("5ee7f9cd-279f-4c5b-83bf-034f6419be7a"),
                    LineId = Guid.Parse("ee9e796d-fbfe-42c2-8eb4-b9674206ebc7"),
                    TrainName = "G1202",
                    TypeOfTrain = "G",
                    Time = new TimeOnly(14, 3)
                },
                new Train
                {
                    TrainId = Guid.Parse("7971f095-300c-4628-b2a8-4e64ba04cbc3"),
                    LineId = Guid.Parse("fec134b0-8623-42db-8602-b64cce2912c2"),
                    TrainName = "K548",
                    TypeOfTrain = "K",
                    Time = new TimeOnly(8, 20)
                },
                new Train
                {
                    TrainId = Guid.Parse("639031e7-cd65-466f-9e8b-f67c14801973"),
                    LineId = Guid.Parse("804edb5e-2bce-43e7-b34b-6db68a9ceb27"),
                    TrainName = "K488",
                    TypeOfTrain = "K",
                    Time = new TimeOnly(10, 10)
                },
                new Train
                {
                    TrainId = Guid.Parse("f5d6e132-c4df-43fe-91c2-39f390dadab7"),
                    LineId = Guid.Parse("ba2b1c71-bff6-4507-ad15-99c6e13bb5fa"),
                    TrainName = "G2195",
                    TypeOfTrain = "G",
                    Time = new TimeOnly(17, 00)
                },
                new Train
                {
                    TrainId = Guid.Parse("88f68a2e-d574-4dd5-b5dd-e5048b82e867"),
                    LineId = Guid.Parse("c9c55cc8-2185-40b8-b85b-55c34c918f66"),
                    TrainName = "K546",
                    TypeOfTrain = "K",
                    Time = new TimeOnly(18, 40)
                });

            modelBuilder.Entity<Station>().HasData(
                new Station
                {
                    StationId = Guid.Parse("4b501cb3-d168-4cc0-b375-48fb33f318a4"),
                    StationName = "广州站",
                    IsTerminal = true
                },
                new Station
                {
                    StationId = Guid.Parse("7eaa532c-1be5-472c-a738-94fd26e5fad6"),
                    StationName = "重庆站",
                    IsTerminal = false
                },
                new Station
                {
                    StationId = Guid.Parse("72457e73-ea34-4e02-b575-8d384e82a481"),
                    StationName = "北京站",
                    IsTerminal = false
                },
                new Station
                {
                    StationId = Guid.Parse("b091b148-8fc7-4ce5-a6c5-c61dbbb3f91f"),
                    StationName = "上海站",
                    IsTerminal = true
                },
                new Station
                {
                    StationId = Guid.Parse("0846ff99-37ac-4849-804b-1eefac46d651"),
                    StationName = "成都站",
                    IsTerminal = true
                },
                new Station
                {
                    StationId = Guid.Parse("07c4638c-48b7-4783-88a5-58f47e2a0458"),
                    StationName = "哈尔滨站",
                    IsTerminal = true
                },
                new Station
                {
                    StationId = Guid.Parse("09626794-5565-452e-85a4-b924805588ba"),
                    StationName = "武汉站",
                    IsTerminal = false
                });

            modelBuilder.Entity<Line>().HasData(
                new Line
                {
                    LineId = Guid.Parse("92d0ada0-2cd0-4cc9-b03d-3eccf17ab1a5"),
                    StartTerminal = "广州站",
                    EndTerminal = "哈尔滨站",
                    StopStation = "广州站,武汉站,北京站,哈尔滨站"
                },
                new Line
                {
                    LineId = Guid.Parse("18c9ecbb-dc2c-43e8-ba77-9a6cef3ac9bc"),
                    StartTerminal = "广州站",
                    EndTerminal = "成都站",
                    StopStation = "广州站,重庆站,成都站"
                },
                new Line
                {
                    LineId = Guid.Parse("cbead21b-0681-4a1a-853f-d5b61fd48f54"),
                    StartTerminal = "广州站",
                    EndTerminal = "上海站",
                    StopStation = "广州站,武汉站,上海站"
                },

                new Line
                {
                    LineId = Guid.Parse("10687777-24de-4a07-a677-633031ae1009"),
                    StartTerminal = "上海站",
                    EndTerminal = "哈尔滨站",
                    StopStation = "上海站,北京站,哈尔滨站"
                },
                new Line
                {
                    LineId = Guid.Parse("e7ff44ba-c4f9-40c8-a5a0-9ddc557f6093"),
                    StartTerminal = "上海站",
                    EndTerminal = "成都站",
                    StopStation = "上海站,武汉站,重庆站,成都站"
                },
                new Line
                {
                    LineId = Guid.Parse("ee3e7e33-2c85-46c9-98e5-b4bf10f32576"),
                    StartTerminal = "上海站",
                    EndTerminal = "广州站",
                    StopStation = "上海站,武汉站,广州站"
                },

                new Line
                {
                    LineId = Guid.Parse("b2187869-2f9f-4ea0-99b4-b8e5c8f34f3d"),
                    StartTerminal = "哈尔滨站",
                    EndTerminal = "广州站",
                    StopStation = "哈尔滨站,北京站,武汉站,广州站"
                },
                new Line
                {
                    LineId = Guid.Parse("ee9e796d-fbfe-42c2-8eb4-b9674206ebc7"),
                    StartTerminal = "哈尔滨站",
                    EndTerminal = "上海站",
                    StopStation = "哈尔滨站,北京站,上海站"
                },
                new Line
                {
                    LineId = Guid.Parse("fec134b0-8623-42db-8602-b64cce2912c2"),
                    StartTerminal = "哈尔滨站",
                    EndTerminal = "成都站",
                    StopStation = "哈尔滨站,北京站,武汉站,重庆站,成都站"
                },

                new Line
                {
                    LineId = Guid.Parse("804edb5e-2bce-43e7-b34b-6db68a9ceb27"),
                    StartTerminal = "成都站",
                    EndTerminal = "广州站",
                    StopStation = "成都站,重庆站,广州站"
                },
                new Line
                {
                    LineId = Guid.Parse("ba2b1c71-bff6-4507-ad15-99c6e13bb5fa"),
                    StartTerminal = "成都站",
                    EndTerminal = "上海站",
                    StopStation = "成都站,重庆站,武汉站,上海站"
                },
                new Line
                {
                    LineId = Guid.Parse("c9c55cc8-2185-40b8-b85b-55c34c918f66"),
                    StartTerminal = "成都站",
                    EndTerminal = "哈尔滨站",
                    StopStation = "成都站,重庆站,武汉站,北京站,哈尔滨站"
                });
        }
    }
}
