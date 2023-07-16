using Console_Project.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Project.Abstract
{
    public interface IMarketable
    {
        public int AddProduct(string productName, int productCount, decimal productPrice, string category);
        void UpdateProduct(int newCode, string newName, decimal newPrice, int newCount, string newCategory);
        void RemoveProduct(int codeOfProduct);
        List<Product> ShowAllProducts();
        void ShowProductsofCategories(string category);
        void ShowProductsPriceRange(decimal startPrice, decimal endPrice);
        void SearchWithName(string name);
        void AddSale(int number);
        void DeleteSaleItemInSale(int saleID, int saleItemID, int saleItemCount);
        void DeleteSalebyID(int saleID);
        void ShowAllSales();
        void ShowSaleByDateRange(DateTime startDate, DateTime endDate);
        void ShowSaleByPriceRange(decimal startPrice, decimal endPrice);
        void ShowSalebyDate(DateTime date);
        void ShowSalebyCode(int ID);
    }

}
