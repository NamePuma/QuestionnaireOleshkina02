using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Connechn;
using Npgsql;
using NpgsqlTypes;

namespace QuestionnaireOleshkina
{
    /// <summary>
    /// Логика взаимодействия для EnterPage.xaml
    /// </summary>
    /// 

   

    public partial class EnterPage : Page
    {  


        private ConnectWithDataBase connect;

        public ConnectWithDataBase Connect { get { return connect; } private set { connect = value; } }

        private Page teacherPage;

        public Page TeacherPage { get { return teacherPage; } private set { teacherPage = value; } }




        public EnterPage(ConnectWithDataBase connechn)
        {
            InitializeComponent();

            Connect = connechn;

            TeacherPage = new EnterInSistem();


        }

        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if(loginTextBox.Text.Length < 1 || loginPassword.Password.Length < 1) { MessageBox.Show("Так, не понял. Ниче не вписал же");   return; }

            var login = loginTextBox.Text;
            var password = loginPassword.Password.ToString();

            NpgsqlCommand npgsqlCommand = Connect.autongsqlConnection.CreateCommand();
           
            npgsqlCommand.CommandText = "select * from \"AccountOl\" where \"Login\" = @log and \"Password\" = @pas";
            npgsqlCommand.Parameters.AddWithValue("@log", NpgsqlDbType.Varchar, login);
            npgsqlCommand.Parameters.AddWithValue("@pas", NpgsqlDbType.Varchar, password);
            var result = npgsqlCommand.ExecuteReader();
            while(result.Read())
            {
                if(result.GetString(5) == "Teacher") {

                    NavigationService.Navigate(TeacherPage);
                }
                else if (result.GetString(5) == "Student")
                {


                }

            }
            result.Close();


        }
    }
}
