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
using System.Windows.Shapes;

namespace Practice
{
    /// <summary>
    /// Логика взаимодействия для DepartmentEditor.xaml
    /// </summary>
    public partial class DepartmentEditor : Window
    {
        List<Department> departments;
        public DepartmentEditor()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            departments = Department.GetAll();
            dataGrid.ItemsSource = departments;
            dataGrid.Columns[0].Visibility = Visibility.Hidden;
            dataGrid.Columns[1].Header = "Имя цеха";
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            foreach(var departments in departments)
            {
                if (departments.Id == -1) departments.Create();
                else departments.Update();
            }
            Close();
        }

        private void dataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Delete == e.Key)
            {
                if (dataGrid.Items.Count != 1)
                foreach (Department department in dataGrid.SelectedItems) {
                    department.Delete();
                }
            }       
        }
    }
}
