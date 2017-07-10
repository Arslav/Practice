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

namespace Practice
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Employee> employees;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Update();

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var editor = new Editor();
            editor.ShowDialog();
            Update();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var editor = new Editor(dataGrid.SelectedItem as Employee);
            editor.ShowDialog();
            Update();
        }

        private void Update()
        {
            employees = Employee.GetAll();
            dataGrid.ItemsSource = employees;
            dataGrid.IsReadOnly = true;
            dataGrid.Columns[0].Visibility = Visibility.Collapsed;
            dataGrid.Columns[1].Header = "Таб. №";
            dataGrid.Columns[2].Header = "Ф.И.О.";
            dataGrid.Columns[3].Header = "Должность";
            dataGrid.Columns[4].Header = "Цех";
            dataGrid.Columns[5].Header = "Начало смены";
            dataGrid.Columns[6].Header = "Конец смены";
        }
    }
}
