using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using System.Collections.Generic;

namespace PatientManagementSystem
{
  static class Program
  {
    static void Main(string[] args)
    {
      while(true)
      {
        Console.WriteLine(" Welcome to Patient Management System ");
        Console.WriteLine();
        //Console.WriteLine(" ************************************ ");
        Console.WriteLine();
        Console.WriteLine("1. Insert ");
        Console.WriteLine("2. Print ");
        Console.WriteLine("3. Update ");
        Console.WriteLine("4. Remove ");
        Console.WriteLine("5. Exit");
        Console.WriteLine();
        Console.Write("Enter your choice: ");
        string? choice = Console.ReadLine();
        switch (choice)
        {
          case "1":
            InsertData();
            break;
          case "2":
            PrintData();

            break;
          case "3":
            UpdateData();

            break;
          case "4":
            RemoveData();
            break;
          case "5":
            Environment.Exit(0);
            break;
          default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
        }
      }
    }

    private static void InsertData()
    {
      using (var context = new PatientContext())
      {
        // Creates the database if not exists
        context.Database.EnsureCreated();

        Console.WriteLine("Enter the Patient ID :");
        int PatientID = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter the Patient Name :");
        string? PatientName = Console.ReadLine();
         Console.WriteLine("Enter the Patient Age :");
        int PatientAge = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter the Patient Email :");
        string? PatientEmail = Console.ReadLine();
        Console.WriteLine("Enter the Patient Blood Group:");
        string? PatientBloodGroup = Console.ReadLine();
        Console.WriteLine("Enter the Patient Location :");
        string? PatientLocation = Console.ReadLine();

        context.Patients.Add(new Patient
        {
          PatientID = PatientID,
          PatientName = PatientName,
          PatientAge = PatientAge,
          PatientEmail = PatientEmail,
          PatientBloodGroup = PatientBloodGroup,
          PatientLocation = PatientLocation
        });

        // Saves changes
        context.SaveChanges();
      }
    }

    private static void PrintData()
    {
      // Gets and prints all books in database
      using (var context = new PatientContext())
      {
        var Patients = context.Patients;
        foreach (var Patient in Patients)
        {
          var data = new StringBuilder();
          data.AppendLine($"Patient ID: {Patient.PatientID}");
          data.AppendLine($"Patient Name: {Patient.PatientName}");
          data.AppendLine($"Patient Age: {Patient.PatientAge}");
          data.AppendLine($"Patient Email: {Patient.PatientEmail}");
          data.AppendLine($"Patient Blood Group: {Patient.PatientBloodGroup}");
          data.AppendLine($"Patient Location: {Patient.PatientLocation}");
          Console.WriteLine(data.ToString());
        }
      }
    }

    private static void UpdateData()
    {
      using (var context = new PatientContext())
      {
        // Display current data
        Console.WriteLine("Current Patients:");
        PrintData();

        Console.Write("Enter Patient ID to update the Patient Details : ");
        int isbnToUpdate = Convert.ToInt32(Console.ReadLine());
       
        var PatientToUpdate = context.Patients.FirstOrDefault(b => b.PatientID == isbnToUpdate);
        // Display current Patient details
        if (PatientToUpdate != null)
        {
          Console.WriteLine("Updating the tables: ");
          Console.WriteLine("What you want to update");
          Console.WriteLine("1. Patient Name");
          Console.WriteLine("2. Patient Age");
          Console.WriteLine("3. Patient Email");
          Console.WriteLine("4. Patient Blood Group");
          Console.WriteLine("5. Patient Location");
          string? updatechoice = Console.ReadLine();
          string? updatedata = null;
          switch (updatechoice)
          {
            case "1":
              updatedata = "PatientName";
              Console.WriteLine($"Current Name: {PatientToUpdate.PatientName}");
              Console.Write("Enter new Name: ");
              string? newName = Console.ReadLine();
              PatientToUpdate.PatientName = newName;
              break;
            case "2":
              updatedata = "PatientAge";

              Console.WriteLine($"Current Patient Age: {PatientToUpdate.PatientAge}");
              Console.Write("Enter new Patient Age: ");
              int newPatientAge =Convert.ToInt32(Console.ReadLine());
              PatientToUpdate.PatientAge = newPatientAge;
              break;
            case "3":
              updatedata = "PatientEmail";
              Console.WriteLine($"Current Email: {PatientToUpdate.PatientEmail}");
              Console.Write("Enter new Email: ");
              string? newEmail = Console.ReadLine();
              PatientToUpdate.PatientEmail = newEmail;
              break;
            case "4":
              updatedata = "PatientBloodGroup";
              Console.WriteLine($"Current Blood Group is: {PatientToUpdate.PatientBloodGroup}");
              Console.Write("Enter valid Blood Group : ");
              string? newBloodGroup = Console.ReadLine();
              PatientToUpdate.PatientBloodGroup = newBloodGroup;
              break;
               case "5":
              updatedata = "PatientLocation";
              Console.WriteLine($"Current Location is: {PatientToUpdate.PatientLocation}");
              Console.Write("Enter valid Location: ");
              string? newLocation = Console.ReadLine();
              PatientToUpdate.PatientLocation = newLocation;
              break;

          }
          context.SaveChanges();
        }

        else
        {
          Console.WriteLine($"Patient with Patient ID {isbnToUpdate} is not found.");
        }
      }
    }
    private static void RemoveData()
    {
      using (var context = new PatientContext())
      {
        // Display current data
        Console.WriteLine("Current Patients:");
        PrintData();

        // Get Patient ID from user for the Patient to remove
        Console.Write("Enter Patient ID to remove: ");
        int isbnToRemove = Convert.ToInt32(Console.ReadLine());

        // Retrieve the Patient by Patient ID for removal
        var PatientToRemove = context.Patients.FirstOrDefault(b => b.PatientID == isbnToRemove);

        if (PatientToRemove != null)
        {
          // Display details of the Patient to be removed
          Console.WriteLine($"Removing Patient - PatientID: {PatientToRemove.PatientID}, Title: {PatientToRemove.PatientID}");

          // Remove the Record
          context.Patients.Remove(PatientToRemove);

          // Save changes
          context.SaveChanges();

          Console.WriteLine("Removal successful!");
        }
        else
        {
          Console.WriteLine($"Patient with PatientID {isbnToRemove} not found.");
        }

      }

    }
  }

}
