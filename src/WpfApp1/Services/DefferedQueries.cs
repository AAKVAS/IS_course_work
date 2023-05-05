using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Services
{
    /// <summary>
    /// Класс, представляющий из себя структуру данных, которая хранит запрос к базе данных и параметры к нему.
    /// </summary>
    public class QueryWithParameters
    {
        /// <summary>
        /// Запрос к базе данных.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Параметры запроса.
        /// </summary>
        public SqlParameter[] Parameters { get; set; } = Array.Empty<SqlParameter>();

        /// <summary>
        /// Конструктор класса QueryWithParameters. В качестве параметров принимает запрос к базе данных и параметры.
        /// </summary>
        /// <param name="query">Запрос к базе данных.</param>
        /// <param name="parameters">Параметры запроса.</param>
        public QueryWithParameters(string query, SqlParameter[] parameters)
        {
            Query = query;
            Parameters = parameters;
        }
    }

    /// <summary>
    /// Класс позволяющий выполнять коллекцию запросов одной транзакцией.
    /// Нужен в ситуациях, когда нужно отложить выполнение запросов.
    /// </summary>
    public class DefferedQueries
    {
        private static readonly ISWildberriesContext _context = App.Context;

        /// <summary>
        /// Коллекция запросов.
        /// </summary>
        private List<QueryWithParameters> _queries = new List<QueryWithParameters>();

        /// <summary>
        /// Коллекция общих для всех запросов параметров.
        /// </summary>
        public static List<SqlParameter> CommonParameters { get; set; } = new();

        /// <summary>
        /// Метод добавления запроса в конец коллекции.
        /// </summary>
        /// <param name="query">Запрос с параметрами.</param>
        public void AddQuery(QueryWithParameters query)
        {
            _queries.Add(query);
        }

        /// <summary>
        /// Метод добавления запроса в начало коллекции.
        /// </summary>
        /// <param name="query">Запрос с параметрами.</param>
        public void PushQueryToFront(QueryWithParameters query)
        {
            _queries.Insert(0, query);
        }

        /// <summary>
        /// Метод, выполняющий все запросы в виде одной транзакции. После попытки выполнить транзакцию, очищает запросы и параметры.
        /// Если выполнение транзакции завершается неудачей, то откатывает изменения.
        /// </summary>
        public void ExecuteQueries()
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                foreach (var query in _queries)
                {
                    List<SqlParameter> parameters = new();
                    parameters.AddRange(CommonParameters);
                    parameters.AddRange(query.Parameters);
                    _context.Database.ExecuteSqlRaw(query.Query, parameters.ToArray());
                }
                transaction.Commit();
                ClearQueries();
            }
            catch (Exception ex)
            {
                ClearQueries();
                transaction.Rollback();
                throw ex;
            }
        }

        /// <summary>
        /// Метод очистки запросов и параметров.
        /// </summary>
        public void ClearQueries()
        {
            _queries.Clear();
            CommonParameters.Clear();
        }
    }
}
