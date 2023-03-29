using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Services
{
    public class OrderService
    {
        private readonly ISWildberriesContext _context;

        public OrderService(ISWildberriesContext context)
        {
            _context = context;
        }

        public dynamic GetOrderByOrderId(int OrderId)
        {
            return _context.Orders.Where(o => o.Id == OrderId).Include(o => o.User).Include(o => o.Product).FirstOrDefault();
        }
    }
}
