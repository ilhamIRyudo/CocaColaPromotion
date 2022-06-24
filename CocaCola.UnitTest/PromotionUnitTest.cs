using CocaCola.Web.Controllers;
using CocaCola.Web.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocaCola.UnitTest
{
    public class PromotionUnitTest
    {
        private IPromotionService _promotionService;

        public PromotionUnitTest(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        [Fact]
        public async void TestGetStoreNotNull()
        {
            string workingDirectory = Environment.CurrentDirectory;

            // This will get the current PROJECT directory
            string projectDirectory2 = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            var path = projectDirectory2 + @"\Database\Stores.xlsx";

            var store = await _promotionService.GetStore(path);
            Assert.NotNull(store);
            Assert.NotEmpty(store);
        }
    }
}
