using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Snai3i_DAL.Data.Configration;
using Snai3i_DAL.Data.Models;
namespace Snai3i_DAL.Data.Context
{
    public class SnaiiDBContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SnaiiDBContext(DbContextOptions<SnaiiDBContext> options
            , IHttpContextAccessor httpContextAccessor) : base(options)
        {

            _httpContextAccessor = httpContextAccessor;

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Unable to create a 'DbContext' of type ''. The exception 'The seed entity for
            //entity type 'Order' cannot be added because it has the navigation 'user' set.
            //To seed relationships,  add the entity seed to 'Order' and specify the foreign key values
            //{'UserId'}. Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging'
            //to see the involved property values.' was thrown while attempting to create an instance.
            //For the different patterns supported at design time, see https://go.microsoft.com/fwlink/?linkid=851728
            optionsBuilder.EnableSensitiveDataLogging();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // table per hierarchy 
            modelBuilder.Entity<ApplicationUser>()
                .HasDiscriminator<string>("type").
                HasValue<User>(usertype.User.ToString() )
                .HasValue<Worker>(usertype.Worker.ToString() );

            //The following code will set ON DELETE NO ACTION to all Foreign Keys
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.NoAction;

            }
            // IdentityUserLogin<string> has no key 

            modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
            modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();
            modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();


            //call WorkerEntityTypeConfiguration
            //new WorkerEntityTypeConfiguration().configure(modelBuilder.Entity<Worker>());
            //modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());




            //dataseeding 

            var superAdminId = "superadmin-id";
            var superAdmin = new User
            {
                Id = superAdminId,
                UserName = "kholoud",
                LastName = "Salama",
                Email = "kholoud@sani3i.com",
                EmailConfirmed = true,
                image = "Khouloud.png",
                PhoneNumber = "0100000000",
                PhoneNumberConfirmed = true,
                NormalizedEmail = "KHOLOUD@SANI3I.COM",
                NormalizedUserName = "KHOLOUD",
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                Isdeleted = false
            };

            //var passwordHasher = new PasswordHasher<ApplicationUser>();
            //superAdmin.PasswordHash = passwordHasher.HashPassword(superAdmin, "Kholoud@123");

            // Seeding Users
            var user1 = new User
            {
                Id = "user1-id",
                UserName = "ahmed",
                LastName = "Ragab",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Email = "ahmed@example.com",
                Longitude = 30.0444,
                Latitude = 31.2357,
                image = "ahmed.png",
                NormalizedEmail = "AHMED@EXAMPLE.COM",
                NormalizedUserName = "AHMED",
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                Isdeleted = false
            };

            var user2 = new User
            {
                Id = "user2-id",
                UserName = "sara",
                LastName = "elalfy",
                Email = "sara@example.com",
                Longitude = 29.9773,
                Latitude = 31.1325,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                image = "Sara.png",
                NormalizedEmail = "SARA@EXAMPLE.COM",
                NormalizedUserName = "SARA",
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                Isdeleted = false
            };

            var user3 = new User
            {
                Id = "user3-id",
                UserName = "omar",
                LastName = "hamada",
                Email = "omar@example.com",
                Longitude = 29.8264,
                Latitude = 30.8141,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                NormalizedEmail = "OMAR@EXAMPLE.COM",
                NormalizedUserName = "OMAR",
                image = "Omar.png",
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                Isdeleted = false
            };

            // Seeding Workers
            var worker1 = new Worker
            {
                Id = "worker1-id",
                UserName = "mustafa",
                LastName = "hamed",
                Email = "mustafa@example.com",
                Nationalnumber = 123456789,
                Governorate = "Cairo",
                StartingDate = new DateTime(2020, 5, 1),
                NumberOfOperation = 10,
                NormalizedEmail = "MUSTAFA@EXAMPLE.COM",
                NormalizedUserName = "MUSTAFA",
                image = "Mustafa.png",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                CraftId = 1,
                Isdeleted = false
            };

