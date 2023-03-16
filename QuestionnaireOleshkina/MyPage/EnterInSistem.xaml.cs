using Connechn;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Serialization;
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


namespace QuestionnaireOleshkina
{
    /// <summary>
    /// Логика взаимодействия для EnterInSistem.xaml
    /// </summary>



    public partial class EnterInSistem : Page
    {
        ConnectWithDataBase connection;
        public ObservableCollection<string> createQuestions { get; set; }

        public ObservableCollection<CreateQuestion> createQuestionForShow { get; set; }
        private enum forImageLion
        {
            visa,
            coll
        }
        private forImageLion enumLion = forImageLion.visa;


        public EnterInSistem(ConnectWithDataBase connect)
        {
            InitializeComponent();
            connection = connect;

            createQuestions = new ObservableCollection<string>();

            createQuestionForShow = connection.Receive(ConnectWithDataBase.teacher);











        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (enumLion == forImageLion.visa)
            {
                imgLion.Visibility = Visibility.Visible;
                enumLion = forImageLion.coll;

            }
            else
            {
                imgLion.Visibility = Visibility.Collapsed;
                enumLion = forImageLion.visa;
            }

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {


            string text = textOnCreateQuestions.Text;

            string type = comboBoxOnTypeQuestions.Text;

            string position = textBoxOnPositionQuestions.Text;





            CreateQuestion createQuestion = new CreateQuestion()
            {
                Text = text,
                position = int.Parse(position),
                PossibleAnswer = createQuestions

           };

           
           


            connection.AddQuestion(createQuestion, type);

          











        }




      

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
          

            var text = textBoxForName.Text;
            if (text.Length < 1)
            {
                MessageBox.Show("Нет названия");
                return;
            }
            Connechn.ConnectWithDataBase.NameQuestionniry = text;
            connection.AddFromInBase();

            stackPanelNameQuetionniry.Visibility = Visibility.Hidden;
            stackPanelCreateQuesrion.Visibility = Visibility.Visible;
            stackPanelEditQuesrions.Visibility = Visibility.Visible;
            DataContext = null;

            

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(PossibleAnswer.Text.Length < 1) { MessageBox.Show("Pipa"); return; }
            createQuestions.Add(PossibleAnswer.Text);

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            createQuestionForShow = connection.Receive(ConnectWithDataBase.teacher);
            DataContext = this;
        }
    }
}
