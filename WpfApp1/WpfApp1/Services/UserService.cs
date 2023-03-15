using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                                    u.country_id,
	                                IIF(u.is_male = 1, 'мужчина', 'женщина') as sex
                                FROM users u";
            return new ObservableCollection<dynamic>(_context.Users
                    .FromSql(query)
                    .Include(u => u.Country)
                    .ToList());
        }
    }
}
