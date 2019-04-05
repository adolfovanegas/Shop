namespace Shop.Web.Data
{
    using Entities;
    using Helpers;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        private readonly Random random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            var user = await this.userHelper.GetUserByEmailAsync("adovan0608@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Adolfo",
                    LastName = "Vanegas",
                    Email = "adovan0608@gmail.com",
                    UserName = "adovan0608@gmail.com",
                    PhoneNumber = "63784383"
                };

                var result = await this.userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("El usuario no pudo ser creado");
                }
            }

            if (!this.context.Products.Any())
            {
                this.AddProduct("Camisa para Dama", user);
                this.AddProduct("Camisa para Hombre", user);
                this.AddProduct("Blusa para Damas", user);
                await this.context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name, User user)
        {
            this.context.Products.Add(new Product
            {
                Name = name,
                Price = this.random.Next(100),
                IsAvailabe = true,
                Stock = this.random.Next(1000),
                User = user
            });
        }

    }
}
