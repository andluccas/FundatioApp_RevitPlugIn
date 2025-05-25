using System.Windows;
using System.Windows.Controls;

namespace FundatioApp.Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel();
        }

        private void MainContentTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}