using CocaCola.Web.Models;

namespace CocaCola.Web.Services
{
    public interface IPromotionService
    {
        Task<List<Store>> GetStore(string path);
        Task<string> Submit(PromotionDto data);
        Task<List<string>> GetItemFromFile(IFormFile file);
    }
}