            var worker2 = new Worker
            {
                Id = "worker2-id",
                UserName = "mohamed",
                LastName = "yousry",
                Email = "mohamed@example.com",
                Nationalnumber = 987654321,
                Governorate = "Alexandria",
                StartingDate = new DateTime(2021, 8, 15),
                NumberOfOperation = 20,
                NormalizedEmail = "MOHAMED@EXAMPLE.COM",
                NormalizedUserName = "MOHAMED",
                image = "Mohamed.png",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                CraftId = 1,
                Isdeleted = false
            };

            var worker3 = new Worker
            {
                Id = "worker3-id",
                UserName = "fatma",
                LastName = "yassin",
                Email = "fatma@example.com",
                Nationalnumber = 1122334455,
                Governorate = "Giza",
                StartingDate = new DateTime(2019, 12, 1),
                NumberOfOperation = 15,
                NormalizedEmail = "FATMA@EXAMPLE.COM",
                NormalizedUserName = "FATMA",
                LockoutEnabled = false,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                image = "Fatma.png",
                SecurityStamp = Guid.NewGuid().ToString(),
                CraftId = 2,
                Isdeleted = false
            };

            // password_hashing 
            // Seeding roles
            var adminRole = new IdentityRole { Id = "role-admin-id", Name = "Admin", NormalizedName = "ADMIN" };
            var superAdminRole = new IdentityRole { Id = "role-superadmin-id", Name = "SuperAdmin", NormalizedName = "SUPERADMIN" };
            var userRole = new IdentityRole { Id = "role-user-id", Name = "User", NormalizedName = "USER" };
            var workerRole = new IdentityRole { Id = "role-worker-id", Name = "Worker", NormalizedName = "WORKER" };

            modelBuilder.Entity<IdentityRole>().HasData(adminRole, superAdminRole, userRole, workerRole);

            // Seeding SuperAdmin and Users
            var passwordHasher = new PasswordHasher<ApplicationUser>();

            superAdmin.PasswordHash = passwordHasher.HashPassword(superAdmin, "Kholoud@123");
            user1.PasswordHash = passwordHasher.HashPassword(user1, "Ahmed@123");
            user2.PasswordHash = passwordHasher.HashPassword(user2, "Sara@123");
            user3.PasswordHash = passwordHasher.HashPassword(user3, "Omar@123");

            worker1.PasswordHash = passwordHasher.HashPassword(worker1, "Mustafa@123");
            worker2.PasswordHash = passwordHasher.HashPassword(worker2, "Mohamed@123");
            worker3.PasswordHash = passwordHasher.HashPassword(worker3, "Fatma@123");



            var userRoles = new[]
        {
                new IdentityUserRole<string> { UserId = "superadmin-id", RoleId = "role-superadmin-id" },
                new IdentityUserRole<string> { UserId = "user1-id", RoleId = "role-user-id" },
                new IdentityUserRole<string> { UserId = "user2-id", RoleId = "role-user-id" },
                new IdentityUserRole<string> { UserId = "user3-id", RoleId = "role-user-id" },
                new IdentityUserRole<string> { UserId = "worker1-id", RoleId = "role-worker-id" },
                new IdentityUserRole<string> { UserId = "worker2-id", RoleId = "role-worker-id" },
                new IdentityUserRole<string> { UserId = "worker3-id", RoleId = "role-worker-id" }


            };

            //seeding claims 

            List<Claim> claims_kholoud = new List<Claim>();
            claims_kholoud.Add(new Claim(ClaimTypes.NameIdentifier, superAdmin.Id));
            claims_kholoud.Add(new Claim("first_name", superAdmin.UserName));
            claims_kholoud.Add(new Claim(ClaimTypes.Role, usertype.User.ToString()));
            claims_kholoud.Add(new Claim(ClaimTypes.Role, usertype.SuperAdmin.ToString()));


            List<Claim> claims_User1 = new List<Claim>();
            claims_User1.Add(new Claim(ClaimTypes.NameIdentifier, user1.Id));
            claims_User1.Add(new Claim("first_name", user1.UserName));
            claims_User1.Add(new Claim(ClaimTypes.Role, usertype.User.ToString()));

            List<Claim> claims_User2 = new List<Claim>();
            claims_User2.Add(new Claim(ClaimTypes.NameIdentifier, user2.Id));
            claims_User2.Add(new Claim("first_name", user2.UserName));
            claims_User2.Add(new Claim(ClaimTypes.Role, usertype.User.ToString()));

