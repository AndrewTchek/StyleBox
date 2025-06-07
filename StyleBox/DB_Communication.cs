using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StyleBox
{
    public class DB_Communication
    {

        private static MySqlConnection con;
        public static void DB_Connect()
        {
            try
            {
                string connstring = "server=localhost;uid=root;pwd=2707200;database=stylebox_db";
                con = new MySqlConnection();
                con.ConnectionString = connstring;
                con.Open();
                MessageBox.Show("Підключення до бази даних відбулось успішно");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Помилка: Підключення не відбулось");
                MessageBox.Show(ex.ToString());
            }
        }
        public static List<Cloth> DB_Get_Data(string filter = "")
        {
            string sql = "select * from stocks";
            MySqlCommand cmd = new MySqlCommand(sql, con);

            if (filter.Length == 1)
            {
                sql = "select * from stocks where cloth_type = @filter";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@filter", filter);
            }
            else if (filter.Length > 1 && filter.Length <= 5)
            {
                sql = "select * from stocks where cloth_article = @filter";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@filter", filter);
            }

            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();


                List<Cloth> clothList = new List<Cloth>();


                while (reader.Read())
                {

                    clothList.Add(new Cloth
                    {
                        cloth_article = reader["cloth_article"].ToString(),
                        cloth_name = reader["cloth_name"].ToString(),
                        cloth_type = reader["cloth_type"].ToString(),
                        cloth_price = Convert.ToDouble(reader["cloth_price"]),
                        cloth_number = Convert.ToInt32(reader["cloth_number"])
                    });
                }
                reader.Close();


                return clothList;
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("Помилка при отриманні даних:" + ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Cloth>();
            }

        }
    
        public static bool DB_Update_Item(Cloth selcetedCloth)
        {
            
            string query = "UPDATE stocks SET " +
                           "cloth_name = @name, " +
                           "cloth_type = @type, " +
                           "cloth_price = @price, " +
                           "cloth_number = @number " +
                           "WHERE cloth_article = @article";

            try
            {
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", selcetedCloth.cloth_name);
                cmd.Parameters.AddWithValue("@type", selcetedCloth.cloth_type);
                cmd.Parameters.AddWithValue("@price", selcetedCloth.cloth_price);
                cmd.Parameters.AddWithValue("@number", selcetedCloth.cloth_number);
                cmd.Parameters.AddWithValue("@article", selcetedCloth.cloth_article);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Помилка при редагуванні: " + ex.Message);
                return false;
            }


        }
        public static bool Delete_Cloth(string article)
        {
            try
            {
                    string query = "DELETE FROM stocks WHERE cloth_article = @article";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@article", article);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при видаленні: " + ex.Message);
                return false;
            }
        }
    
        public static bool DB_Add_Item(Cloth new_item)
        {







            string query = "SELECT COUNT(*) FROM stocks WHERE cloth_article = @article";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@article", new_item.cloth_article);

            long count = (long)cmd.ExecuteScalar();
            if (count > 0)
            {
                MessageBox.Show("Товар з таким артикулом вже існує");
                return false;
            }

            try
            {
                 query = @"INSERT INTO stocks 
                (cloth_article, cloth_name, cloth_type, cloth_price, cloth_number)
                VALUES (@article, @name, @type, @price, @number)";

                cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@article", new_item.cloth_article);
                cmd.Parameters.AddWithValue("@name", new_item.cloth_name);
                cmd.Parameters.AddWithValue("@type", new_item.cloth_type);
                cmd.Parameters.AddWithValue("@price", new_item.cloth_price);
                cmd.Parameters.AddWithValue("@number", new_item.cloth_number);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при додаванні: " + ex.Message);
                return false;
            }
        }
    }

}
