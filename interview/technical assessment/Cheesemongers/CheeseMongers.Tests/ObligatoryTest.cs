using CheeseMongers;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Collections.Generic;
using Xunit;

namespace CheeseMongers.Tests
{
    public class ObligatoryTest
    {
        [Fact]
        public void When_Run_ItemStructure_Immutable()
        {
            // Arrange
            var originalItem = new CheeseMongersItem
            {
                Name = "Standard Cheese",
                ValidByDays = 10,
                Quality = 20
            };

            var clone = new CheeseMongersItem
            {
                Name = originalItem.Name,
                ValidByDays = originalItem.ValidByDays,
                Quality = originalItem.Quality
            };

            var items = new List<CheeseMongersItem> { originalItem };
            var program = new Program(items);

            // Act
            program.UpdateQuality();

            Assert.Equal(clone.Name, originalItem.Name);
        }

        [Fact]
        public void When_CaciocavalloPodolico_Then_ValidByDaysNeverChanged()
        {
            // Arrange
            var cacio = new CheeseMongersItem
            {
                Name = "Caciocavallo Podolico",
                ValidByDays = 10,
                Quality = 20
            };
            var items = new List<CheeseMongersItem> { cacio };
            var program = new Program(items);

            int validByDaysBefore = cacio.ValidByDays;

            // Act
            program.UpdateQuality();

            // Assert
            Assert.Equal(validByDaysBefore, cacio.ValidByDays);
        }

        [Fact]
        public void When_ValidByDaysPassed_Then_QualityDegradesFiveTimesAsFast()
        {
            // Arrange
            var items = new List<CheeseMongersItem>
            {
                new CheeseMongersItem
                {
                    Name = "Standard Cheese",
                    ValidByDays = 0, // Expired
                    Quality = 25
                }
            };
            var program = new Program(items);

            // Act
            program.UpdateQuality();

            // Assert
            Assert.Equal(20, items[0].Quality); // 25 - 5 = 20
        }


        [Fact]
        public void When_ThereAre14DaysOrLess_Then_QualityOfPass_IncreaseBy3()
        {
            // Arrange
            var item = new CheeseMongersItem
            {
                Name = "Tasting with Chef Massimo",
                ValidByDays = 14,
                Quality = 50
            };

            var program = new Program(new List<CheeseMongersItem> { item });

            // Act
            program.UpdateQuality();

            // Assert
            Assert.Equal(53, item.Quality); // +3 expected
        }


        [Fact]
        public void When_ThereAre7DaysOrLess_Then_QualityOfPass_IncreaseBy5()
        {
            // Arrange
            var items = new List<CheeseMongersItem>
            {
                new CheeseMongersItem
                {
                    Name = "Tasting with Chef Massimo",
                    ValidByDays = 5,
                    Quality = 90
                }
            };

            var program = new Program(items);

            // Act
            program.UpdateQuality();

            // Assert
            Assert.Equal(95, items[0].Quality); // Should increase by 5
            Assert.Equal(4, items[0].ValidByDays); // Decrease by 1
        }


        [Fact]
        public void When_QualityUpdated_Then_QualityNeverIncreasedOver100()
        {
            // Arrange
            var items = new List<CheeseMongersItem>
            {
                new CheeseMongersItem { Name = "Parmigiano Regiano", ValidByDays = 5, Quality = 100 },
                new CheeseMongersItem { Name = "Tasting with Chef Massimo", ValidByDays = 5, Quality = 98 },
                new CheeseMongersItem { Name = "Standard Cheese", ValidByDays = 3, Quality = 95 },
                new CheeseMongersItem { Name = "Ricotta", ValidByDays = 1, Quality = 99 }
            };

            var app = new Program(items);

            // Act
            app.UpdateQuality();

            // Assert
            foreach (var item in items)
            {
                Assert.True(item.Quality <= 100, $"Item '{item.Name}' exceeded quality 100 with value {item.Quality}");
            }
        }

        [Fact]
        public void When_CaciocavalloPodolico_Then_QualityNeverDecreased()
        {
            // Arrange
            var items = new List<CheeseMongersItem>
            {
                new CheeseMongersItem { Name = "Caciocavallo Podolico", ValidByDays = 10, Quality = 50 }
            };
            var program = new Program(items);

            // Act
            int initialQuality = items[0].Quality;
            for (int day = 0; day < 10; day++)
            {
                program.UpdateQuality();
            }

            // Assert
            Assert.True(items[0].Quality >= initialQuality, "Quality should never decrease for Caciocavallo Podolico");
        }

        [Fact]
        public void When_Ricotta_BeforeExpiry_Then_QualityDecreasesBy3()
        {
            var items = new List<CheeseMongersItem>
            {
                new CheeseMongersItem { Name = "Ricotta", ValidByDays = 2, Quality = 10 }
            };

            var program = new Program(items);
            program.UpdateQuality();

            Assert.Equal(7, items[0].Quality);
            Assert.Equal(1, items[0].ValidByDays);
        }

        [Fact]
        public void When_Ricotta_OnExpiryDay_Then_QualityDecreasesBy3()
        {
            var items = new List<CheeseMongersItem>
            {
                new CheeseMongersItem { Name = "Ricotta", ValidByDays = 0, Quality = 10 }
            };

            var program = new Program(items);
            program.UpdateQuality();

            Assert.Equal(7, items[0].Quality);
            Assert.Equal(-1, items[0].ValidByDays);
        }

        [Fact]
        public void When_Ricotta_AfterExpiry_Then_QualityDecreasesBy5()
        {
            var items = new List<CheeseMongersItem>
            {
                new CheeseMongersItem { Name = "Ricotta", ValidByDays = -1, Quality = 10 }
            };

            var program = new Program(items);
            program.UpdateQuality();

            Assert.Equal(5, items[0].Quality);
            Assert.Equal(-2, items[0].ValidByDays);
        }

        [Fact]
        public void When_Ricotta_QualityWouldGoBelowZero_Then_QualitySetToZero()
        {
            var items = new List<CheeseMongersItem>
            {
                new CheeseMongersItem { Name = "Ricotta", ValidByDays = -1, Quality = 3 }
            };

            var program = new Program(items);
            program.UpdateQuality();

            Assert.Equal(0, items[0].Quality);
            Assert.Equal(-2, items[0].ValidByDays);
        }


    }

}