            List<Claim> Claims_user3 = new List<Claim>();
            Claims_user3.Add(new Claim(ClaimTypes.NameIdentifier, user3.Id));
            Claims_user3.Add(new Claim("first_name", user3.UserName));
            Claims_user3.Add(new Claim(ClaimTypes.Role, usertype.User.ToString()));

            List<Claim> claims_worker1 = new List<Claim>();
            claims_worker1.Add(new Claim(ClaimTypes.NameIdentifier, worker1.Id));
            claims_worker1.Add(new Claim("first_name", worker1.UserName));
            claims_worker1.Add(new Claim(ClaimTypes.Role, usertype.Worker.ToString()));

            List<Claim> claims_worker2 = new List<Claim>();
            claims_worker2.Add(new Claim(ClaimTypes.NameIdentifier, worker2.Id));
            claims_worker2.Add(new Claim("first_name", worker2.UserName));
            claims_worker2.Add(new Claim(ClaimTypes.Role, usertype.Worker.ToString()));

            List<Claim> claims_worker3 = new List<Claim>();
            claims_worker3.Add(new Claim(ClaimTypes.NameIdentifier, worker3.Id));
            claims_worker3.Add(new Claim("first_name", worker3.UserName));
            claims_worker3.Add(new Claim(ClaimTypes.Role, usertype.Worker.ToString()));


            // Seeding Reviews
            var review1 = new Review
            {
                Id = 1,
                UserId = "user1-id",
                WorkerId = "worker1-id",
                Rate = 4.5f,
                Comment = "Great service, very satisfied!",
                OrderId = 1
            };
            var review2 = new Review
            {
                Id = 2,
                UserId = "user2-id",
                WorkerId = "worker2-id",
                Rate = 4.0f,
                Comment = "Good job, but a bit delayed.",
                OrderId = 2
            };
            var review3 = new Review
            {
                Id = 3,
                UserId = "user3-id",
                WorkerId = "worker3-id",
                Rate = 5.0f,
                Comment = "Excellent service, highly recommended!",
                OrderId = 3
            };

            // Seeding Orders

            var order1 = new Order
            {
                Id = 1,
                ForwardDate = new DateTime(2023, 12, 25),
                ConfirmDate = new DateTime(2023, 12, 27),
                UserId = "user1-id",
                WorkerId = "worker1-id",
                Value = 1500.50,
                Commision = 10.0,
                reviewId = 1
            };
            var order2 = new Order
            {
                Id = 2,
                ForwardDate = new DateTime(2023, 12, 26),
                ConfirmDate = new DateTime(2023, 12, 28),
                UserId = "user2-id",
                WorkerId = "worker2-id",
                Value = 800.75,
                Commision = 8.0,
                reviewId = 2
            };
            var order3 = new Order
            {
                Id = 3,
                ForwardDate = new DateTime(2023, 12, 27),
                ConfirmDate = new DateTime(2023, 12, 29),
                UserId = "user3-id",
                WorkerId = "worker3-id",
                Value = 1000.25,
                Commision = 9.5,
                reviewId = 3
            };



            // Seeding Sales
            var sale1 = new Sales
            {
                Id = 1,
                Date = new DateTime(2023, 12, 30),
                DeliveryState = "Delivered",
                Address = "123 Main St, Cairo",
                OrderState = "Completed",
                UserId = "user1-id",
                WorkerId = "worker1-id",
                CardId = 1
            };
            var sale2 = new Sales
            {
                Id = 2,
                Date = new DateTime(2023, 12, 29),
                DeliveryState = "Pending",
                Address = "456 Elm St, Alexandria",
                OrderState = "Processing",
                UserId = "user3-id",
                WorkerId = "worker3-id",
                CardId = 2
            };
            var sale3 = new Sales
            {
                Id = 3,
                Date = new DateTime(2023, 12, 28),
                DeliveryState = "Shipped",
                Address = "789 Oak St, Giza",
                OrderState = "Shipped",
                UserId = "user2-id",
                WorkerId = "worker2-id",
                CardId = 3
            };

