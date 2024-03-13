
// TODO: declare a constant to represent the max size of the values
// and dates arrays. The arrays must be large enough to store
// values for an entire month.
using System.Text;

int physicalSize = 31;
int logicalSize = 0;

// TODO: create a double array named 'values', use the max size constant you declared
// above to specify the physical size of the array.
double[] values = new double[physicalSize];

// TODO: create a string array named 'dates', use the max size constant you declared
// above to specify the physical size of the array.
string[] dates = new string[physicalSize];

bool goAgain = true;
  while (goAgain)
  {
    try
    {
      DisplayMainMenu();
      string mainMenuChoice = Prompt("\nEnter a Main Menu Choice: ").ToUpper();
      if (mainMenuChoice == "L")
        logicalSize = LoadFileValuesToMemory(dates, values);
      if (mainMenuChoice == "S")
        SaveMemoryValuesToFile(dates, values, logicalSize);
      if (mainMenuChoice == "D")
        DisplayMemoryValues(dates, values, logicalSize);
      if (mainMenuChoice == "A")
        logicalSize = AddMemoryValues(dates, values, logicalSize);
      if (mainMenuChoice == "E")
        EditMemoryValues(dates, values, logicalSize);
      if (mainMenuChoice == "Q")
      {
        goAgain = false;
        throw new Exception("Bye, hope to see you again.");
      }
      if (mainMenuChoice == "R")
      {
        while (true)
        {
          if (logicalSize == 0)
					  throw new Exception("No entries loaded. Please load a file into memory");
          DisplayAnalysisMenu();
          string analysisMenuChoice = Prompt("\nEnter an Analysis Menu Choice: ").ToUpper();
          if (analysisMenuChoice == "A")
            FindAverageOfValuesInMemory(values, logicalSize);
          if (analysisMenuChoice == "H")
            FindHighestValueInMemory(values, logicalSize);
          if (analysisMenuChoice == "L")
            FindLowestValueInMemory(values, logicalSize);
          if (analysisMenuChoice == "G")
            GraphValuesInMemory(dates, values, logicalSize);
          if (analysisMenuChoice == "R")
            throw new Exception("Returning to Main Menu");
        }
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"{ex.Message}");
    }
  }

void DisplayMainMenu()
{
	Console.WriteLine("\nMain Menu");
	Console.WriteLine("L) Load Values from File to Memory");
	Console.WriteLine("S) Save Values from Memory to File");
	Console.WriteLine("D) Display Values in Memory");
	Console.WriteLine("A) Add Value in Memory");
	Console.WriteLine("E) Edit Value in Memory");
	Console.WriteLine("R) Analysis Menu");
	Console.WriteLine("Q) Quit");
}

void DisplayAnalysisMenu()
{
	Console.WriteLine("\nAnalysis Menu");
	Console.WriteLine("A) Find Average of Values in Memory");
	Console.WriteLine("H) Find Highest Value in Memory");
	Console.WriteLine("L) Find Lowest Value in Memory");
	Console.WriteLine("G) Graph Values in Memory");
	Console.WriteLine("R) Return to Main Menu");
}

string Prompt(string prompt)
{
  string response = "";
  Console.Write(prompt);
  response = Console.ReadLine();
  return response;
}

// string PromptDate(string prompt)
// {
//   bool inValidInput = true;
//   DateTime date = DateTime.Today;
// }
string GetFileName()
{
	string fileName = "";
	do
	{
		fileName = Prompt("Enter file name including .csv or .txt: ");
	} while (string.IsNullOrWhiteSpace(fileName));
	return fileName;
}

int LoadFileValuesToMemory(string[] dates, double[] values)
{
	string fileName = GetFileName();
	int logicalSize = 0;
	string filePath = $"./data/{fileName}";
	if (!File.Exists(filePath))
		throw new Exception($"The file {fileName} does not exist.");
	string[] csvFileInput = File.ReadAllLines(filePath);
	for(int i = 0; i < csvFileInput.Length; i++)
	{
		Console.WriteLine($"lineIndex: {i}; line: {csvFileInput[i]}");
		string[] items = csvFileInput[i].Split(',');
		for(int j = 0; j < items.Length; j++)
		{
			Console.WriteLine($"itemIndex: {j}; item: {items[j]}");
		}
		if(i != 0)
		{
		dates[logicalSize] = items[0];
    values[logicalSize] = double.Parse(items[1]);
    logicalSize++;
		}
	}
  Console.WriteLine($"Load complete. {fileName} has {logicalSize} data entries");
	return logicalSize;
}

void DisplayMemoryValues(string[] dates, double[] values, int logicalSize)
{
	if(logicalSize == 0)
		throw new Exception($"No Entries loaded. Please load a file to memory or add a value in memory");
	Console.WriteLine($"\nCurrent Loaded Entries: {logicalSize}");
	Console.WriteLine($"   Date     Value");
	for (int i = 0; i < logicalSize; i++)
		Console.WriteLine($"{dates[i]}   {values[i]}");
}

