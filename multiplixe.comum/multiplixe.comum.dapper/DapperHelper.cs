using Dapper;
using Microsoft.Extensions.Configuration;
using multiplixe.comum.helper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace multiplixe.comum.dapper
{
    public class DapperHelper
    {
        private DynamicParameters parameters { get; set; }

        private IConfiguration configuration { get; set; }

        public DapperHelper(IConfiguration configuration)
        {
            this.configuration = configuration;

            ResetParameter();
        }

        public void ExecuteMultiple(string procedure, IMultiResultExtractor extractor)
        {
            using (var conn = ConnectionFactory())
            {
                var multiple = SqlMapper.QueryMultiple(conn, procedure, param: parameters, commandType: CommandType.StoredProcedure);

                extractor.Execute(multiple);

                conn.Close();
            }
        }

        public bool ExecuteWithBooleanResult(string procedure)
        {
            AddOutputParameter(DbType.Boolean);
            return ExecuteWithGenericsResult<bool>(procedure);
        }

        public int ExecuteWithIntResult(string procedure)
        {
            AddOutputParameter(DbType.Int32);
            return ExecuteWithGenericsResult<int>(procedure);
        }

        public T ExecuteWithGenericsResult<T>(string procedure)
        {
            T response;

            using (var conn = ConnectionFactory())
            {
                try
                {
                    conn.Open();
                    SqlMapper.Query(conn, procedure, parameters, commandType: CommandType.StoredProcedure);
                    response = parameters.Get<T>("_result");
                }
                catch (Exception ex)
                {
                    // ##todo log
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }

            return response;
        }

        public int InsertWithTransaction(string procedure, MySqlConnection connection)
        {
            var result = SqlMapper.QueryFirst<int>(connection, procedure, parameters, commandType: CommandType.StoredProcedure);

            return result;
        }

        public void UpdateWithTransaction(string procedure, MySqlConnection connection)
        {
            SqlMapper.Query(connection, procedure, parameters, commandType: CommandType.StoredProcedure);
        }

        public T ExecuteWithOneResultWithTransaction<T>(string procedure, MySqlConnection connection) where T : BaseResult
        {
            T result = default(T);

            var list = ExecuteWithManyResultWithTransaction<T>(procedure, connection);

            if (list != null & list.Any())
            {
                result = list[0];
            }

            return result;
        }

        public List<T> ExecuteWithManyResultWithTransaction<T>(string procedure, MySqlConnection connection) where T : BaseResult
        {
            var result = SqlMapper.Query<T>(connection, procedure, param: parameters, commandType: CommandType.StoredProcedure).ToList();
            return result;
        }

        public int Insert(string procedure)
        {
            var result = 0;

            using (var conn = ConnectionFactory())
            {
                try
                {
                    conn.Open();

                    result = SqlMapper.QueryFirst<int>(conn, procedure, parameters, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    // ##todo log
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }

            return result;
        }

        public void Delete(string procedure)
        {
            using (var conn = ConnectionFactory())
            {
                try
                {
                    conn.Open();

                    SqlMapper.Execute(conn, procedure, parameters, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    // ##todo log
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void Update(string procedure)
        {
            using (var conn = ConnectionFactory())
            {
                try
                {
                    conn.Open();

                    SqlMapper.Query(conn, procedure, parameters, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    // ##todo log
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public MySqlConnection ConnectionFactory()
        {
            return new MySqlConnection(configuration.GetConnectionString("database"));
        }

        public DapperHelper AddParameter(string name, string value)
        {
            var _this = this;

            if (!string.IsNullOrEmpty(value))
            {
                _this = AddParameter(name, value, DbType.String);
            }
            else
            {
                _this = AddParameterNullValue(name);
            }

            return _this;
        }

        public DapperHelper ResetParameter()
        {
            parameters = new DynamicParameters();

            return this;
        }

        public DapperHelper AddParameter(string name, DateTime value)
        {
            return AddParameter(name, value.ToString("yyyy/MM/dd HH:mm"), DbType.DateTime);
        }

        public DapperHelper AddParameter(string name, bool value)
        {
            return AddParameter(name, value, DbType.Boolean);
        }

        public DapperHelper AddParameter(string name, int value)
        {
            return AddParameter(name, value, DbType.Int32);
        }

        public DapperHelper AddParameter(string name, object value)
        {
            var json = SerializadorHelper.Serializar(value);

            return AddParameter(name, json, DbType.String);
        }

        public DapperHelper AddParameter(string name, Guid value)
        {
            var _this = this;

            if (value.Equals(Guid.Empty))
            {
                _this = AddParameterNullValue(name);
            }
            else
            {
                _this = AddParameter(name, value, DbType.Guid);
            }

            return _this;
        }


        public DapperHelper AddParameter(string name, decimal value)
        {
            return AddParameter(name, value, DbType.Decimal);
        }


        public DapperHelper AddParameterNullValue(string name)
        {
            parameters.Add(name, null);

            return this;
        }

        private DapperHelper AddParameter(string name, object value, DbType type)
        {
            parameters.Add(name, value, type, ParameterDirection.Input);

            return this;
        }

        public DapperHelper AddOutputParameter(DbType type)
        {
            return AddOutputParameter("_result", type);
        }

        public DapperHelper AddOutputParameter(string name, DbType type)
        {
            parameters.Add(name, dbType: type, direction: ParameterDirection.Output);

            return this;
        }

        public T ExecuteWithOneResult<T>(string procedure) where T : BaseResult
        {
            T result = default(T);

            var list = ExecuteWithManyResult<T>(procedure);

            if (list != null & list.Any())
            {
                result = list[0];
            }

            return result;
        }

        public List<T> ExecuteWithManyResult<T>(string procedure) where T : BaseResult
        {
            var result = new List<T>();

            using (var conn = ConnectionFactory())
            {
                try
                {
                    conn.Open();
                    result = SqlMapper.Query<T>(conn, procedure, param: parameters, commandType: CommandType.StoredProcedure).ToList();
                }
                catch (Exception ex)
                {
                    // ##todo log
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }

            }

            return result;
        }
    }
}
