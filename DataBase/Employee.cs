using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    //Класс для работы с рабочими
    public class Employee
    {
        //Поля
        public int Id { get; private set; } = -1;
        public int Tabel { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public Department Department { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }

        //Получение всех рабочих из базы данных
        public static List<Employee> GetAll()
        {
            var conn = DataBase.Connection;
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM [Employees]";
            var reader = cmd.ExecuteReader();
            var result = new List<Employee>();
            while (reader.Read())
            {
                var employee = new Employee();
                employee.Id = reader.GetInt32(0);
                employee.Tabel = reader.GetInt32(1);
                employee.Name = reader.GetString(2);
                employee.Position = reader.GetString(3);
                employee.Department = Department.GetByID(reader.GetInt32(4));
                employee.Start = TimeSpan.Parse(reader.GetString(5));
                employee.End = TimeSpan.Parse(reader.GetString(6));
                result.Add(employee);
            }
            return result;
        }

        //Получение рабочего по id из базы данных
        public static Employee GetByID(int id)
        {
            var conn = DataBase.Connection;
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM [Employees] WHERE [id] = $id";
            cmd.Parameters.AddWithValue("$id", id);
            var reader = cmd.ExecuteReader();
            var employee = new Employee();
            if(reader.Read())
            {
                employee.Id = reader.GetInt32(0);
                employee.Tabel = reader.GetInt32(1);
                employee.Name = reader.GetString(2);
                employee.Position = reader.GetString(3);
                employee.Department = Department.GetByID(reader.GetInt32(4));
                employee.Start = TimeSpan.Parse(reader.GetString(5));
                employee.End = TimeSpan.Parse(reader.GetString(4));
                return employee;
            }
            return null;
        }

        //Создание цеха
        public void Create()
        {
            var conn = DataBase.Connection;
            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO [Employees] VALUES (null,$tabel,$name,$position,$department,$start,$end); SELECT last_insert_rowid();";
            cmd.Parameters.AddWithValue("$tabel", Tabel);
            cmd.Parameters.AddWithValue("$name", Name);
            cmd.Parameters.AddWithValue("$position", Position);
            cmd.Parameters.AddWithValue("$department", Department.Id);
            cmd.Parameters.AddWithValue("$start", Start.ToString());
            cmd.Parameters.AddWithValue("$end", End.ToString());
            Id = Convert.ToInt32(cmd.ExecuteScalar());
        }

        //Обновление рабочего
        public void Update()
        {
            var conn = DataBase.Connection;
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"UPDATE [Employees] SET
                                    [tabel] = $tabel,
                                    [name] = $name,
                                    [position] = $position,
                                    [department] = $department,
                                    [start] = $start,
                                    [end] = $end
                                WHERE [id] = $id";
            cmd.Parameters.AddWithValue("$id", Id);
            cmd.Parameters.AddWithValue("$tabel", Tabel);
            cmd.Parameters.AddWithValue("$name", Name);
            cmd.Parameters.AddWithValue("$position", Position);
            cmd.Parameters.AddWithValue("$department", Department.Id);
            cmd.Parameters.AddWithValue("$start", Start.ToString());
            cmd.Parameters.AddWithValue("$end", End.ToString());
            cmd.ExecuteNonQuery();
        }
    }
}
