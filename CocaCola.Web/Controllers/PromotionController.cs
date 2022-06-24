using CocaCola.Web.Models;
using CocaCola.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CocaCola.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PromotionController : Controller
    {
        private IPromotionService _promotionService;

        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        public async Task<IActionResult> Index()
        {
            Promotion promotion = new Promotion();
            promotion.Store = await GetStore();
            return View(promotion);
        }

        [HttpGet]
        [Route("GetStore")]
        public async Task<List<Store>> GetStore() {

            var result = await _promotionService.GetStore();

            return result;
        }

        [HttpPost]
        [Route("Submit")]
        public async Task<string> Submit([FromBody] PromotionDto data)
        {
            var filePath = await _promotionService.Submit(data);

            return "File uploaded to " + filePath;

            //return Ok();
        }

        [HttpPost]
        [Route("GetItemFromFile")]
        public async Task<List<string>> GetItemFromFile(IFormFile file)
        {
            List<string> result = await _promotionService.GetItemFromFile(file);

            return result;
        }
    }
}
