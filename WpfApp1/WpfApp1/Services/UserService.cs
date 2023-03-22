using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using WpfApp1.Models;

namespace WpfApp1.Services
{
    public class UserService
    {
        private readonly ISWildberriesContext _context;

        public UserService(ISWildberriesContext context)
        {
            _context = context;
        }

        public ObservableCollection<dynamic> GetUserGeneralInfo()
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

        public ObservableCollection<dynamic> GetUserAvgCost()
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

    }
}
