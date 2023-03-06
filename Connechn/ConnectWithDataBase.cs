using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;
using System.Windows.Input;
using System.Dynamic;

namespace Connechn
{
    

    public class ConnectWithDataBase
    {
       public NpgsqlConnection ngsqlConnection;

        public NpgsqlConnection autongsqlConnection;

        public void Connect(string host, string port, string user, string password, string database) {
            ngsqlConnection = new NpgsqlConnection(string.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", host, port, user, password, password));
            ngsqlConnection.Open();

        
        
        } 
        public void AutoConnect()
        {
            autongsqlConnection = new NpgsqlConnection(string.Format("Server=10.14.206.27;Port=5432;User Id=student;Password=1234;Database=QuestionnireOleshka;"));
            autongsqlConnection.Open();
        }
       public void QQ(Question question)
        {
           NpgsqlCommand npgsqlCommand = autongsqlConnection.CreateCommand();

            npgsqlCommand.CommandText = "insert into public.\"Question\" ( \"Content\", \"From\", \"type\")\r\nvalues (@content, @from, @type);";
            npgsqlCommand.Parameters.AddWithValue("@content", NpgsqlDbType.Varchar, question.Content);
            npgsqlCommand.Parameters.AddWithValue("@from", NpgsqlDbType.Jsonb, question.From);
            npgsqlCommand.Parameters.AddWithValue("@type", NpgsqlDbType.Varchar, question.From);
            var a = npgsqlCommand.ExecuteNonQuery();




        }
        


      



    }
}
