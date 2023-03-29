using Microsoft.EntityFrameworkCore;
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

        public ObservableCollection<dynamic> GetProductsReviews()
        {
            return new ObservableCollection<dynamic>(_context.Reviews.Include(r => r.Order).ThenInclude(o => o.User).Include(r => r.Order).ThenInclude(o => o.Product));
        }

        public ObservableCollection<dynamic> GetReviewImages(Reviews review)
        {
            if (review != null)
            {
                return new ObservableCollection<dynamic>(_context.ReviewImages.Where(ri => ri.Review.Equals(review)).Include(p => p.Review).ToList());
            }
            else
            {
                return new ObservableCollection<dynamic> { };
            }
        }

        public Products GetProductWithImages(Products product)
        {
            return _context.Products.Where(p => p.Equals(product)).Include(p => p.Images).ToList().FirstOrDefault() ?? new Products();
        }

        public Reviews GetReviewByOrderId(int OrderId)
        {
            return _context.Reviews.Where(r => r.OrderId == OrderId).Include(r => r.Order).ThenInclude(o => o.User).Include(r => r.Order).ThenInclude(o => o.Product).FirstOrDefault();
        }

        public Reviews GetReviewsWithImages(Reviews review)
        {
            return _context.Reviews
                .Where(r => r.OrderId == review.OrderId)
                .Include(r => r.Order)
                    .ThenInclude(o => o.User)
                .Include(r => r.Order)
                    .ThenInclude(o => o.Product)
                .Include(r => r.Images)
                .ToList()
                .FirstOrDefault() ?? new Reviews();
        }

    }
}
