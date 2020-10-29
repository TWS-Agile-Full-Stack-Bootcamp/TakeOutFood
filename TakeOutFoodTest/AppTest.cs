using System.Collections.Generic;
using TakeOutFood;
using Xunit;

namespace TakeOutFoodTest
{
    public class AppTest
    {
        [Fact]
        public void Should_use_50_percentage_sales_promotion()
        {
            List<string> inputs = new List<string>() { "ITEM0001 x 1", "ITEM0013 x 2", "ITEM0022 x 1" };
            App app = new App(new ItemRepositoryTestImpl(), new SalesPromotionRepositoryTestImpl());
            string receiptstring = app.BestCharge(inputs);

            Assert.Equal("============= Order details =============\n" +
                    "Braised chicken x 1 = 18 yuan\n" +
                    "Chinese hamburger x 2 = 12 yuan\n" +
                    "Cold noodles x 1 = 8 yuan\n" +
                    "-----------------------------------\n" +
                    "Promotion used:\n" +
                    "Half price for certain dishes (Braised chicken，Cold noodles)，saving 13 yuan\n" +
                    "-----------------------------------\n" +
                    "Total：25 yuan\n" +
                    "===================================", receiptstring);
        }

        [Fact]
        public void Should_use_buy_30_save_6_sales_promotion()
        {
            List<string> inputs = new List<string>() { "ITEM0013 x 4", "ITEM0022 x 1" };

            App app = new App(new ItemRepositoryTestImpl(), new SalesPromotionRepositoryTestImpl());
            string receiptstring = app.BestCharge(inputs);

            Assert.Equal("============= Order details =============\n" +
                    "Chinese hamburger x 4 = 24 yuan\n" +
                    "Cold noodles x 1 = 8 yuan\n" +
                    "-----------------------------------\n" +
                    "Promotion used:\n" +
                    "满30减6 yuan，saving 6 yuan\n" +
                    "-----------------------------------\n" +
                    "Total：26 yuan\n" +
                    "===================================", receiptstring, );
        }

        [Fact]
        public void Should_use_no_sales_promotion()
        {
            List<string> inputs = new List<string> { "ITEM0013 x 4" };

            App app = new App(new ItemRepositoryTestImpl(), new SalesPromotionRepositoryTestImpl());
            string receiptstring = app.BestCharge(inputs);

            Assert.Equal("============= Order details =============\n" +
                    "Chinese hamburger x 4 = 24 yuan\n" +
                    "-----------------------------------\n" +
                    "Total：24 yuan\n" +
                    "===================================", receiptstring);
        }
    }
}
