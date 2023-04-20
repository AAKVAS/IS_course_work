using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using WpfApp1.Models;

namespace WpfApp1.Services
{
    /// <summary>
    /// Класс, предоставляющий методы для работы с данными, связанными с пользователями.
    /// </summary>
    public class UserService
    {
        private static readonly ISWildberriesContext _context = App.Context;

        /// <summary>
        /// Метод, возвращающий сведения о пользователях.
        /// </summary>
        /// <returns>Пользователи.</returns>
        public static ObservableCollection<dynamic> GetUserGeneralInfo()
        {
            FormattableString query = $@"SELECT 
	                                            u.id,
	                                            u.lastname,
	                                            u.firstname,
	                                            u.patronymic,
	                                            u.phone_number,
	                                            u.birthday,
	                                            u.email,
                                                u.order_code,
                                                u.is_male,
                                                u.country_id
                                            FROM users u";
            return new ObservableCollection<dynamic>(_context.Users
                    .FromSql(query)
                    .Include(u => u.Country)
                    .ToList());
        }

        /// <summary>
        /// Метод, возвращающий средние затраты пользователей.
        /// </summary>
        /// <returns>Средние затраты пользователей.</returns>
        public static ObservableCollection<dynamic> GetUserAvgCost()
        {
            FormattableString query = $@"SELECT u.id, 
                                                u.firstname, 
                                                u.lastname, 
                                                u.patronymic, 
                                                ISNULL(AVG(o.price), 0) as AvgCost
                                            FROM users u
                                                LEFT JOIN orders o ON o.user_id = u.id
                                            GROUP BY 
                                                u.id, 
                                                u.firstname, 
                                                u.lastname, 
                                                u.patronymic";
            return new ObservableCollection<dynamic>(_context.UserAverageCostDTO
                .FromSql(query)
                .ToList());
        }

        /// <summary>
        /// Метод, возвращающий товары пользователей в корзине.
        /// </summary>
        /// <returns>Товары пользователей в корзине.</returns>
        public static ObservableCollection<dynamic> GetUserDeferredProducts()
        {
            return new ObservableCollection<dynamic>(_context.DeferredProducts
                .Include(dp => dp.User)
                .Include(dp => dp.Product)
                .ToList());
        }

        /// <summary>
        /// Метод, возвращающий товар в корзине вместе с его изображениями.
        /// В качетсве параметра принимает товар в корзине.
        /// </summary>
        /// <param name="deferredProduct">Товар в корзине.</param>
        /// <returns>Товар в корзине с его изображениями.</returns>
        public static DeferredProducts GetDeferredProductsWithProductImages(DeferredProducts deferredProduct)
        {
            return _context.DeferredProducts.Where(dp => dp.Id == deferredProduct.Id).Include(dp => dp.Product).ThenInclude(p => p.Images).FirstOrDefault()
                ?? new DeferredProducts();
        }
    }
}
