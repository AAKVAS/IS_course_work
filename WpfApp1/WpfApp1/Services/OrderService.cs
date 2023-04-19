using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WpfApp1.Models;
using WpfApp1.Models.DTO;

namespace WpfApp1.Services
{
    /// <summary>
    /// Класс, предоставляющий методы работы с данными, связанными с доставками.
    /// </summary>
    public class OrderService
    {
        private static readonly ISWildberriesContext _context = App.Context;

        /// <summary>
        /// Метод, возвращающий доставку по его Id.
        /// </summary>
        /// <param name="OrderId">Id доставки.</param>
        /// <returns>Модель доставки.</returns>
        public dynamic GetOrderByOrderId(int OrderId)
        {
            return _context.Orders.Where(o => o.Id == OrderId).Include(o => o.User).Include(o => o.Product).FirstOrDefault();
        }

        /// <summary>
        /// Метод, возвращающий коллекцию доставок для раздела "Доставки / Список доставок."
        /// </summary>
        /// <returns>Коллекция доставок.</returns>
        public ObservableCollection<dynamic> GetOrdersList()
        {
            return new ObservableCollection<dynamic>(_context.Orders
                .Include(o => o.User)
                .Include(o => o.Product)
                .Include(o => o.PickUpPoint)
                .ToList());
        } 

        /// <summary>
        /// Метод, добавляющий в базу данных доставку. В качестве параметра принимает модель доставки.
        /// </summary>
        /// <param name="order">Модель доставки.</param>
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

        /// <summary>
        /// Метод, удаляющий доставку из БД. В качестве параметра принимает модель доставки.
        /// </summary>
        /// <param name="order">Модель доставки.</param>
        public void DeleteOrder(Orders order)
        {
            string query = @"DELETE FROM orders
                              WHERE id = @id;";
            _context.Database.ExecuteSqlRaw(query, new SqlParameter("@id", order.Id));
        }

        /// <summary>
        /// Метод, добавляющий статус доставки в БД. В качестве параметра принимает модель изменения статуса доставки.
        /// </summary>
        /// <param name="orderHistory">Изменение статуса доставки.</param>
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

        /// <summary>
        /// Метод, извлекающий коллекцию доставок, готовых к получению.
        /// </summary>
        /// <returns>Коллекция доставок, готовых к получению.</returns>
        public ObservableCollection<dynamic> GetOrdersReadyToReceive()
        {
            //Id статуса доставки, готового к получению.
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

        /// <summary>
        /// Метод, возвращающий историю доставки с изображениями товара в ней.
        /// </summary>
        /// <param name="orderHistory">Модель истории доставки.</param>
        /// <returns>Модель истории доставки с изображениями товара.</returns>
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


        /// <summary>
        /// Метод, возвращающий статусы, в которые может перейти доставка, готовая к получению.
        /// </summary>
        /// <returns>Коллекция статусов, в которые может перейти доставка, готовая к получению.</returns>
        public List<OrderStatuses> GetStatusesForReadyToRecieveOrder()
        {
            string query = @"SELECT os.id,
                                    os.description
                                FROM order_statuses os
                                WHERE os.id IN (11, 12, 13)";

            return _context.OrderStatuses.FromSqlRaw(query).ToList();
        }

        /// <summary>
        /// Метод, возвращающий коллекцию изменений статусов доставки.
        /// </summary>
        /// <returns>Коллкция изменений статусов доставки.</returns>
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

        /// <summary>
        /// Метод, возвращающий коллекцию сотрудников, участвоваших в изменении статуса доставки.
        /// В качестве параметра принимает изменение статуса в виде OrderHistoryDTO.
        /// </summary>
        /// <param name="orderHistoryDTO">Изменение статуса доставки.</param>
        /// <returns>Коллекция сотрудников, участвоваших в изменении заказа.</returns>
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

        /// <summary>
        /// Метод, возвращающий запрос с параметрами, выполняющий вставку изменения статуса доставки.
        /// </summary>
        /// <param name="orderHistoryDTO">Изменение статуса доставки.</param>
        /// <returns>Запрос вставки изменения статуса доставки с параметрами.</returns>
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

        /// <summary>
        /// Метод, возвращающий запрос с параметрами, выполняющий обновление записи об изменении статуса доставки.
        /// </summary>
        /// <param name="orderHistoryDTO">Изменение статуса доставки.</param>
        /// <returns>Запрос обновления записи об изменении статуса доставки.</returns>
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

        /// <summary>
        /// Метод, возвращающий запрос с параметрами, выполняющий удаление изменения статуса доставки.
        /// </summary>
        /// <param name="orderHistoryDTO">Изменение статуса доставки.</param>
        /// <returns>Запрос удаления изменения статуса доставки.</returns>
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

        /// <summary>
        /// Метод, возвращающий запрос для вставки участия сотрудника в изменении статуса доставки.
        /// В качестве параметра принимает участие сотрудника в изменении статуса доставки.
        /// </summary>
        /// <param name="workersInOrdersDTO">Участие сотрудника в изменении статуса доставки.</param>
        /// <returns>Запрос добавления участия сотрудника в изменении статуса доставки.</returns>
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

        /// <summary>
        /// Метод, возвращающий запрос удаления участия сотрудника в изменении статуса доставки.
        /// В качестве параметра принимает участие сотрудника в изменении статуса доставки.
        /// </summary>
        /// <param name="workersInOrdersDTO">Участие сотрудника в изменении статуса доставки.</param>
        /// <returns>Запрос удаления участия сотрудника в изменении статуса доставки.</returns>
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
