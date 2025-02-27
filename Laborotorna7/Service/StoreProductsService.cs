
namespace Laborotorna7.Service
{
    public interface IStoreProdictService
    {
        Task<List<StoreProducts>> GetStoreProducts();
        Task<StoreProducts> CreateProducts(StoreProducts products);

        Task<StoreProducts> UpdateProducts(string model, string manufacture, StoreProducts products);

        Task<bool> DeleteProducts(string model, string manufacture);
    }
    public class StoreProductsService :IStoreProdictService
    {
        private readonly List<StoreProducts> _products;
        
        public StoreProductsService()
        {
            _products = new List<StoreProducts> {
                new StoreProducts { Model = "130/900 EQ2", Manufacture = "Arsenal", Price = 20.16, Type = "Reflector", Count = 7, Type_item = "telescops" },
                new StoreProducts { Model = "GSO 8 \"Dobson Deluxe\"", Manufacture = "Arsenal", Price = 47.38, Type = "Reflector", Count = 1, Type_item = "telescops" },
                new StoreProducts { Model = "70AZ2", Manufacture = "Sky-Watcher", Price = 7.70, Type = "Refraktor", Count = 10, Type_item = "telescops" },
                new StoreProducts { Model = "10x50 Porro", Manufacture = "Sky-Watcher", Price = 3.36, Type = "Porro", Count = 2, Type_item = "binoculars" },
                new StoreProducts { Model = "Crossfire HD 12x50 WP", Manufacture = "Vortex", Price = 11.99, Type = "Roof", Count = 5, Type_item = "binoculars" },
                new StoreProducts { Model = "Konusarmy 10x50 WA", Manufacture = "Konus", Price = 2.20, Type = "Porro", Count = 2, Type_item = "binoculars" },
                new StoreProducts { Model = "150/750 EQ3", Manufacture = "Sky-Watcher", Price = 35.50, Type = "Reflector", Count = 3, Type_item = "telescops" },
                new StoreProducts { Model = "8x42 Monarch", Manufacture = "Nikon", Price = 15.75, Type = "Roof", Count = 4, Type_item = "binoculars" },
                new StoreProducts { Model = "114/900 AZ", Manufacture = "Celestron", Price = 18.90, Type = "Reflector", Count = 6, Type_item = "telescops" },
                new StoreProducts { Model = "Prostaff 7S 10x42", Manufacture = "Nikon", Price = 13.25, Type = "Roof", Count = 8, Type_item = "binoculars" }
            };
        }

        public async Task<List<StoreProducts>> GetStoreProducts()
        {
            return await Task.FromResult(_products);
        }

        public async Task<StoreProducts> CreateProducts(StoreProducts storeProducts)
        {

            if (storeProducts == null) throw new ArgumentNullException(nameof(storeProducts));

            if (_products.Any(p => p.Model == storeProducts.Model.Trim() && p.Manufacture == storeProducts.Manufacture.Trim())) throw new InvalidOperationException("products with param already exists");

            _products.Add(storeProducts);

            return await Task.FromResult(storeProducts);
        }

        public async Task<StoreProducts> UpdateProducts(string model, string manufacture, StoreProducts products)
        {
            if(string.IsNullOrEmpty(model) || string.IsNullOrEmpty(manufacture))
             {
                throw new ArgumentNullException("param must not be empty");
            }

            var findProducts = _products.FindIndex(i => i.Model == model.Trim() && i.Manufacture == manufacture.Trim());
            if (findProducts == -1)
            {
                return null;
            }

            _products[findProducts] = products; 

            return await Task.FromResult(products);
        }

        public async Task<bool> DeleteProducts(string model, string manufacture)
        {
            if (string.IsNullOrEmpty(model) || string.IsNullOrEmpty(manufacture))
            {
                throw new ArgumentNullException("param must not be empty");
            }

            var findProducts = _products.FirstOrDefault(i => i.Model == model.Trim() && i.Manufacture == manufacture.Trim());
            if (findProducts == null)
            {
                return false;
            }
            _products.Remove(findProducts);
            return await Task.FromResult(true);
        }
    }
}
