namespace Shop.Web.Data
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Shop.Web.Data.Entities;

    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DataContext context;

        public ProductRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IQueryable<Product> GetAllWithUsers()
        {
            return context.Products.Include(p=>p.User);
        }
    }

}
