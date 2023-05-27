using System;

namespace ConsoleApp17
{
    class Program
    {
        static void Main(string[] args)
        {
            string databasePath = "ruta_de_tu_base_de_datos.sqlite";
            GestorEmpleados gestorEmpleados = new GestorEmpleados(databasePath);
            gestorEmpleados.CrearTabla();

            Empleado empleado1 = new Empleado(1, "John", "Doe", 30, "Gerente",100);
            gestorEmpleados.AgregarEmpleado(empleado1);

            Empleado empleado2 = new Empleado(2, "Jane", "Smith", 25, "Asistente",200);
            gestorEmpleados.AgregarEmpleado(empleado2);

            Empleado empleado3 = new Empleado(3, "Obed", "Esteban", 18, "Tesorero",456);
            gestorEmpleados.AgregarEmpleado(empleado3);

            Empleado empleado4 = new Empleado(4, "Ana", "López", 40, "Secretaria",456);
            gestorEmpleados.AgregarEmpleado(empleado4);

            // Obtener y mostrar los empleados
            var empleados = gestorEmpleados.ObtenerEmpleados();
            foreach (var empleado in empleados)
            {
                Console.WriteLine(empleado.ToString());
            }

            Console.ReadLine();
        }
    }
}