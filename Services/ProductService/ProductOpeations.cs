using Console_Project.Common.Enum;
using Console_Project.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Console_Project.Services.ProductService
{
    public class ProductOpeations
    {
        public List<Product> product;
        public List<Sale> sale;
        public List<SaleItem> saleitem;
        public ProductOpeations()
        {
            product = new();
            sale = new();
            saleitem = new();
        }
        public int AddProduct(string productName, int productCount, decimal productPrice, string category)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new FormatException("Error... Name is empty!");

            if (productCount <= 0)
                throw new FormatException("Count can`t be lower than zero or equal zero!");

            if (productPrice < 0)
                throw new FormatException("Price is lower than 0!");
            
            if(string.IsNullOrWhiteSpace(category))
                throw new FormatException("Category is empty!");

            bool isSuccessful
                = Enum.TryParse(typeof(Categories), category, true, out object parsedCategories);

            if (!isSuccessful) {
                throw new InvalidDataException("Category cannot be found");
            }
            var NewProduct = new Product
            {
                ProdcutName = productName,
                ProductCount = productCount,
                ProductPrice = productPrice,
                Categories = (Categories)parsedCategories
            };
            product.Add(NewProduct);
            return NewProduct.Code;
        }

        public void UpdateProduct(int newCode , string newName , decimal newPrice , int newCount, string newCategory) 
        {
            var selectedProduct = product.FirstOrDefault(x => x.Code == newCode);
            
            if (selectedProduct == null)
                throw new Exception($"Product code {newCode} not found!");
           
            bool isSuccessful
                = Enum.TryParse(typeof(Categories), newCategory, true, out object newparsedCategories);
            if (!isSuccessful)
            {
                throw new Exception("This category not found");
            }

            selectedProduct.ProductPrice = newPrice;
            selectedProduct.ProdcutName = newName;
            selectedProduct.ProductCount = newCount;
            selectedProduct.Categories = (Categories)newparsedCategories;
        }

        public void RemoveProduct(int CodeOfProduct)
        {
            var RemoveProduct = product.FirstOrDefault(x => x.Code == CodeOfProduct);

            if (RemoveProduct == null)
                throw new Exception($"{CodeOfProduct} Product not found! ");

            product = product.Where(x => x.Code != CodeOfProduct).ToList();
        }
        
        public List<Product> ShowAllProducts()
        {
            return product;
        }
    }
}
