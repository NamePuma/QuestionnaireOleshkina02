using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;
using System.Windows.Input;
using System.Dynamic;
using System.Security.Policy;
using System.Net.Http;

namespace Connechn
{
    

    public class ConnectWithDataBase
    {

        public static string NameQuestionniry { get; set; }

        public static string teacher { get; set; }

       public NpgsqlConnection ngsqlConnection;

        public NpgsqlConnection autongsqlConnection;

        public void Connect(string host, string port, string user, string password, string database) {
            ngsqlConnection = new NpgsqlConnection(string.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", host, port, user, password, password));
            ngsqlConnection.Open();

        
        
        } 
        public void AutoConnect()
        {
            autongsqlConnection = new NpgsqlConnection(string.Format("Server=10.14.206.27;Port=5432;User Id=student;Password=1234;Database=Qqq;"));
            autongsqlConnection.Open();
        }
       
      
        public void AddFromInBase()
        {
           

            NpgsqlCommand npgsqlCommand = autongsqlConnection.CreateCommand();

             

            npgsqlCommand.CommandText = "insert into public.\"From\" (\"Name\", \"Teacher\")\r\nvalues (@titer, @nameQuestionniry);";
            npgsqlCommand.Parameters.AddWithValue("@nameQuestionniry", NpgsqlDbType.Varchar, Connechn.ConnectWithDataBase.teacher);
            npgsqlCommand.Parameters.AddWithValue("@titer", NpgsqlDbType.Varchar, Connechn.ConnectWithDataBase.NameQuestionniry);
           var result =  npgsqlCommand.ExecuteNonQuery();


        }
        public void AddQuestion(string json)
        {
            NpgsqlCommand npgsqlCommand = autongsqlConnection.CreateCommand();

            npgsqlCommand.CommandText = "select \"Id\"\r\nfrom \"From\" order by \"Id\"  desc limit 1";
            var e = npgsqlCommand.ExecuteReader();
            e.Read();
            int Id = e.GetInt32(0);
            Console.WriteLine(Id);

            //npgsqlCommand.CommandText = "insert into \"Question \" ( \"Content\" , \"From\", \"Type\" )\r\nvalues (@json, @from, @type);";
            //npgsqlCommand.Parameters.AddWithValue("@json", NpgsqlDbType.Jsonb, json);
            //npgsqlCommand.Parameters.AddWithValue("@from", NpgsqlDbType.Integer, json);
            //npgsqlCommand.Parameters.AddWithValue("@json", NpgsqlDbType.Jsonb, json);


        }

      



    }
}
