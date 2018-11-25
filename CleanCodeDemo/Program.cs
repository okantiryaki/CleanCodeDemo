using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCodeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IProductService productService = new ProductManager(new IsBankAdapter());

            productService.Sell(new Product { ProductId = 1, ProductName = "Laptop", Price = 1000 },
                new Student { Id = 1, Name = "Okan", DiscountRate = 0.90m });
        }
    }

    class CustomerBase : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class Customer : CustomerBase
    {
        public string CustomerType { get; set; }
        public decimal DiscountRate { get; set; }
    }

    class Student : Customer
    {
        
    }

    class Officer : Customer
    {

    }

    class Product : IEntity
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }
    }

    class ProductManager : IProductService
    {
        private IBankService _bankService;

        public ProductManager(IBankService bankService)
        {
            _bankService = bankService;
        }

        public void Sell(Product product, Customer stundentCustomer)
        {
            decimal price = product.Price;

            price = product.Price * (decimal)stundentCustomer.DiscountRate;

            price = _bankService.ConvertRate(new CurrencyRate { Currency = 1, Price = price });

            Console.WriteLine(price);
            Console.ReadLine();
        }

    }

    internal interface IProductService
    {
        void Sell(Product product, Customer customer);

    }

    class CentralBankAdapter : IBankService
    {
        public decimal ConvertRate(CurrencyRate currencyRate)
        {
            CentralBankSerice centralBankSerice = new CentralBankSerice();
            return centralBankSerice.ConvertCuurency(currencyRate);
        }
    }

    class IsBankAdapter : IBankService
    {
        public decimal ConvertRate(CurrencyRate currencyRate)
        {
            IsBankSerice ısBankSerice = new IsBankSerice();
            return ısBankSerice.ConvertCuurency(currencyRate);
        }
    }

    class FakeBankService : IBankService
    {
        public decimal ConvertRate(CurrencyRate currencyRate)
        {
            return currencyRate.Price / 5.30m;
        }
    }

    internal interface IBankService
    {
        decimal ConvertRate(CurrencyRate currencyRate);
    }

    class CurrencyRate
    {
        public decimal Price { get; set; }
        public int Currency { get; set; }
    }

    class CentralBankSerice
    {
        public decimal ConvertCuurency(CurrencyRate currencyRate)
        {
            return currencyRate.Price / (decimal)5.28;
        }
    }

    class IsBankSerice
    {
        public decimal ConvertCuurency(CurrencyRate currencyRate)
        {
            return currencyRate.Price / (decimal)1;
        }
    }
}
