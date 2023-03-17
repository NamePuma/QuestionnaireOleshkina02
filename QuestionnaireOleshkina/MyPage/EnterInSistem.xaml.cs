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
        private enum TwoClick
        {
            One, Two
        }
        private TwoClick clickForListBox = TwoClick.One;
        
        private enum ForIdQuestion
        {
            one , two 
        }
        private ForIdQuestion enumId = ForIdQuestion.one;

        ConnectWithDataBase connection;
        public ObservableCollection<string> createQuestions { get; set; }

        public ObservableCollection<Receive> ForShow { get; set; }

        public ObservableCollection<CreateQuestion> ShowQuestion { get; set; }
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

            



            ForShow = connection.Receive(ConnectWithDataBase.teacher);
            DataContext = this;









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



            if (enumId == ForIdQuestion.one)
            {

                connection.AddQuestionWithId(createQuestion, type, ConnectWithDataBase.IdQuestion);
            }
            else if (enumId == ForIdQuestion.two)
            {
                connection.AddQuestion(createQuestion, type);
            }
            if (ShowQuestion == null)
            {
                
                ShowQuestion = new ObservableCollection<CreateQuestion>();
                listiForQuestionnaire.ItemsSource = ShowQuestion;
                ShowQuestion.Add(createQuestion);
                clickForListBox = TwoClick.Two;


            }
            else
            {
                ShowQuestion.Add(createQuestion);
            }





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
            ForShow.Clear();
            enumId = ForIdQuestion.two;




        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(PossibleAnswer.Text.Length < 1) { MessageBox.Show("Pipa"); return; }
            createQuestions.Add(PossibleAnswer.Text);

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //switch(clickForListBox)
            //{
            //    case TwoClick.One: clickForListBox = TwoClick.Two; listBoxForQuestions.SelectedItem = null; break;
            //    case TwoClick.Two: MessageBox.Show("Вы вфывыф"); clickForListBox = TwoClick.One; break;

            //}
            if (clickForListBox == TwoClick.One)
            {

                var list = sender as ListBox;

                if (list.SelectedItem == null) return;

                Receive receive = (Receive)list.SelectedItem;

                int id = receive.Id;

                ConnectWithDataBase.IdQuestion = id;

                ShowQuestion = connection.SelectQuestionniyAuto(id);

                stackPanelNameQuetionniry.Visibility = Visibility.Hidden;
                stackPanelCreateQuesrion.Visibility = Visibility.Visible;
                stackPanelEditQuesrions.Visibility = Visibility.Visible;

                listiForQuestionnaire.ItemsSource = ShowQuestion;

                clickForListBox = TwoClick.Two;

            }
            else if (clickForListBox == TwoClick.Two)
            {
                var list = sender as ListBox;

                if (list.SelectedItem == null) return;

                CreateQuestion receive = (CreateQuestion)list.SelectedItem;

                
            }


            
            




        }
    }
}
