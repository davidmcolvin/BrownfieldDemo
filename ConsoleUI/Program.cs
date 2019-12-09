using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Please note - THIS IS A BAD APPLICATION - DO NOT REPLICATE WHAT IT DOES
// This application was designed to simulate a poorly-built application that
// you need to support.  Do not follow any of these practices.  This is for
// demonstration purposes only.  You have been warned.

// Steps to debug
// D.R.I.F.T
// D = Don't panic
// R = Read the error message
// I = Identify the purpose of the code
// F = Figure out what breaks that code
// T = Try to find a new solution where the code doesn't break

namespace ConsoleUI
{
  class Program
  {
    static void Main(string[] args)
    {
      string w, rawTimeWorked;
      int i;
      double ttl, t;

      List<TimeSheetEntry> ents = LoadTimeSheets();

      ttl = 0;
      for (i = 0; i < ents.Count; i++)
      {
        if (ents[i].WorkDone.ToLower().Contains("acme"))
        {
          ttl += ents[i].HoursWorked;
        }
      }
      Console.WriteLine("Simulating Sending email to Acme");
      Console.WriteLine("Your bill is $" + ttl * 150 + " for the hours worked.");

      ttl = 0;
      for (i = 0; i < ents.Count; i++)
      {
        if (ents[i].WorkDone.ToLower().Contains("abc"))
        {
          ttl += ents[i].HoursWorked;
        }
      }
      Console.WriteLine("Simulating Sending email to ABC");
      Console.WriteLine("Your bill is $" + ttl * 125 + " for the hours worked.");

      ttl = 0;
      for (i = 0; i < ents.Count; i++)
      {
        ttl += ents[i].HoursWorked;
      }
      if (ttl > 40)
      {
        Console.WriteLine("You will get paid $" + (((ttl - 40) * 15) + (40 * 10)) + " for your work.");
      }
      else
      {
        Console.WriteLine("You will get paid $" + ttl * 10 + " for your time.");
      }
      Console.WriteLine();
      Console.Write("Press any key to exit application...");
      Console.ReadKey();
    }

    private static List<TimeSheetEntry> LoadTimeSheets()
    {
      List<TimeSheetEntry> output = new List<TimeSheetEntry>();
      string enterMoreTimeSheets = "";
      do
      {
        Console.Write("Enter what you did: ");
        string workDone = Console.ReadLine();
        Console.Write("How long did you do it for: ");
        string rawHoursWorked = Console.ReadLine();
        double hoursWorked;

        while (double.TryParse(rawHoursWorked, out hoursWorked) == false)
        {
          Console.WriteLine();
          Console.WriteLine("Invalid number given");
          Console.Write("How long did you do it for: ");
          rawHoursWorked = Console.ReadLine();
        }

        TimeSheetEntry timeSheet = new TimeSheetEntry();
        timeSheet.HoursWorked = hoursWorked;
        timeSheet.WorkDone = workDone;
        output.Add(timeSheet);
        Console.Write("Do you want to enter more time (yes/no): ");
        enterMoreTimeSheets = Console.ReadLine();
      } while (enterMoreTimeSheets == "yes");

      return output;
    }
  }

  class TimeSheetEntry
  {
    public double HoursWorked { get; set; }
    public string WorkDone { get; set; }
  }
}
