using Microsoft.EntityFrameworkCore;
using StockManagement.Api.Contract.Entities;
using StockManagement.Api.Contract.Helpers;
using StockManagement.Api.Contract.Models;

namespace StockManagement.Api.DAL
{
    public static class SeedData
    {
        public static void SeedInitialData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasData(FirstUser);
            modelBuilder.Entity<UserEntity>().HasData(SecondUser);
            modelBuilder.Entity<UserEntity>().HasData(AdminUser);

            modelBuilder.Entity<VarietyEntity>().HasData(AlbaVariety);
            modelBuilder.Entity<VarietyEntity>().HasData(ErikaVariety);
            modelBuilder.Entity<VarietyEntity>().HasData(AmiraVariety);
            modelBuilder.Entity<VarietyEntity>().HasData(TestVariety);

            AddFruit(modelBuilder, RaspberryErikaFruit);
            AddFruit(modelBuilder, RaspberryAmiraFruit);
            AddFruit(modelBuilder, BlueberryAlbaFruit);
            AddFruit(modelBuilder, RaspberryTestFruit);

            AddBatch(modelBuilder, FirstBatch);
            AddBatch(modelBuilder, SecondBatch);
            AddBatch(modelBuilder, ThirdBatch);
            AddBatch(modelBuilder, FourthBatch);
        }

        public static void AddBatch(ModelBuilder modelBuilder, BatchEntity batch)
        {
            modelBuilder.Entity<BatchEntity>().HasData(new BatchEntity
            {
                Id = batch.Id,
                Quantity = batch.Quantity,
                FruitId = batch.Fruit.Id
            });
        }

        public static void AddFruit(ModelBuilder modelBuilder, FruitEntity fruit)
        {
            modelBuilder.Entity<FruitEntity>().HasData(new FruitEntity
            {
                Id = fruit.Id,
                Name = fruit.Name,
                VarietyId = fruit.Variety.Id
            });
        }

        #region Fruits 

        public static FruitEntity BlueberryAlbaFruit
        {
            get
            {
                var entity = new FruitEntity
                {
                    Id = 1,
                    Name = "Blueberry",
                    Variety = AlbaVariety
                };

                return entity;
            }
        }

        public static FruitEntity RaspberryAmiraFruit
        {
            get
            {
                var entity = new FruitEntity
                {
                    Id = 2,
                    Name = "Raspberry",
                    Variety = AmiraVariety
                };

                return entity;
            }
        }

        public static FruitEntity RaspberryErikaFruit
        {
            get
            {
                var entity = new FruitEntity
                {
                    Id = 3,
                    Name = "Raspberry",
                    Variety = ErikaVariety
                };

                return entity;
            }
        }

        public static FruitEntity RaspberryTestFruit
        {
            get
            {
                var entity = new FruitEntity
                {
                    Id = 4,
                    Name = "Raspberry",
                    Variety = TestVariety
                };

                return entity;
            }
        }

        #endregion

        #region Variety

        public static VarietyEntity AmiraVariety
        {
            get
            {
                var entity = new VarietyEntity
                {
                    Id = 1,
                    Name = "Amira"
                };
                return entity;
            }
        }

        public static VarietyEntity AlbaVariety
        {
            get
            {
                var entity = new VarietyEntity
                {
                    Id = 2,
                    Name = "Alba"
                };
                return entity;
            }
        }

        public static VarietyEntity ErikaVariety
        {
            get
            {
                var entity = new VarietyEntity
                {
                    Id = 3,
                    Name = "Erika"
                };
                return entity;
            }
        }

        public static VarietyEntity TestVariety
        {
            get
            {
                var entity = new VarietyEntity
                {
                    Id = 4,
                    Name = "Test"
                };
                return entity;
            }
        }

        #endregion

        #region Batch

        public static BatchEntity FirstBatch
        {
            get
            {
                var entity = new BatchEntity
                {
                    Id = 1,
                    Fruit = RaspberryAmiraFruit,
                    Quantity = 12
                };

                return entity;
            }
        }

        public static BatchEntity SecondBatch
        {
            get
            {
                var entity = new BatchEntity
                {
                    Id = 2,
                    Fruit = RaspberryErikaFruit,
                    Quantity = 10
                };

                return entity;
            }
        }

        public static BatchEntity ThirdBatch
        {
            get
            {
                var entity = new BatchEntity
                {
                    Id = 3,
                    Fruit = RaspberryAmiraFruit,
                    Quantity = 10
                };

                return entity;
            }
        }

        public static BatchEntity FourthBatch
        {
            get
            {
                var entity = new BatchEntity
                {
                    Id = 4,
                    Fruit = BlueberryAlbaFruit,
                    Quantity = 15
                };

                return entity;
            }
        }

        #endregion

        #region Users

        public static UserEntity FirstUser
        {
            get
            {
                PasswordHelper.CreatePasswordHash("Password", out var passwordHash, out var passwordSalt);

                var entity = new UserEntity
                {
                    Id = 1,
                    Username = "FirstUser",
                    FirstName = "First",
                    LastName = "First",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Role = Role.User
                };

                return entity;
            }
        }

        public static UserEntity SecondUser
        {
            get
            {
                PasswordHelper.CreatePasswordHash("Password", out var passwordHash, out var passwordSalt);

                var entity = new UserEntity
                {
                    Id = 2,
                    Username = "SecondUser",
                    FirstName = "Second",
                    LastName = "Second",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Role = Role.User
                };

                return entity;
            }
        }

        public static UserEntity AdminUser
        {
            get
            {
                PasswordHelper.CreatePasswordHash("admin", out var passwordHash, out var passwordSalt);

                var entity = new UserEntity
                {
                    Id = 3,
                    Username = "admin",
                    FirstName = "Second",
                    LastName = "Second",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Role = Role.Admin
                };

                return entity;
            }
        }

        #endregion
    }
}