namespace CocaCola.Web.Models
{
    public class PromotionDto
    {
        public PromotionDto()
        {
            Store = new List<string>();
            Items = new List<string>();
        }

        public string Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ValueType { get; set; }
        public List<string> Items { get; set; }
        public List<string> Store { get; set; }
    }
}
