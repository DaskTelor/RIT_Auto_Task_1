using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    public class MarkerDatabase
    {
        private readonly string _connectionString;
        private readonly LoggerBase _logger;
        public MarkerDatabase(string connectionString = "Server=localhost;Database=marker_db;Trusted_Connection=True;", LoggerBase logger = null)
        {
            _connectionString = connectionString;
            _logger = logger;
        }
        public async Task<List<Marker>> GetAllMarkersAsync()
        {
            List<Marker> markers = new List<Marker>();
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                await connection.OpenAsync();
                string commandStr = "select * from marker_db.dbo.marker";
                SqlCommand command = new SqlCommand(commandStr, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                _logger?.Log(commandStr, LoggerBase.LogType.DEBUG);
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        int id = reader.GetInt32(0);
                        double lat = reader.GetSqlDecimal(1).ToDouble();
                        double lng = reader.GetSqlDecimal(2).ToDouble();
                        markers.Add(new Marker(id, lat, lng));
                        _logger?.Log("Marker {id = " + id + ", lat = " + lat + ", lng = " + lng + "} has been readed.", LoggerBase.LogType.DEBUG);
                    }
                }
                await Task.Run(() => { reader.Close(); });
            }
            catch (SqlException ex)
            {
                _logger?.Log(ex.GetType().FullName + " {" + ex.Message + "}", LoggerBase.LogType.WARN);
            }
            catch (InvalidOperationException ex)
            {
                _logger?.Log(ex.GetType().FullName + " {" + ex.Message + "}", LoggerBase.LogType.WARN);
            }
            catch (InvalidCastException ex)
            {
                _logger?.Log(ex.GetType().FullName + " {" + ex.Message + "}", LoggerBase.LogType.WARN);
            }
            finally
            {
                await Task.Run(() => { connection.Close(); });
            }
            return markers;
        }
        public async Task UpdateMarkerAsync(Marker marker)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                await connection.OpenAsync();
                string commandStr = $"update marker_db.dbo.marker set lat = {marker.Lat.ToString("###.########")}, lng = {marker.Lng.ToString("###.########")} where id = {marker.Id}";
                SqlCommand command = new SqlCommand(commandStr, connection);
                await command.ExecuteNonQueryAsync();
                _logger.Log(commandStr, LoggerBase.LogType.DEBUG);
            }
            catch (SqlException ex)
            {
                _logger.Log(ex.GetType().FullName + " {" + ex.Message + "}", LoggerBase.LogType.WARN);
            }
            catch (InvalidOperationException ex)
            {
                _logger.Log(ex.GetType().FullName + " {" + ex.Message + "}", LoggerBase.LogType.WARN);
            }
            finally
            {
                await Task.Run(() => { connection.Close(); });
            }
        }
    }
}
