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
    public class QueryWithParameters
    {
        public string Query { get; set; }
        public SqlParameter[] Parameters { get; set; } = Array.Empty<SqlParameter>();

        public QueryWithParameters(string query, SqlParameter[] parameters)
        {
            Query = query;
            Parameters = parameters;
        }
    }


    public class DefferedQueries
    {
        private static readonly ISWildberriesContext _context = new ISWildberriesContext();

        private List<QueryWithParameters> _queries = new List<QueryWithParameters>();
        public static List<SqlParameter> CommonParameters { get; set; } = new();

        public void AddQuery(QueryWithParameters query)
        {
            _queries.Add(query);
        }

        public void PushQueryToFront(QueryWithParameters query)
        {
            _queries.Insert(0, query);
        }

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

        public void ClearQueries()
        {
            _queries.Clear();
        }
    }
}
