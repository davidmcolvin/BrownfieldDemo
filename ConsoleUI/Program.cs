using BrownfieldLibrary;
using BrownfieldLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
  class Program
  {
    static void Main(string[] args)
    {
      List<TimeSheetEntryModel> timeSheets = LoadTimeSheets();

      List<CustomerModel> customers = DataAccess.GetCustomers();

      EmployeeModel currentEmployee = DataAccess.GetCurrentEmployee();

      foreach (var customer in customers)
      {
        BillCustomer(timeSheets, customer);
      }

      PayEmployee(timeSheets, currentEmployee);

      Console.WriteLine();
      Console.Write("Press any key to exit application...");
      Console.ReadKey();
    }

    private static void PayEmployee(List<TimeSheetEntryModel> timeSheets, EmployeeModel employee)
    {
      decimal totalPay = TimeSheetProcessor.CalculateEmployeePay(timeSheets, employee);
      Console.WriteLine($"You will get paid ${totalPay} for your time");
    }

    private static void BillCustomer(List<TimeSheetEntryModel> timeSheets, CustomerModel customer)
    {
      double totalHours = TimeSheetProcessor.GetHoursWorkedForCompany(timeSheets, customer.CustomerName);

      Console.WriteLine($"Simulating Sending email to {customer.CustomerName}");
      Console.WriteLine();
      Console.WriteLine("Your bill is $" + (decimal)totalHours * customer.HourlyRateToBill + " for the hours worked.");
      Console.WriteLine();
    }

    private static List<TimeSheetEntryModel> LoadTimeSheets()
    {
      List<TimeSheetEntryModel> output = new List<TimeSheetEntryModel>();
      string enterMoreTimeSheets = "";
      do
      {
        Console.Write("Enter what you did: ");
        string workDone = Console.ReadLine();
        Console.WriteLine();
        Console.Write("How long did you do it for: ");
        string rawHoursWorked = Console.ReadLine();
        Console.WriteLine();
        double hoursWorked;

        while (double.TryParse(rawHoursWorked, out hoursWorked) == false)
        {
          Console.WriteLine("Invalid number given");
          Console.WriteLine();
          Console.Write("How long did you do it for: ");
          rawHoursWorked = Console.ReadLine();
          Console.WriteLine();
        }

        TimeSheetEntryModel timeSheet = new TimeSheetEntryModel();
        timeSheet.HoursWorked = hoursWorked;
        timeSheet.WorkDone = workDone;
        output.Add(timeSheet);
        Console.Write("Do you want to enter more time (yes/no): ");
        enterMoreTimeSheets = Console.ReadLine();
        Console.WriteLine();
      } while (enterMoreTimeSheets == "yes");

      return output;
    }
  }
}
