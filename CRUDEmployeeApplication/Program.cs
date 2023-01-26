using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CRUDEmployeeApplication
{
    class Employee
    {
        public string EmployeeId { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
    }
    public class Program
    {
        static List<Employee> employees = new List<Employee>();
        static void Main(string[] args)
        {
            // Add some initial data 
            employees.Add(new Employee { EmployeeId = "1001", FullName = "Adit", BirthDate = new DateTime(1954, 8, 17) });
            employees.Add(new Employee { EmployeeId = "1002", FullName = "Anton", BirthDate = new DateTime(1954, 8, 18) });
            employees.Add(new Employee { EmployeeId = "1003", FullName = "Amir", BirthDate = new DateTime(1954, 8, 19) });

            bool Loop = true;
            // Show menu
            while (Loop)
            {
                Console.WriteLine("1. Add employee");
                Console.WriteLine("2. Display employees");
                Console.WriteLine("3. Remove employee");
                Console.WriteLine("4. Exit");
                Console.Write("Choose option: ");

                try
                {
                    int option = int.Parse(Console.ReadLine());

                    switch (option)
                    {
                        case 1:
                            AddEmployee();
                            break;
                        case 2:
                            DisplayEmployees();
                            break;
                        case 3:
                            RemoveEmployee();
                            break;
                        case 4:
                            Loop= false;
                            break;
                        default:
                            Console.WriteLine("Please Choose 1 - 4");
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("error");
                }
            }
        }
        static void AddEmployee()
        {
            InputId:
            Employee newEmployee = new Employee();

            // Get employee ID
            Console.Write("Enter employee ID: ");
            newEmployee.EmployeeId = Console.ReadLine();

            // Check for duplicate employee ID
            if (employees.Any(e => e.EmployeeId == newEmployee.EmployeeId))
            {
                Console.WriteLine("Error: Employee ID already exists.");
                goto InputId;
            }

            inputName:
            // Get full name
            Console.Write("Enter full name: ");
            newEmployee.FullName = Console.ReadLine();

             InputBirthdate:
            // Get birthdate
            Console.Write("Enter birthdate (dd-MM-yyyy): ");
            string birthdateString = Console.ReadLine();
            if (!DateTime.TryParseExact(birthdateString, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime birthdate))
            {
                Console.WriteLine("Error: Invalid birthdate format.");
                goto InputBirthdate;
            }

            newEmployee.BirthDate = birthdate;

            if(employees.Any(e => e.FullName == newEmployee.FullName && e.BirthDate == newEmployee.BirthDate))
            {
                Console.WriteLine("Error: Employee Name already exists.");
                goto inputName;
            }

            // Add employee to list
            employees.Add(newEmployee);

            Console.WriteLine("Employee added successfully.");
            DisplayEmployees();
        }
        static void DisplayEmployees()
        {
            // Check if list is empty
            if (!employees.Any())
            {
                Console.WriteLine("No employees found.");
                return;
            }

            Console.WriteLine("EmployeeId\tFullName\tBirthDate");

            // Display each employee
            foreach (Employee employee in employees)
            {
                Console.WriteLine($"{employee.EmployeeId}\t\t{employee.FullName}\t\t{employee.BirthDate.ToString("dd-MM-yyyy")}");
            }
        }
        static void RemoveEmployee()
        {
            RemoveEmp:
            Console.Write("Enter the EmployeeId of employee to remove: ");
            string employeeId = Console.ReadLine();

            // Check if employee exists
            Employee employee = employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (employee == null)
            {
                Console.WriteLine("Error: Employee not found.");
                goto RemoveEmp;
            }

            // Confirm removal
            Console.Write($"Are you sure you want to remove employee {employee.FullName} with ID {employee.EmployeeId}? (y/n): ");
            string confirm = Console.ReadLine();
            if (confirm.ToLower() != "y")
            {
                Console.WriteLine("Removal cancelled.");
                return;
            }

            // Remove employee
            employees.Remove(employee);
            Console.WriteLine("Employee removed successfully.");
        }
    }
}
