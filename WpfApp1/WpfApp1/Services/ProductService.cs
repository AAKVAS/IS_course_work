using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WpfApp1.Models;

namespace WpfApp1.Services
{
    /// <summary>
    /// Класс, предоставляющий методы работы с данными, связанными с товарами.
    /// </summary>
    public class ProductService
    {
        private static readonly ISWildberriesContext _context = App.Context;

        /// <summary>
        /// Метод, возвращающий коллекцию товаров для раздела "Товары / Общие сведения".
        /// </summary>
        /// <returns>Коллекия товаров.</returns>
        public static ObservableCollection<dynamic> GetProductsGeneralInfo()
        {
            return new ObservableCollection<dynamic>(_context.Products
                    .Include(p => p.Supplier)
                    .Include(p => p.Category)
                    .ToList());
        }

        /// <summary>
        /// Метод, возвращающий коллекцию отзывов к товарам для раздела "Товары / Отзывы к товару".
        /// </summary>
        /// <returns>Отзывы к товарам.</returns>
        public static ObservableCollection<dynamic> GetProductsReviews()
        {
            return new ObservableCollection<dynamic>(_context.Reviews.Include(r => r.Order).ThenInclude(o => o.User).Include(r => r.Order).ThenInclude(o => o.Product));
        }

        /// <summary>
        /// Метод, возвращающий коллекцию изображений к отзыву к товару.
        /// </summary>
        /// <param name="review">Отзыв.</param>
        /// <returns>Изображения отзыва.</returns>
        public static ObservableCollection<dynamic> GetReviewImages(Reviews review)
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

        /// <summary>
        /// Метод, возвращающий товар с его изображениями.
        /// В качестве параметра принимает модель товара.
        /// </summary>
        /// <param name="product">Товар.</param>
        /// <returns>Товар с его изображениями.</returns>
        public static Products GetProductWithImages(Products product)
        {
            return _context.Products.Where(p => p.Equals(product)).Include(p => p.Images).ToList().FirstOrDefault() ?? new Products();
        }

        /// <summary>
        /// Метод, возвращающий отзыв к доствке по её Id.
        /// </summary>
        /// <param name="orderId">Id доставки.</param>
        /// <returns>Отзыв к доставке.</returns>
        public static Reviews GetReviewByOrderId(int orderId)
        {
            return _context.Reviews.Where(r => r.OrderId == orderId).Include(r => r.Order).ThenInclude(o => o.User).Include(r => r.Order).ThenInclude(o => o.Product).FirstOrDefault();
        }

        /// <summary>
        /// Метод, возвращающий отзыв с его изображениями.
        /// В качестве параметра принимается модель отзыва.
        /// </summary>
        /// <param name="review">Отзыв.</param>
        /// <returns>Отзыв с его изображениями.</returns>
        public static Reviews GetReviewsWithImages(Reviews review)
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

        /// <summary>
        /// Метод, возвращающий коллекцию категорий товаров.
        /// </summary>
        /// <returns>Категории товаров.</returns>
        public static List<Categories> GetCategories()
        {
            return _context.Categories.Include(c => c.ParentCategory).ToList();
        }

        /// <summary>
        /// Метод, возвращающий коллекцию истории цен на товары.
        /// </summary>
        /// <returns>История цен на товары.</returns>
        public static ObservableCollection<dynamic> GetPriceHistory()
        {
            return new ObservableCollection<dynamic>(_context.PriceHistory.Include(ph => ph.Product).ToList());
        }
    }
}
