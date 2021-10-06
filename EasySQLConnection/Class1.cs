using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EasySQLConnection
{
    /// <summary>
    /// Класс, для подключения к базе данных.
    /// </summary>
    public class ESqlConnection
    {
        private string DBName;
        

        public ESqlConnection(string dn)
        {
            DBName = dn;
        }
        /// <summary>
        /// Основной метод для подключения к базе данных.
        /// </summary>
        /// <param name="sqlQuery">SQL Запрос.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public EnumerableRowCollection<DataRow> ExecuteSqlQuery(string sqlQuery)
        {
            try
            {
                DataTable resultDataTable = new DataTable();

                using (var sqlConnection = new SqlConnection($@"Data source = localhost; Initial Catalog = {DBName}; Integrated Security = True "))
                {
                    SqlCommand cmd = new SqlCommand()
                    {
                        CommandType = CommandType.Text,
                        Connection = sqlConnection,
                        CommandTimeout = 30
                    };

                    sqlConnection.Open();

                    cmd.CommandText = sqlQuery;
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    resultDataTable.Load(sqlDataReader);

                    sqlDataReader.Close();
                    sqlConnection.Close();
                }

                return resultDataTable.AsEnumerable();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка выполнения SQL запроса:\r\nОшибка: {ex.Message}\r\nSQL запрос:\r\n{sqlQuery}");
            }
        }
        /// <summary>
        /// Универсальный метод, с помощью которрого можно выбирать, обновлять, добавлять и удалять данные из базы данных.
        /// </summary>
        /// <param name="Method">SELECT - производит выборку данных из базы данных.
        ///                      UPDATE - производит обновление данных в базе данных.
        ///                      INSERT INTO - производит вставку данных в базу данных.
        ///                      DELETE - производит удаление данных из базы данных.</param>
        /// <param name="сolumns">Столбцы над которыми будут производиться действия.</param>
        /// <param name="methodprefix">FROM для SELECT и DELETE,
        ///                            VALUES для INSERT,
        ///                            SET для UPDATE</param>
        /// <param name="tableName">Название таблицы к которой поступит запрос.</param>
        //public void ExecuteUniversalSqlQuery(string Method, string[] сolumns, string methodprefix, string tableName  )
        //{
        //    string sqlQuery = $@"{Method} {сolumns} {methodprefix} {tableName}";
        //}
    }
}

/*
    public EnumerableRowCollection<DataRow> ExecuteSqlQueryAsEnumerable(string sqlQuery)
        {
            try
            {
                DataTable resultDataTable = new DataTable();

                using (var sqlConnection = new SqlConnection(@"Data source = localhost; Initial Catalog = MainDB; Integrated Security = True "))
                {
                    SqlCommand cmd = new SqlCommand()
                    {
                        CommandType = CommandType.Text,
                        Connection = sqlConnection,
                        CommandTimeout = 30
                    };

                    sqlConnection.Open();

                    cmd.CommandText = sqlQuery;
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    resultDataTable.Load(sqlDataReader);

                    sqlDataReader.Close();
                    sqlConnection.Close();
                }

                return resultDataTable.AsEnumerable();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка выполнения SQL запроса:\r\nОшибка: {ex.Message}\r\nSQL запрос:\r\n{sqlQuery}");
            }
        }
 */