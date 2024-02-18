using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    // Listas para almacenar pacientes, medicamentos y tratamientos
    static List<Paciente> pacientes = new List<Paciente>();
    static List<Medicamento> catalogoMedicamentos = new List<Medicamento>();
    static List<Tratamiento> tratamientos = new List<Tratamiento>();


    // Función principal que mantiene el programa en ejecución
    static void Main()
    {
        while (true)
        {
            MostrarMenuPrincipal();
        }
    }

    // Muestra el menú principal y llama a las funciones correspondientes según la elección del usuario
    static void MostrarMenuPrincipal()
    {
        Console.WriteLine("Menú Principal");
        Console.WriteLine("1- Agregar paciente");
        Console.WriteLine("2- Agregar medicamento al catálogo");
        Console.WriteLine("3- Asignar tratamiento médico a un paciente");
        Console.WriteLine("4- Consultas");
        Console.WriteLine("5- Salir"); // Nueva opción de salida

        int opcion = PedirEntero("Seleccione una opción: ");

        switch (opcion)
        {
            case 1:
                AgregarPaciente();
                break;
            case 2:
                AgregarMedicamentoAlCatalogo();
                break;
            case 3:
                AsignarTratamientoMedico();
                break;
            case 4:
                RealizarConsultas();
                break;
            case 5:
                Environment.Exit(0); // Salir del programa
                break;
            default:
                Console.WriteLine("Opción no válida. Intente de nuevo.");
                break;
        }
    }

    // Función para agregar un nuevo paciente al sistema
    static void AgregarPaciente()
    {
        Console.WriteLine("Agregar Paciente");
        Console.Write("Nombre: ");
        string nombre = Console.ReadLine();
        Console.Write("Teléfono: ");
        string telefono = Console.ReadLine();
        Console.Write("Tipo de Sangre: ");
        string tipoSangre = Console.ReadLine();
        Console.Write("Dirección: ");
        string direccion = Console.ReadLine();
        Console.Write("Fecha de Nacimiento (YYYY-MM-DD): ");
        DateTime fechaNacimiento = DateTime.Parse(Console.ReadLine());

        // Crear un nuevo paciente y agregarlo a la lista de pacientes
        Paciente nuevoPaciente = new Paciente(nombre, telefono, tipoSangre, direccion, fechaNacimiento);
        pacientes.Add(nuevoPaciente);

        Console.WriteLine("Paciente agregado exitosamente.");
    }

    // Función para agregar un nuevo medicamento al catálogo
    static void AgregarMedicamentoAlCatalogo()
    {
        Console.WriteLine("Agregar Medicamento al Catálogo");
        Console.Write("Código del Medicamento: ");
        string codigoMedicamento = Console.ReadLine();
        Console.Write("Nombre del Medicamento: ");
        string nombreMedicamento = Console.ReadLine();
        Console.Write("Cantidad: ");
        int cantidad = PedirEntero("Ingrese la cantidad disponible: ");

        // Crear un nuevo medicamento y agregarlo al catálogo
        Medicamento nuevoMedicamento = new Medicamento(codigoMedicamento, nombreMedicamento, cantidad);
        catalogoMedicamentos.Add(nuevoMedicamento);

        Console.WriteLine("Medicamento agregado al catálogo exitosamente.");
    }

    // Función para asignar un tratamiento médico a un paciente
    static void AsignarTratamientoMedico()
    {
        Console.WriteLine("Asignar Tratamiento Médico a un Paciente");

        if (pacientes.Count == 0 || catalogoMedicamentos.Count == 0)
        {
            Console.WriteLine("No hay pacientes o medicamentos en el sistema. Regrese al menú principal para agregar.");
            return;
        }

        Console.WriteLine("Lista de Pacientes:");
        MostrarListaPacientes();

        int indicePaciente = PedirEntero("Seleccione el número del paciente al que desea asignar tratamiento: ") - 1;

        if (indicePaciente < 0 || indicePaciente >= pacientes.Count)
        {
            Console.WriteLine("Número de paciente no válido. Regrese al menú principal.");
            return;
        }

        Paciente pacienteSeleccionado = pacientes[indicePaciente];

        Console.WriteLine("Lista de Medicamentos en Catálogo:");
        MostrarCatalogoMedicamentos();

        List<Medicamento> medicamentosSeleccionados = new List<Medicamento>();

        // Permitir al usuario seleccionar hasta 3 medicamentos para el tratamiento
        for (int i = 0; i < 3; i++)
        {
            int indiceMedicamento = PedirEntero($"Seleccione el número del medicamento {i + 1}: ") - 1;

            if (indiceMedicamento >= 0 && indiceMedicamento < catalogoMedicamentos.Count)
            {
                Medicamento medicamentoSeleccionado = catalogoMedicamentos[indiceMedicamento];

                Console.Write($"Ingrese la cantidad de {medicamentoSeleccionado.Nombre} que desea asignar: ");
                int cantidadAsignada = PedirEntero("");
                // Verificar que la cantidad asignada no supere la disponibilidad en inventario
                if (cantidadAsignada <= medicamentoSeleccionado.Cantidad)
                {
                    // Agregar el medicamento con la cantidad asignada al tratamiento
                    medicamentosSeleccionados.Add(new Medicamento(medicamentoSeleccionado.Codigo, medicamentoSeleccionado.Nombre, cantidadAsignada));
                    // Actualizar la cantidad en el catálogo de medicamentos
                    medicamentoSeleccionado.Cantidad -= cantidadAsignada;
                }
                else
                {
                    Console.WriteLine($"La cantidad ingresada supera la disponibilidad en inventario ({medicamentoSeleccionado.Cantidad}).");
                    i--;
                }
            }
            else
            {
                Console.WriteLine("Número de medicamento no válido. Seleccione otro.");
                i--;
            }
        }
        // Crear un nuevo tratamiento y agregarlo a la lista de tratamientos
        Tratamiento nuevoTratamiento = new Tratamiento(pacienteSeleccionado, medicamentosSeleccionados);
        tratamientos.Add(nuevoTratamiento);

        Console.WriteLine("Tratamiento asignado exitosamente.");
    }

    // Función para realizar consultas y mostrar informes
    static void RealizarConsultas()
    {
        Console.WriteLine("Consultas");
        Console.WriteLine($"Cantidad total de pacientes registrados: {pacientes.Count}");

        Console.WriteLine("Reporte de todos los medicamentos recetados sin repetir:");
        MostrarMedicamentosRecetados();

        Console.WriteLine("Reporte de cantidad de pacientes agrupados por edades:");
        MostrarCantidadPacientesPorEdades();

        Console.WriteLine("Reporte Pacientes y consultas ordenado por nombre:");
        MostrarPacientesYConsultasOrdenadosPorNombre();
    }



    // Función para mostrar la lista de pacientes
    static void MostrarListaPacientes()
    {
        for (int i = 0; i < pacientes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {pacientes[i].Nombre}");
        }
    }
    // Función para mostrar el catálogo de medicamentos
    static void MostrarCatalogoMedicamentos()
    {
        for (int i = 0; i < catalogoMedicamentos.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {catalogoMedicamentos[i].Nombre}");
        }
    }
    // Función para mostrar todos los medicamentos recetados sin repetir
    static void MostrarMedicamentosRecetados()
    {
        HashSet<string> medicamentosRecetados = new HashSet<string>();

        foreach (var tratamiento in tratamientos)
        {
            foreach (var medicamento in tratamiento.Medicamentos)
            {
                medicamentosRecetados.Add(medicamento.Nombre);
            }
        }

        foreach (var medicamento in medicamentosRecetados)
        {
            Console.WriteLine($"- {medicamento}");
        }
    }
    // Función para mostrar la cantidad de pacientes agrupados por edades
    static void MostrarCantidadPacientesPorEdades()
    {
        int pacientes0a10 = pacientes.Count(p => CalcularEdad(p.FechaNacimiento) <= 10);
        int pacientes11a30 = pacientes.Count(p => CalcularEdad(p.FechaNacimiento) > 10 && CalcularEdad(p.FechaNacimiento) <= 30);
        int pacientes31a50 = pacientes.Count(p => CalcularEdad(p.FechaNacimiento) > 30 && CalcularEdad(p.FechaNacimiento) <= 50);
        int pacientes51mas = pacientes.Count(p => CalcularEdad(p.FechaNacimiento) > 50);

        Console.WriteLine($"0-10 años: {pacientes0a10} pacientes");
        Console.WriteLine($"11-30 años: {pacientes11a30} pacientes");
        Console.WriteLine($"31-50 años: {pacientes31a50} pacientes");
        Console.WriteLine($"Mayores de 51 años: {pacientes51mas} pacientes");
    }
    // Función para mostrar pacientes y consultas ordenados por nombre
    static void MostrarPacientesYConsultasOrdenadosPorNombre()
    {
        var pacientesOrdenados = pacientes.OrderBy(p => p.Nombre);

        foreach (var paciente in pacientesOrdenados)
        {
            Console.WriteLine($"Paciente: {paciente.Nombre}");
            var consultasPaciente = tratamientos.Where(t => t.Paciente == paciente);

            foreach (var consulta in consultasPaciente)
            {
                Console.WriteLine($"- Consulta con {consulta.Medicamentos.Count} medicamentos");
            }
        }
    }
    // Función para calcular la edad a partir de la fecha de nacimiento
    static int CalcularEdad(DateTime fechaNacimiento)
    {
        int edad = DateTime.Now.Year - fechaNacimiento.Year;
        if (DateTime.Now.Month < fechaNacimiento.Month || (DateTime.Now.Month == fechaNacimiento.Month && DateTime.Now.Day < fechaNacimiento.Day))
        {
            edad--;
        }
        return edad;
    }
    // Función para solicitar un número entero al usuario
    static int PedirEntero(string mensaje)
    {
        int resultado;
        do
        {
            Console.Write(mensaje);
        } while (!int.TryParse(Console.ReadLine(), out resultado));
        return resultado;
    }
}

