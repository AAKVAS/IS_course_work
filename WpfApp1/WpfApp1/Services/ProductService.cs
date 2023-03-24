using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using WpfApp1.Models;

namespace WpfApp1.Services
{
    public class ProductService
    {
        private readonly ISWildberriesContext _context;

        public ProductService(ISWildberriesContext context)
        {
            _context = context;
        }

        public ObservableCollection<dynamic> GetProductsGeneralInfo()
        {
            return new ObservableCollection<dynamic>(_context.Products
                    .Include(p => p.Supplier)
                    .Include(p => p.Category)
                    .ToList());
        }

        public Products GetProductWithImages(Products product)
        {
            return _context.Products.Where(p => p.Equals(product)).Include(p => p.Images).ToList().FirstOrDefault();
        }

        public ObservableCollection<dynamic> GetProductImages(Products product)
        {
            return new ObservableCollection<dynamic>(_context.ProductImages.Where(p => p.Product.Equals(product)).Include(p => p.Product).ToList());
        }



    }
}
