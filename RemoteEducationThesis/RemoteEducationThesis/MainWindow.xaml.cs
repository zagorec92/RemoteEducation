using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace RemoteEducationThesis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 
        /// </summary>
        private TestPropertyChanged test;

        public MainWindow()
        {
            InitializeComponent();
            test = new TestPropertyChanged("Initial");
            test.PropertyChanged += test_PropertyChanged;
            Repeating();
        }

        void test_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ExampleText")
                txtBlockExample.Text = test.ExampleText;
        }

        private void bttnChangeText_Click(object sender, RoutedEventArgs e)
        {
            test.ExampleText = txtBoxTest.Text;
            test.ExampleText = txtBoxAnotherTest.Text;
        }

        private async void Repeating()
        {
            txtBoxTest.Text = test.ExampleText;
            await Task.Delay(2000);

            for (int i = 0; i < 10; i++)
            {
                test.ExampleText = i.ToString();
                txtBoxTest.Text = test.ExampleText;
                await Task.Delay(1000);
            }
        }
    }
}
