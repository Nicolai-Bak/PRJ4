namespace ApiApplication.SearchAlgorithm.Models
{
    public class StoreSearch
    {
        public int StoreID { get; set; }
        public double TotalPrice { get; set; }
        public double Distance { get; set; }
        public List<ProductSearch> Products { get; set; }

        public StoreSearch(int id)
        {
            StoreID = id;
        }

        public void Add(ProductSearch p)
        {
            Products.Add(p);
            TotalPrice += p.Price * p.Amount;
        }
        
    }
}
