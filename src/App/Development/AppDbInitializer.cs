using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Security;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Management;
using App.DataAcess;
using App.DataAcess.System;
using App.Data;

namespace App.Development
{
    /// <summary>
    /// Database initializer with test data
    /// </summary>
    public class AppDbInitializer : CreateDatabaseIfNotExists<AppContext>
    {

        protected override void Seed(AppContext context)
        {

            CrearTablasMembership();
            context.SaveChanges();

        }

        private void CrearTablasMembership()
        {
            var con = ConfigurationManager.ConnectionStrings["AppContext"].ConnectionString;
            //verificar que no se hayan creado ya
            if (!SqlServerDatabaseUtils.DbTableExists("dbo.aspnet_Membership", con))
            {
                var sqlcon = new SqlConnection(con);
                SqlServices.Install(sqlcon.Database, SqlFeatures.All, con);
            }

        }
    }
}