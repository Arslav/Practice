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
    /// Логика взаимодействия для Editor.xaml
    /// </summary>
    public partial class Editor : Window
    {
        List<Department> departments;
        Employee employee;

        public Editor()
        {
            InitializeComponent();
            departments = Department.GetAll();
            DepartmentComboBox.ItemsSource = departments;
            if (DepartmentComboBox.Items.Count != 0) DepartmentComboBox.SelectedIndex = 0;
        }

        public Editor(Employee employee) : this()
        {
            this.employee = employee;
            TabelTextBox.Text = employee.Tabel.ToString();
            NameTextBox.Text = employee.Name;
            PositionTextBox.Text = employee.Position;
            DepartmentComboBox.SelectedItem = employee.Department;
            StartTimePicker.Value = DateTime.Today + employee.Start;
            EndTimePicker.Value = DateTime.Today + employee.End;
            RandomButton.Visibility = Visibility.Hidden;
            AddEditButton.Content = "Сохранить";
        }

        private void DepartmentEdit_Click(object sender, RoutedEventArgs e)
        {
            var depEdit = new DepartmentEditor();
            depEdit.ShowDialog();
            departments.Clear();
            departments = Department.GetAll();
            DepartmentComboBox.ItemsSource = departments;
            if (DepartmentComboBox.Items.Count != 0) DepartmentComboBox.SelectedIndex = 0;
        }

        private void AddEdit_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Написать валидацию
            if (employee == null) employee = new Employee();
            employee.Tabel = int.Parse(TabelTextBox.Text);
            employee.Name = NameTextBox.Text;
            employee.Position = PositionTextBox.Text;
            employee.Department = DepartmentComboBox.SelectedItem as Department;
            employee.Start = StartTimePicker.Value.Value.TimeOfDay;
            employee.End = EndTimePicker.Value.Value.TimeOfDay;
            if (employee.Id == -1) employee.Create();
            else employee.Update();
            Close();
        }
    }
}
