

using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace ConsoleApp17
{
    internal class GestorEmpleados
    {
        private string connectionString;

        public GestorEmpleados(string databasePath)
        {
            connectionString = "Data Source =c:\\TMP\\Empleados1.db";
        }

        public void CrearTabla()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "CREATE TABLE IF NOT EXISTS Empleados (Id INTEGER PRIMARY KEY AUTOINCREMENT, Nombre VARCHAR(100), Apellido VARCHAR(100), Edad INTEGER, Cargo VARCHAR(100), Sueldo DECIMAL(10, 2))";
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void AgregarEmpleado(Empleado empleado)
        {
            if (!EmpleadoExiste(empleado))
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Empleados (Nombre, Apellido, Edad, Cargo, Sueldo) " + "VALUES (@Nombre, @Apellido, @Edad, @Cargo, @Sueldo)";
                    

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                        command.Parameters.AddWithValue("@Apellido", empleado.Apellido);
                        command.Parameters.AddWithValue("@Edad", empleado.Edad);
                        command.Parameters.AddWithValue("@Cargo", empleado.Cargo);
                        command.Parameters.AddWithValue("@Sueldo", empleado.Sueldo);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        private bool EmpleadoExiste(Empleado empleado)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Empleados WHERE Nombre = @Nombre AND Apellido = @Apellido";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                    command.Parameters.AddWithValue("@Apellido", empleado.Apellido);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public List<Empleado> ObtenerEmpleados()
        {
            List<Empleado> empleados = new List<Empleado>();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Empleados";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string nombre = reader.GetString(1);
                            string apellido = reader.GetString(2);
                            int edad = reader.GetInt32(3);
                            string cargo = reader.GetString(4);
                            decimal sueldo = reader.GetDecimal(5); // Lee el valor del campo de sueldo
                            Empleado empleado = new Empleado(id, nombre, apellido, edad, cargo, sueldo);
                            empleados.Add(empleado);
                        }
                    }
                }
            }
            return empleados;
        }
    }
}