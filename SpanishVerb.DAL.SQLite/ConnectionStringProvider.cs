using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpanishVerb.DAL.Abstract;

namespace SpanishVerb.DAL.SQLite
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        // the model name in the app.config connection string (any model name - Model1?)
        public string GetConnectionStringFromPath(string model, string path)
        {
    ////< add name = "AnkiEntities" connectionString = "metadata=res://*/AnkiModel.csdl|res://*/AnkiModel.ssdl|res://*/AnkiModel.msl;provider=System.Data.SQLite.EF6;provider connection string=&quot;data source=E:\Projects\AnkiSQLite\AnkiSQLite\DB\collection.anki2&quot;" providerName = "System.Data.EntityClient" />


                 // Build the provider connection string with configurable settings
                 var providerSB = new SqlConnectionStringBuilder
            {
                // You can also pass the sql connection string as a parameter instead of settings
                ////InitialCatalog = settings.InitialCatalog,
                DataSource = path,
                //UserID = settings.User,
                //Password = settings.Password
            };

            var efConnection = new EntityConnectionStringBuilder();
            // or the config file based connection without provider connection string
            // var efConnection = new EntityConnectionStringBuilder(@"metadata=res://*/model1.csdl|res://*/model1.ssdl|res://*/model1.msl;provider=System.Data.SqlClient;");
            efConnection.Provider = "System.Data.SQLite.EF6";
            efConnection.ProviderConnectionString = providerSB.ConnectionString;
            // based on whether you choose to supply the app.config connection string to the constructor
            ////efConnection.Metadata = $"res://*/Model.{model}.csdl|res://*/Model.{model}.ssdl|res://*/Model.{model}.msl";
            efConnection.Metadata = $"res://*/{model}.csdl|res://*/{model}.ssdl|res://*/{model}.msl";
            return efConnection.ToString();

        }
        // Or just pass the connection string
        public string GetConnectionString(string model, string providerConnectionString)
        {

            var efConnection = new EntityConnectionStringBuilder();
            // or the config file based connection without provider connection string
            // var efConnection = new EntityConnectionStringBuilder(@"metadata=res://*/model1.csdl|res://*/model1.ssdl|res://*/model1.msl;provider=System.Data.SqlClient;");
            efConnection.Provider = "System.Data.SQLite.EF6";
            efConnection.ProviderConnectionString = providerConnectionString;
            // based on whether you choose to supply the app.config connection string to the constructor
            efConnection.Metadata = string.Format("res://*/Model.{0}.csdl|res://*/Model.{0}.ssdl|res://*/Model.{0}.msl", model);
            // Make sure the "res://*/..." matches what's already in your config file.
            return efConnection.ToString();

        }
    }
}
