using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    public class Department
    {
        public int Id { get; private set; } = -1;
        public string Name { get; set; }

        //Получение всех цехов из базы данных
        public static List<Department> GetAll()
        {
            var conn = DataBase.Connection;
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM [Departments]";
            var reader = cmd.ExecuteReader();
            var result = new List<Department>();
            while (reader.Read())
            {
                var department = new Department();
                department.Id = reader.GetInt32(0);
                department.Name = reader.GetString(1);
                result.Add(department);
            }
            return result;
        }

        //Получение цеха по ID
        public static Department GetByID(int id)
        {
            var conn = DataBase.Connection;
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM [Departments] WHERE [id] = $id";
            cmd.Parameters.AddWithValue("$id", id);
            var reader = cmd.ExecuteReader();
            var department = new Department();
            if (reader.Read())
            {
                department.Id = reader.GetInt32(0);
                department.Name = reader.GetString(1);
                return department;
            }
            return null;
        }

        //Создание цеха
        public void Create()
        {
            var conn = DataBase.Connection;
            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO [Departments] VALUES (null,$name); SELECT last_insert_rowid();";
            cmd.Parameters.AddWithValue("$name", Name);
            Id = Convert.ToInt32(cmd.ExecuteScalar());
        }

        //Обновление цеха
        public void Update()
        {
            var conn = DataBase.Connection;
            var cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE [Departments] SET [name] = $name WHERE [id] = $id";
            cmd.Parameters.AddWithValue("$id", Id);
            cmd.Parameters.AddWithValue("$name", Name);
            cmd.ExecuteNonQuery();
        }

        //Удаление цеха
        public void Delete()
        {
            var conn = DataBase.Connection;
            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM [Departments] WHERE [id] = $id";
            cmd.Parameters.AddWithValue("$id", Id);
            cmd.ExecuteNonQuery();
            Id = -1;
        }


        public override string ToString()
        {
            return Name;
        }
    }
}
