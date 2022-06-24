namespace CocaCola.Web.Models
{
    public class Promotion
    {
        public Promotion()
        {
            Store = new List<Store>();
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }

        public string Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ValueType { get; set; }
        public List<Store> Store { get; set; }
    }
}
