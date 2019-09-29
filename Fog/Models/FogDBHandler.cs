using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Fog.Models
{
    public class FogDBHandler
    {
        private MySqlConnection conn;

        public FogDBHandler()
        {
            string conString = "server=192.168.0.27;" +
                               "database=cs470;" +
                               "user id=UMKC470;" +
                               "Password=umkc;" +
                               "persistsecurityinfo=True;" +
                               "allowuservariables=True";
            conn = new MySqlConnection(conString);
        }

        //************************Validate Login****************************
        public bool ValidateUser(LoginModel loginModel)
        {
            try {
                MySqlCommand cmd = new MySqlCommand("validate_userF", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", loginModel.Username);
                cmd.Parameters.AddWithValue("@userpassword", loginModel.Password);
                cmd.Parameters.AddWithValue("@returnvalue", MySqlDbType.Bit);
                cmd.Parameters["@returnvalue"].Direction = ParameterDirection.ReturnValue;
                conn.Open();
                cmd.ExecuteScalar();
                conn.Close();
                return Convert.ToBoolean(cmd.Parameters["@returnvalue"].Value);
            }
            catch { return false; }
        }
    }
}
