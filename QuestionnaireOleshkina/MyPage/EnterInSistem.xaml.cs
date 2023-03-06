using Connechn;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private enum forImageLion {
            visa, 
            coll
        }
        private forImageLion enumLion = forImageLion.visa;
        
        
        public EnterInSistem()
        {
            InitializeComponent();

            
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

        class ASD
        {
            public int Position { get; set; }
            public string Text { get; set; }
            public List<string> Variants { get; set; }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var p = textOnCreateQuestions.Text;
            var v = comboBoxOnTypeQuestions.Text;
            var s = textBoxOnPositionQuestions.Text;

            string a = JsonConvert.DeserializeObject<string>(p);

            a += JsonConvert.DeserializeObject<string>(p);

            Console.WriteLine();








        }
    }
}
