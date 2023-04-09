using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;
using WpfApp1.Models.DTO;

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
        }

        public void DeleteOrder(Orders order)
        {
            string query = @"DELETE FROM orders
                              WHERE id = @id;";
            _context.Database.ExecuteSqlRaw(query, new SqlParameter("@id", order.Id));
        }

        public void ChangeOrderStatus(OrderHistory orderHistory)
        {
            string query = @"INSERT INTO order_history (
	                                order_id,
	                                status_id, 
	                                current_storage_id)
                                VALUES (
	                                @order_id, 
	                                @status_id, 
	                                @current_storage_id)";

            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@order_id", orderHistory.OrderId),
                new SqlParameter("@status_id", orderHistory.Status.Id),
                new SqlParameter("@current_storage_id", orderHistory.CurrentStorageId),
            };
            _context.Database.ExecuteSqlRaw(query, parameters);
        }

        public ObservableCollection<dynamic> GetOrdersReadyToReceive()
        {
            const int readyToReceiveStatus = 11;
            return new ObservableCollection<dynamic>(
                _context.OrderHistory
                    .Where(oh => (oh.IsLastStatus ?? false) && oh.StatusId == readyToReceiveStatus)
                    .Include(oh => oh.Order)
                        .ThenInclude(o => o.User)
                    .Include(oh => oh.Order)
                        .ThenInclude(o => o.Product)
                    .Include(oh => oh.Order)
                        .ThenInclude(o => o.PickUpPoint)
                    .Include(oh => oh.Status)
                        .ToList());
        }

        public OrderHistory GetOrderHistoryWithProductImages(OrderHistory orderHistory)
        {
            return _context.OrderHistory.Where(oh => oh.Equals(orderHistory))
                    .Include(oh => oh.Order)
                        .ThenInclude(o => o.User)
                    .Include(oh => oh.Order)
                        .ThenInclude(o => o.Product)
                            .ThenInclude(p => p.Images)
                    .Include(oh => oh.Order)
                        .ThenInclude(o => o.PickUpPoint)
                    .Include(oh => oh.Status)
                        .FirstOrDefault() ?? new OrderHistory();
        }

        public List<OrderStatuses> GetStatusesForReadyToRecieveOrder()
        {
            string query = @"SELECT os.id,
                                    os.description
                                FROM order_statuses os
                                WHERE os.id IN (11, 12, 13)";

            return _context.OrderStatuses.FromSqlRaw(query).ToList();
        }

        public ObservableCollection<dynamic> GetOrderHistory()
        {
            string query = @"
                            SELECT 
                                    o.id as OrderId,
                                    pr.id as ProductId,
                                    pr.title as ProductTitle,
                                    os.id as StatusId,
                                    os.description as StatusDescription,
                                    s.id as StorageId,
                                    s.storage_type as StorageType,
                                    st.title as StorageTitle,
                                    s.country as Country,
                                    s.federal_subject as FederalSubject,
                                    s.locality as Locality,
                                    s.street as Street,
                                    s.house_number as HouseNumber,
                                    oh.status_changed_at as StatusChangedAt,
                                    oh.is_last_status as IsLastStatus,
                                    ISNULL(w.id, 0) as WorkerIdNotNullable,
                                    w.firstname as WorkerFirstname,
                                    w.lastname as WorkerLastname,
                                    w.patronymic as WorkerPatronymic,
                                    p.title as WorkerPost
                                FROM 
                                     order_history oh 
                                JOIN orders o                   ON oh.order_id = o.id
                                JOIN products pr                ON pr.id = o.product_id
                                JOIN order_statuses os          ON os.id = oh.status_id
                                JOIN storages s                 ON s.id = oh.current_storage_id
                                JOIN storage_types st           ON st.id = s.storage_type
                                LEFT JOIN workers_in_orders wio ON oh.order_id = wio.order_id
                                                            AND wio.status_changed_at = oh.status_changed_at
                                LEFT JOIN workers w             ON w.id = wio.worker_id
                                LEFT JOIN posts p               ON p.id = w.post_id";

            return new ObservableCollection<dynamic>(_context.OrderHistoryDTO.FromSqlRaw(query).AsNoTracking().ToList());
        }

        public ObservableCollection<dynamic> GetWorkersInOrder(OrderHistoryDTO orderHistoryDTO)
        {
            string query = @"SELECT 
                                   oh.order_id as OrderId,
                                   oh.status_changed_at as StatusChangedAt,
                                   wio.worker_id as WorkerId,
	                               w.firstname as WorkerFirstname,
	                               w.lastname as WorkerLastname,
                                   w.patronymic as WorkerPatronymic,
	                               p.title as WorkerPost
                              FROM order_history oh 
                              JOIN workers_in_orders wio ON oh.order_id = wio.order_id
                                                        AND wio.status_changed_at = oh.status_changed_at
                              JOIN workers w             ON w.id = wio.worker_id
                              JOIN posts   p             ON p.id = w.post_id
                             WHERE oh.order_id = @id
                               AND oh.status_changed_at = @status_changed_at";

            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@id", orderHistoryDTO.OrderId),
                new SqlParameter("@status_changed_at", orderHistoryDTO.StatusChangedAt)
            };

            return new ObservableCollection<dynamic>(_context.WorkersInOrdersDTO.FromSqlRaw(query, parameters).ToList());
        }

        public QueryWithParameters GetInsertOrderHistoryQuery(OrderHistoryDTO orderHistoryDTO)
        {
            string query = @"INSERT INTO order_history (
                                    order_id,
	                                status_changed_at,
	                                status_id,
	                                current_storage_id
                                )
                                VALUES (
                                    @order_id,
	                                @status_changed_at,
	                                @status_id,
	                                @current_storage_id
                                )";

            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@order_id", orderHistoryDTO.OrderId),
                new SqlParameter("@status_changed_at", orderHistoryDTO.StatusChangedAt),
                new SqlParameter("@status_id", orderHistoryDTO.StatusId),
                new SqlParameter("@current_storage_id", orderHistoryDTO.StorageId)

            };
            return new QueryWithParameters(query, parameters);
        }

        public QueryWithParameters GetUpdateOrderHistoryQuery(OrderHistoryDTO orderHistoryDTO)
        {
            string query = @"UPDATE oh
                               SET 
                                   oh.status_id          = @status_id,
	                               oh.current_storage_id = @current_storage_id
                              FROM 
                                   order_history oh 
                              JOIN orders o ON oh.order_id = o.id
                              WHERE oh.order_id            = @order_id
                                AND oh.status_changed_at   = @status_changed_at";

            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@order_id", orderHistoryDTO.OrderId),
                new SqlParameter("@status_changed_at", orderHistoryDTO.StatusChangedAt),
                new SqlParameter("@status_id", orderHistoryDTO.StatusId),
                new SqlParameter("@current_storage_id", orderHistoryDTO.StorageId)
            };
            return new QueryWithParameters(query, parameters);
        }

        public void DeleteOrderHistory(OrderHistoryDTO orderHistoryDTO)
        {
            string query = @"DELETE FROM order_history
                              WHERE order_id            = @order_id
                                    AND status_changed_at   = @status_changed_at";

            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@order_id", orderHistoryDTO.OrderId),
                new SqlParameter("@status_changed_at", orderHistoryDTO.StatusChangedAt)
            };
            _context.Database.ExecuteSqlRaw(query, parameters);
        }

        public QueryWithParameters GetInsertWorkerInOrderHistoryQuery(WorkersInOrdersDTO workersInOrdersDTO)
        {
            string query = @"INSERT INTO workers_in_orders (
                                    order_id,
	                                status_changed_at,
	                                worker_id
                                )
                                VALUES (
                                    @order_id,
	                                @status_changed_at,
	                                @worker_id
                                )";

            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@worker_id", workersInOrdersDTO.WorkerId)
            };
            return new QueryWithParameters(query, parameters);
        }

        public QueryWithParameters GetDeleteWorkerInOrderHistoryQuery(WorkersInOrdersDTO workersInOrdersDTO)
        {
            string query = @"DELETE FROM workers_in_orders
                              WHERE order_id          = @order_id
                                AND status_changed_at = @status_changed_at 
	                            AND worker_id         = @worker_id";

            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@worker_id", workersInOrdersDTO.WorkerId)
            };
            return new QueryWithParameters(query, parameters);
        }
    }
}