double FindHighestValueInMemory(double[] values, int logicalSize)
{
    double highest = values[0];
	  for (int i = 1; i < logicalSize; i++)
  {
        if (values[i] > highest)

        {
            highest = values[i];
        }

  }
    Console.WriteLine($"\nHighest value is {highest}");
    return highest;
}

double FindLowestValueInMemory(double[] values, int logicalSize)
{
	double lowest = values[0]; 

    for (int i = 1; i < logicalSize; i++)
    {
        if (values[i] < lowest)
        {
            lowest = values[i];
        }
    }

    Console.WriteLine($"The lowest value in memory is: {lowest}");
    return lowest;
}

void FindAverageOfValuesInMemory(double[] values, int logicalSize)
{

  double sum = 0;

  for (int i = 0; i < logicalSize; i++)
    {
        sum += values[i];
    }
  
  double average = sum / logicalSize;
	
   Console.WriteLine($"The average value in memory is: {average}");
}

void SaveMemoryValuesToFile(string[] dates, double[] values, int logicalSize)
{
	Console.WriteLine("Not Implemented Yet");
	//TODO: Replace this code with yours to implement this function.
}

string PromptDate(string promptdate){
  string response = "";
  try {
    Console.WriteLine(promptdate);
    response = Console.ReadLine();
  }catch (Exception ex)
  {
    Console.WriteLine(ex.Message);
  }
  return response;
}

double PromptDoubleBetweenMinMax(String msg, double min, double max)
{
  bool inValidInput = true;
  double num = 0;
  while (inValidInput)
  {
    try
    {
      Console.Write($"{msg} between {min} and {max}: ");
      num = double.Parse(Console.ReadLine());
      if (num <= min || num >= max)
        throw new Exception($"Must be between {min} and {max} exclusive");
      inValidInput = false; 
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Invalid: {ex.Message}");
    }
  }
  return num;
}
int AddMemoryValues(string[] dates, double[] values, int logicalSize)
{
	double value = 0.0;
  string dateString = "";
  double minSize = 0.0;
  double maxSize = 1000.0;
  dateString = PromptDate("Enter date format mm-dd-yyyy (e.g 11-23-2023): " );
  bool found = false;
  for(int i =0; i < logicalSize; i++)
    if (dates[i].Equals(dateString))
        found = true;
  if (found = true)
    throw new Exception($"{dateString} is already in memory. Edit entry instead");
  value = PromptDoubleBetweenMinMax($"Enter a double value between {minSize} , {maxSize}  ", minSize , maxSize);
  dates[logicalSize] = dateString;
  values[logicalSize] = value;
  logicalSize++;
  return logicalSize;
}

void EditMemoryValues(string[] dates, double[] values, int logicalSize)
{
	try
    {
        string fileName = GetFileName();
        string filePath = $"./data/{fileName}";

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"The file {fileName} does not exist.");

        string[] csvFileInput = File.ReadAllLines(filePath);

    //     // Display file content
    //     Console.WriteLine("Current content of the file:");
    //     for (int i = 0; i < csvFileInput.Length; i++)
    //     {
    //         Console.WriteLine($"Line {i + 1}: {csvFileInput[i]}");
    //     }

    //     // Prompt the user to enter the date they want to edit
    //     Console.Write("\nEnter the date (format: MM-dd-yyyy) to edit: ");
    //     string dateToEdit = Console.ReadLine().Trim(); // Trim to remove leading/trailing spaces

    //     // Find the index of the date in the dates array
    //     int index = Array.IndexOf(dates, dateToEdit);

    //     if (index != -1)
    //     {
    //         // Prompt the user to enter the new value
    //         Console.Write("Enter the new value: ");
    //         double newValue = double.Parse(Console.ReadLine());

    //         // Update the corresponding value in the values array
    //         values[index] = newValue;

    //         // Update the file content
    //         StringBuilder sb = new StringBuilder();
    //         for (int i = 0; i < csvFileInput.Length; i++)
    //         {
    //             if (i == 0 || csvFileInput[i].StartsWith(dateToEdit))
    //             {
    //                 sb.AppendLine($"{dateToEdit},{newValue}");
    //             }
    //             else
    //             {
    //                 sb.AppendLine(csvFileInput[i]);
    //             }
    //         }
    //         File.WriteAllText(filePath, sb.ToString());

    //         Console.WriteLine($"Value for date {dateToEdit} updated successfully in the file.");
    //     }
    //     else
    //     {
    //         Console.WriteLine($"Date '{dateToEdit}' not found in the file.");
    //         // Print dates array for debugging purposes
    //         Console.WriteLine("Dates in memory:");
    //         foreach (string date in dates)
    //         {
    //             Console.WriteLine(date);
    //         }
    //     }
    // }
    // catch (FormatException)
    // {
    //     Console.WriteLine("Invalid input. Please enter a valid number for the new value.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
  }


void GraphValuesInMemory(string[] dates, double[] values, int logicalSize)
{
	Console.WriteLine("Not Implemented Yet");
	//TODO: Replace this code with yours to implement this function.
}