            // Seeding Sizes
            var size1 = new Size
            {
                Id = 1,
                Price = 100.00,
                ToolSize = 10.5,
                Stock = 20,
                ToolId = 2
            };
            var size2 = new Size
            {
                Id = 2,
                Price = 150.00,
                ToolSize = 12.5,
                Stock = 15,
                ToolId = 3
            };
            var size3 = new Size
            {
                Id = 3,
                Price = 200.00,
                ToolSize = 15.0,
                Stock = 10,
                ToolId = 3
            };

            // Seeding Cards
            var card1 = new Card
            {
                Id = 1,
                BuyerId = user1.Id,
                Quantity = 2,
                ToolId = 1,
                SizeId = 1,
            };
            var card2 = new Card
            {
                Id = 2,
                BuyerId = user2.Id,
                Quantity = 3,
                ToolId = 2,
                SizeId = 2,
            };
            var card3 = new Card
            {
                Id = 3,
                BuyerId = user2.Id,
                Quantity = 5,
                ToolId = 3,
                SizeId = 3,
            };

            // Seeding Tools
            var tool1 = new Tool
            {
                Id = 1,
                Name = "Hammer",
                Description = "Heavy-duty hammer",
                Image = "hammer.png"

            };
            var tool2 = new Tool
            {
                Id = 2,
                Name = "Screwdriver",
                Description = "Multi-purpose screwdriver",
                Image = "screwdriver.png"
            };
            var tool3 = new Tool
            {
                Id = 3,
                Name = "Wrench",
                Description = "Adjustable wrench",
                Image = "wrench.png"
            };

            // Seeding Crafts
            var craft1 = new Craft
            {
                CraftId = 1,
                CraftName = "Plumbing",
                PictureUrl = "service-1.jpg",

            };

            var craft2 = new Craft
            {
                CraftId = 2,
                CraftName = "Electrical",
                PictureUrl = "service-2.jpg",


            };

            var craft3 = new Craft
            {
                CraftId = 3,
                CraftName = "Carpentry",
                PictureUrl = "service-3.jpg",


            };

