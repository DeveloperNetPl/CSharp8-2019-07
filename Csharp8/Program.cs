using System;
using System.Collections.Generic;
#nullable enable
namespace Csharp8
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee[] employees = {
                new Employee {
                    FirstName = "Zenon",
                    SecondName = "Zdzisław",
                    Surname = "Zwykly",
                    State = EmploymentState.employed,
                    InWorkNow = true},
                new Employee {
                    FirstName = "Adrian",
                    Surname = "Niezwykly",
                    State = EmploymentState.fired,
                    InWorkNow = true},
                new Employee {
                    FirstName = "Tomasz",
                    SecondName = "Zdzisław",
                    Surname = "Nieobecny",
                    State = EmploymentState.employed,
                    InWorkNow = false},
                new Employee {
                    FirstName = "Maciej",
                    Surname = "Nieobecny",
                    State = EmploymentState.fired,
                    InWorkNow = false},
                };
            foreach (var employee in employees)
            {
                Version7Switch(employee);
                Version8Switch(employee);
                Version8SwitchTuples(employee);
            }
        }

        public enum EmploymentState
        {
            unknown = 0,
            employed = 1,
            fired = 2
        }

        public class Employee
        {
            public string FirstName { get; set; }
            public string? SecondName { get; set; }
            public string Surname { get; set; }
            public EmploymentState State { get; set; }
            public bool InWorkNow { get; set; }
        }

        static void Version7Switch(Employee employee)
        {
            switch (employee)
            {
                case Employee emp when emp.State == EmploymentState.employed && emp.InWorkNow:
                    Console.WriteLine($"Pracownik {employee.FirstName} {employee.Surname} jest w pracy.");
                    break;
                case Employee emp when emp.State == EmploymentState.employed && !emp.InWorkNow:
                    Console.WriteLine($"Pracownik {employee.FirstName} {employee.Surname} jest zatrudniony ale nieobecny.");
                    break;
                case Employee emp when emp.State == EmploymentState.fired && emp.InWorkNow:
                    Console.WriteLine($"Pracownik {employee.FirstName} {employee.Surname} jest nielegalnie w pracy.");
                    break;
                case Employee emp when emp.State == EmploymentState.fired && !emp.InWorkNow:
                    Console.WriteLine($"Pracownik {employee.FirstName} {employee.Surname} został zwolniony i jest nieobeny.");
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        static void Version8Switch(Employee employee)
        {
            string result = employee switch
            {
                Employee { State: EmploymentState.employed, InWorkNow: true } emp => "jest w pracy.",
                Employee { State: EmploymentState.employed, InWorkNow: false } emp => "jest zatrudniony ale nieobecny.",
                Employee { State: EmploymentState.fired, InWorkNow: true } emp => "jest nielegalnie w pracy.",
                Employee { State: EmploymentState.fired, InWorkNow: false } emp => "został zwolniony i jest nieobeny.",
                _ => throw new InvalidOperationException()
            };
            Console.WriteLine($"Pracownik { employee.FirstName} { employee.Surname} {result}");
        }

        static void Version8SwitchTuples(Employee employee)
        {
            string result = (employee.State, employee.InWorkNow) switch
            {
                (EmploymentState.employed, true) => "jest w pracy.",
                (EmploymentState.employed, false) => "jest zatrudniony ale nieobecny.",
                (EmploymentState.fired, true) => "jest nielegalnie w pracy.",
                (EmploymentState.fired, false) => "został zwolniony i jest nieobeny.",
                _ => throw new InvalidOperationException()
            };
            Console.WriteLine($"Pracownik { employee.FirstName} { employee.Surname} {result}");
        }
    }

}