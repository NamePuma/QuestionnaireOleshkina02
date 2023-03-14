﻿using System;
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
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using System.Collections.ObjectModel;

namespace Connechn
{
    

    public class ConnectWithDataBase
    {

        public static string NameQuestionniry { get; set; }

        public static string NameStudent { get; set; }
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
        public void AddQuestion(CreateQuestion createQuestion, string type)
        {
            NpgsqlCommand npgsqlCommand = autongsqlConnection.CreateCommand();

            npgsqlCommand.CommandText = "select \"Id\"\r\nfrom \"From\" order by \"Id\"  desc limit 1";
            var e = npgsqlCommand.ExecuteReader();
            e.Read();
            int Id = e.GetInt32(0);
            Console.WriteLine(Id);
            e.Close();
            
            string json = JsonConvert.SerializeObject(createQuestion);

           

            npgsqlCommand.CommandText = "insert into \"Question \" ( \"Content\" , \"From\", \"Type\" )\r\nvalues (@json, @from, @type);";
            npgsqlCommand.Parameters.AddWithValue("@json", NpgsqlDbType.Jsonb, json);
            npgsqlCommand.Parameters.AddWithValue("@from", NpgsqlDbType.Integer, Id);
            npgsqlCommand.Parameters.AddWithValue("@type", NpgsqlDbType.Varchar, type);
            npgsqlCommand.ExecuteNonQuery();
            


        }

        public ObservableCollection<CreateQuestion> SelectQuestionniyAuto()
        {


            NpgsqlCommand npgsqlCommand = autongsqlConnection.CreateCommand();
            npgsqlCommand.CommandText = "Select \"id\", \"Content\" from \"Question \"";
            var result = npgsqlCommand.ExecuteReader();
            ObservableCollection<CreateQuestion> addInCollection = new ObservableCollection<CreateQuestion>();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    CreateQuestion createQuestion = JsonConvert.DeserializeObject<CreateQuestion>(result.GetString(1));
                    //result.Close();
                    //return createQuestion;
                    addInCollection.Add(createQuestion);




                }
                result.Close();
                return addInCollection; 
            }
            
           

            
            result.Close();
            return null;
        }





        public CreateQuestion SelectQuestionniy()
        {


            NpgsqlCommand npgsqlCommand = autongsqlConnection.CreateCommand();
            npgsqlCommand.CommandText = "Select \"id\", \"Content\" from \"Question \" order by \"id\"  desc";
            var result = npgsqlCommand.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    CreateQuestion createQuestion = JsonConvert.DeserializeObject<CreateQuestion>(result.GetString(1));
                    result.Close();
                    return createQuestion;





                }

            }




            result.Close();
            return null;
        }




    }


    public class CreateQuestion
    {
        public string Text { get; set; }

        public int position { get; set; }

        public ObservableCollection<string> PossibleAnswer { get; set; }
    }
}