// Clase que representa a un paciente
class Paciente
{
    public string Nombre { get; set; }
    public string Telefono { get; set; }
    public string TipoSangre { get; set; }
    public string Direccion { get; set; }
    public DateTime FechaNacimiento { get; set; }

    // Constructor de la clase Paciente
    public Paciente(string nombre, string telefono, string tipoSangre, string direccion, DateTime fechaNacimiento)
    {
        Nombre = nombre;
        Telefono = telefono;
        TipoSangre = tipoSangre;
        Direccion = direccion;
        FechaNacimiento = fechaNacimiento;
    }
}

// Clase que representa un medicamento
class Medicamento
{
    public string Codigo { get; set; }
    public string Nombre { get; set; }
    public int Cantidad { get; set; }

    // Constructor de la clase Medicamento
    public Medicamento(string codigo, string nombre, int cantidad)
    {
        Codigo = codigo;
        Nombre = nombre;
        Cantidad = cantidad;
    }
}

// Clase que representa un tratamiento médico
class Tratamiento
{
    public Paciente Paciente { get; set; }
    public List<Medicamento> Medicamentos { get; set; }
    // Constructor de la clase Tratamiento
    public Tratamiento(Paciente paciente, List<Medicamento> medicamentos)
    {
        Paciente = paciente;
        Medicamentos = medicamentos;
    }
}
