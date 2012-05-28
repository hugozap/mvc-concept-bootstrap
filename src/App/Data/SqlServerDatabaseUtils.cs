using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web.Management;
using System.Configuration;
using System.Web.Security;

namespace App.DataAcess.System
{
    /// <summary>
    /// Utilidades para el motor de la base de datos
    /// Si en el futuro se requiere soporte a multiples motores habria que tener componentes
    /// especializados para cada uno de los motores.
    /// </summary>
    public class SqlServerDatabaseUtils
    {
        public static bool DbTableExists(string strTableNameAndSchema, string strConnection)
        {
            using (SqlConnection connection = new SqlConnection(strConnection))
            {
                string strCheckTable =
                   String.Format(
                      "IF OBJECT_ID('{0}', 'U') IS NOT NULL SELECT 'true' ELSE SELECT 'false'",
                      strTableNameAndSchema);

                SqlCommand command = new SqlCommand(strCheckTable, connection);
                command.CommandType = CommandType.Text;
                connection.Open();

                return Convert.ToBoolean(command.ExecuteScalar());
            }
        }

            
            public static void CrearTablasMembership()
            {
                var con = ConfigurationManager.ConnectionStrings["AppContext"].ConnectionString;
                //verificar que no se hayan creado ya
                if (!SqlServerDatabaseUtils.DbTableExists("dbo.aspnet_Membership", con))
                {
                    var sqlcon = new SqlConnection(con);
                    SqlServices.Install(sqlcon.Database, SqlFeatures.All, con);
                }            
            }

          public static void BorrarTablasMembership()
            {
                var con = ConfigurationManager.ConnectionStrings["AppContext"].ConnectionString;
             

                    var sqlcon = new SqlConnection(con);
                    try
                    {
                        foreach (MembershipUser user in Membership.GetAllUsers())
                            Membership.DeleteUser(user.UserName, true);   
                        SqlServices.Uninstall(sqlcon.Database, SqlFeatures.All, con);
                    }
                    catch (Exception ex)
                    {

                    }
                    
                        
            }
    }
}
