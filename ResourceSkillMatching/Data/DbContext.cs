using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace ResourceSkillMatching.Data
{
    public class DbContext
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public DbContext(string connectionString, IConfiguration configuration)
        {
            _connectionString = connectionString;
            _configuration = configuration;
        }

        public IEnumerable<T> Query<T>(string sql, SqlParameter[] parameters = null, CommandType commandType = CommandType.StoredProcedure)
        {
            var items = new List<T>();

            using (SqlCommand sqlCommand = new SqlCommand(sql))
            {
                sqlCommand.CommandType = commandType;
                if (parameters != null && parameters.Length > 0)
                    sqlCommand.Parameters.AddRange(parameters);
                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            var objT = Activator.CreateInstance<T>();
                            foreach (var property in typeof(T).GetProperties())
                            {
                                if (HasColumn(sqlDataReader, property.Name) && !sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal(property.Name)))
                                    property.SetValue(objT, ConvertTo(sqlDataReader[property.Name], property));
                            }
                            items.Add(objT);
                        }
                    }
                }
            }

            return items;
        }

        public bool HasColumn(SqlDataReader dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        private object ConvertTo(object value, PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType.IsEnum)
            {
                return (value == null) ? null : Enum.ToObject(propertyInfo.PropertyType, value);
            }
            else
            {
                return (value == null) ? null : Convert.ChangeType(value, propertyInfo.PropertyType);
            }
        }
    }
}