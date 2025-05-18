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
                string connstring = "server=localhost;uid=root;pwd=27072005;database=stylebox_db";
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
        public static List<Cloth> DB_Get_Data()
        {
            string sql = "select * from stocks";
            MySqlCommand cmd = new MySqlCommand(sql, con);
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
    
        public static void DB_Update_Item(Cloth selcetedCloth)
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
                cmd.ExecuteNonQuery();
                MessageBox.Show("Дані оновлено!");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }


        }
    }

}
