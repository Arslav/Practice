using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace Practice
{
    public static class DataBase
    {
        //Путь к базе
        public const string PATH = "data.db"; 

        //Флаг инициализации
        public static bool IsInit { get; set; } = false;

        //Код инициализации бд
        private static void _init()
        {
            if (!IsInit)
            {
                //Создаем файл если базы не существует
                if(!File.Exists(PATH))
                    SQLiteConnection.CreateFile(PATH);
                //Подключаемся к базе
                _connection = new SQLiteConnection("Data Source = "+PATH);
                _connection.Open();
                //Получаем управление
                var cmd = _connection.CreateCommand();
                //Создаем таблицы если их не существует
                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS [Employees] (
                                      [id] INTEGER NOT NULL
                                    , [tabel] INTEGER NOT NULL
                                    , [name] TEXT NOT NULL
                                    , [position] TEXT NOT NULL
                                    , [department] INTEGER NOT NULL
                                    , [start] TEXT NOT NULL
                                    , [end] TEXT NOT NULL
                                    , CONSTRAINT [PK_Employees] PRIMARY KEY ([id])
                                  );";
                cmd.CommandText += @"CREATE TABLE IF NOT EXISTS [Departments] (
                                      [id] INTEGER NOT NULL
                                    , [name] TEXT NOT NULL
                                    , CONSTRAINT [PK_Departments] PRIMARY KEY ([id])
                                  );";
                //...
                cmd.ExecuteNonQuery();
                //Инициализация выполненна
                IsInit = true;
            }
        }

        //Подключение к базе
        private static SQLiteConnection _connection; 
        public static SQLiteConnection Connection
        {
            get
            {
                if (!IsInit) _init();
                return _connection;
            }

            private set {}
        }
    }
}