            // Add seeded data to ModelBuilder
            // Seeding into the database
            // even the order of the dataseeding still making an error takecare 

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);

            modelBuilder.Entity<User>().HasData(user1, user2, user3, superAdmin);
            modelBuilder.Entity<Worker>().HasData(worker1, worker2, worker3);
            modelBuilder.Entity<Craft>().HasData(craft1, craft2, craft3);
            modelBuilder.Entity<Review>().HasData(review1, review2, review3);
            modelBuilder.Entity<Order>().HasData(order1, order2, order3);
            // first seed the entites above to send your data
            // second seed the entites below to safely upload your data 
            modelBuilder.Entity<Tool>().HasData(tool1, tool2, tool3);
            modelBuilder.Entity<Sales>().HasData(sale1, sale2, sale3);
            modelBuilder.Entity<Size>().HasData(size1, size2, size3);
            modelBuilder.Entity<Card>().HasData(card1, card2, card3);

            //add seeded claims 
            int i = 1;

            foreach (var claim in claims_kholoud)
            {
                IdentityUserClaim<string> identityUserClaim = new IdentityUserClaim<string>();
                identityUserClaim.Id = i;
                identityUserClaim.UserId = superAdminId;
                identityUserClaim.InitializeFromClaim(claim);
                modelBuilder.Entity<IdentityUserClaim<string>>().
                    HasData(identityUserClaim);
                i = i + 1;
            }

            foreach (var claim in claims_User1)
            {
                IdentityUserClaim<string> identityUserClaim = new IdentityUserClaim<string>();
                identityUserClaim.Id = i;
                identityUserClaim.UserId = user1.Id;
                identityUserClaim.InitializeFromClaim(claim);
                modelBuilder.Entity<IdentityUserClaim<string>>().
                    HasData(identityUserClaim);
                i = i + 1;
            }

            foreach (var claim in claims_User2)
            {
                IdentityUserClaim<string> identityUserClaim = new IdentityUserClaim<string>();
                identityUserClaim.Id = i;
                identityUserClaim.UserId = user2.Id;

                identityUserClaim.InitializeFromClaim(claim);
                modelBuilder.Entity<IdentityUserClaim<string>>().
                    HasData(identityUserClaim);
                i = i + 1;

            }

            foreach (var claim in Claims_user3)
            {
                IdentityUserClaim<string> identityUserClaim = new IdentityUserClaim<string>();
                identityUserClaim.Id = i;
                identityUserClaim.UserId = user3.Id;

                identityUserClaim.InitializeFromClaim(claim);
                modelBuilder.Entity<IdentityUserClaim<string>>().
                    HasData(identityUserClaim);
                i = i + 1;

            }

            foreach (var claim in claims_worker1)
            {
                IdentityUserClaim<string> identityUserClaim = new IdentityUserClaim<string>();
                identityUserClaim.Id = i;
                identityUserClaim.UserId = worker1.Id;
                identityUserClaim.InitializeFromClaim(claim);
                modelBuilder.Entity<IdentityUserClaim<string>>().
                    HasData(identityUserClaim);
                i = i + 1;

            }

            foreach (var claim in claims_worker2)
            {
                //modelBuilder.Entity<IdentityUserClaim<string>>().HasData(new IdentityUserClaim<string>
                //{
                //    Id = 1,
                //    UserId = worker2.Id,
                //    ClaimType = claim.Type,
                //    ClaimValue = claim.Value,
                //});
                IdentityUserClaim<string> identityUserClaim = new IdentityUserClaim<string>();
                identityUserClaim.Id = i;
                identityUserClaim.UserId = worker2.Id;

                identityUserClaim.InitializeFromClaim(claim);
                modelBuilder.Entity<IdentityUserClaim<string>>().
                    HasData(identityUserClaim);
                i = i + 1;

            }

            foreach (var claim in claims_worker3)
            {
                IdentityUserClaim<string> identityUserClaim = new IdentityUserClaim<string>();
                identityUserClaim.Id = i;
                identityUserClaim.UserId = worker3.Id;

                identityUserClaim.InitializeFromClaim(claim);
                modelBuilder.Entity<IdentityUserClaim<string>>().
                    HasData(identityUserClaim);
                i = i + 1;

            }


        }







        public DbSet<ApplicationUser> applicationUsers { get; set; }

        public DbSet<Craft> crafts { get; set; }

        public DbSet<Order> orders { get; set; }

        public DbSet<Review> reviews { get; set; }

        public DbSet<Chat> chats { get; set; }

        public DbSet<Tool> tools { get; set; }

        public DbSet<Size> sizes { get; set; }

        public DbSet<Card> cards { get; set; }

        public DbSet<Sales> sales { get; set; }

        // overriding savechange for softdelete
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                var entity = entry.Entity;

                if (entry.State == EntityState.Deleted && entity is Isoftdelete)
                {
                    entry.State = EntityState.Unchanged;
                    entity.GetType().GetProperty("Isdeleted")?.SetValue(entity, true);
                }
            }

            return base.SaveChanges();
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "System";
            var username = _httpContextAccessor.HttpContext?.User.FindFirst("first_name")?.Value ?? "System";

            foreach (var entry in ChangeTracker.Entries())
            {
                var entity = entry.Entity;

                // Handle soft delete
                if (entry.State == EntityState.Deleted && entity is Isoftdelete)
                {
                    entry.State = EntityState.Modified; // Mark entity as modified instead of deleted
                    entity.GetType().GetProperty("Isdeleted")?.SetValue(entity, true);
                    entity.GetType().GetProperty("deletedbyId")?.SetValue(entity, userId);
                    entity.GetType().GetProperty("deleteddatetime")?.SetValue(entity, DateTime.UtcNow);
                }

                // Handle entity creation
                if (entry.State == EntityState.Added)
                {
                    entity.GetType().GetProperty("createdbyName")?.SetValue(entity, username);
                    entity.GetType().GetProperty("createdbyId")?.SetValue(entity, userId);
                    entity.GetType().GetProperty("createddatetime")?.SetValue(entity, DateTime.UtcNow);
                }

                // Handle entity modification
                if (entry.State == EntityState.Modified)
                {
                    entity.GetType().GetProperty("modifiedbyName")?.SetValue(entity, username);
                    entity.GetType().GetProperty("modifiedbyId")?.SetValue(entity, userId);
                    entity.GetType().GetProperty("modifiedDatetime")?.SetValue(entity, DateTime.UtcNow);
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }


    }
}


