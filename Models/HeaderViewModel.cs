namespace FashKartWebsite.Models
{
    public class HeaderViewModel
    {
       public List<BasketViewModel> Basket { get; set; } = new List<BasketViewModel>();
        public int Count { get; set; }
        public double Sum {  get; set; }
    }
}
