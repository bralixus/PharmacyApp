using System;
using System.Data.SqlClient;

namespace PharmacyApp
{
    public abstract class ActiveRecord
    {
        public string ID { get; }

        protected SqlConnection connection = null;
        
        public abstract void Save(int ID);

        public abstract void Reload(int ID);
        
        protected void Open()
        {
            if (connection == null)
            {
                try
                {
                    connection = new SqlConnection();
                    connection.ConnectionString = "Integrated Security=SSPI;" +
                                                  "Data Source=.\\SQLEXPRESS;" +
                                                  "Initial Catalog=Pharmacy";
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            else
            {
                throw new Exception();
            }
            
            connection.Open();
            
        }

        protected void Close()
        {
            if (connection != null)
            {
                connection.Close();
            }
            else
            {
                throw new Exception("Nie można zamknąć");
            }
        }

        public abstract void Delete(int ID);
       
    }
}

   


