using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celsius.Data
{
    public class DataStore
    {
        public static async Task<IDataReader> ReadFromDataTbl(string sp, IDataParameter[] parameters = null)
        {
            IDataReader result = null;

            try
            {
                var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CBDB"].ConnectionString);
                    var command = connection.CreateCommand();
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                command.CommandText = sp;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 60;
                connection.Open();
                result = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static async Task<int> WriteToDataTbl(string sp, IDataParameter[] parameters)
        {
            int result = 0;

            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CBDB"].ConnectionString))
                {
                    var command = connection.CreateCommand();
                    if (parameters == null || parameters.Length == 0)
                    {
                        throw new Exception("Invalid parameters");
                    }
                    command.Parameters.AddRange(parameters);
                    command.Parameters.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.Int));
                    command.Parameters["@RETURN_VALUE"].Direction = ParameterDirection.ReturnValue;
                    command.CommandText = sp;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 60;
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                    result = (int)command.Parameters["@RETURN_VALUE"].Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;

        }
    }
}
