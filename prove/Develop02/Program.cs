
using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<Entry> journal = new List<Entry>();
    static List<string> prompts = new List<string>() {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Load the journal from a file");
            Console.WriteLine("4. Save the journal to a file");
            Console.WriteLine("5. Exit");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    WriteNewEntry();
                    break;
                case 2:
                    DisplayJournal();
                    break;
                case 3:
                    LoadJournal();
                    break;
                case 4:
                    SaveJournal();
                    break;
                case 5:
                    return;
            }
        }
    }

    static void WriteNewEntry()
    {
        Random rnd = new Random();
        int index = rnd.Next(prompts.Count);

        Console.WriteLine(prompts[index]);
        string response = Console.ReadLine();

        Entry entry = new Entry()
        {
            Prompt = prompts[index],
            Response = response,
            Date = DateTime.Now
        };

        journal.Add(entry);
    }

    static void DisplayJournal()
    {
        foreach (Entry entry in journal)
        {
            Console.WriteLine("Prompt: " + entry.Prompt);
            Console.WriteLine("Response: " + entry.Response);
            Console.WriteLine("Date: " + entry.Date);
            Console.WriteLine();
        }
    }

    static void LoadJournal()
    {
        Console.WriteLine("Enter the filename:");
        string filename = Console.ReadLine();

        using (StreamReader reader = new StreamReader(filename))
        {
            journal = new List<Entry>();
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] parts = line.Split(',');

                Entry entry = new Entry()
                {
                    Prompt = parts[0],
                    Response = parts[1],
                    Date = DateTime.Parse(parts[2])
                };

                journal.Add(entry);
            }
        }
    }

    static void SaveJournal()
    {
        Console.WriteLine("Enter the filename:");
        string filename = Console.ReadLine();

        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Entry entry in journal)
            {
                string line = entry.Prompt + "," + entry.Response + "," +