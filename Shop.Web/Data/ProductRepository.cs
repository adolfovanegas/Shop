namespace Shop.Web.Data
{
    using Shop.Web.Data.Entities;

    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context)
        {
        }
    }

}
