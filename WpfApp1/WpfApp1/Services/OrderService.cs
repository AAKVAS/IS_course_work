using Microsoft.Data.SqlClient;
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
    public class OrderService
    {
        private static readonly ISWildberriesContext _context = new ISWildberriesContext();

        public dynamic GetOrderByOrderId(int OrderId)
        {
            return _context.Orders.Where(o => o.Id == OrderId).Include(o => o.User).Include(o => o.Product).FirstOrDefault();
        }

        public ObservableCollection<dynamic> GetOrdersList()
        {
            return new ObservableCollection<dynamic>(_context.Orders
                .Include(o => o.User)
                .Include(o => o.Product)
                .Include(o => o.PickUpPoint)
                .ToList());
        } 

        public void InsertOrder(Orders order)
        {
            string query = @"INSERT INTO orders (
                                               user_id,
                                               product_id,
	                                           product_count,
	                                           pick_up_point_id,
                                               created_at,
	                                           estimated_delivery_at
                                        )
                                        VALUES (
                                               @user_id,
                                               @product_id,
	                                           @product_count,
	                                           @pick_up_point_id,
                                               @created_at,
	                                           @estimated_delivery_at
                                        )";
            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@user_id", order.User.Id),
                new SqlParameter("@product_id", order.Product.Id),
                new SqlParameter("@product_count", order.ProductCount),
                new SqlParameter("@pick_up_point_id", order.PickUpPoint.Id),
                new SqlParameter("@created_at", order.CreatedAt),
                new SqlParameter("@estimated_delivery_at", order.EstimatedDeliveryAt)
            };

            _context.Database.ExecuteSqlRaw(query, parameters);
            _context.SaveChanges();

        }

        public ObservableCollection<dynamic> GetOrdersReadyToReceive()
        {
            const int readyToReceiveStatus = 11;
            return new ObservableCollection<dynamic>(
                _context.OrderHistory
                    .Where(oh => oh.IsLastStatus ?? false && oh.StatusId == readyToReceiveStatus)
                    .Include(oh => oh.Order)
                        .ThenInclude(o => o.User)
                    .Include(oh => oh.Order)
                        .ThenInclude(o => o.Product)
                    .Include(oh => oh.Order)
                        .ThenInclude(o => o.PickUpPoint)
                    .Include(oh => oh.Status)
                        .ToList());
        }
    }
}
