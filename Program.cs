using System;
using System.Text.RegularExpressions;
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
					  throw new Exception("No entries loaded. Please load a file into memory or add an entry to memory");
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
  Array.Sort(dates, values, 0, logicalSize);
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
  string folderPath = @"F:\CPSC1012\Github\CPSC1012-Assignment3\data";
  string filename = GetFileName();

  string fullPath = folderPath + "\\" + filename;

	string[] csvLines = new string[logicalSize + 1];
  csvLines[0] = "New Dates, New Values";
  for (int n = 0; n < logicalSize; n++)
  {
    csvLines[n + 1] = $"{dates[n]}, {values[n]}";
  }
  File.AppendAllLines(fullPath, csvLines);
  Console.WriteLine($"Data successfully written to file at: {fullPath}");

}

string PromptDate(string promptdate){
  bool inValidDate = true;
  string response = "";
  while(inValidDate)
  try {
    Console.WriteLine(promptdate);
    response = Console.ReadLine();

              
    DateTime date = DateTime.ParseExact(response, "MM-dd-yyyy", null);
    
    inValidDate = false;

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
  double maxSize = 10.0;
  dateString = PromptDate("Enter date format mm-dd-yyyy (e.g 11-23-2023): " );
  bool found = false;
  for(int i =0; i < logicalSize; i++)
  {
    if (dates[i].Equals(dateString))
    {
        found = true;
    }
  }
  if (found == true)
    throw new Exception($"{dateString} is already in memory. Edit entry instead");
  value = PromptDoubleBetweenMinMax($"Enter a double value between {minSize} , {maxSize}  ", minSize , maxSize);
  dates[logicalSize] = dateString;
  values[logicalSize] = value;
  logicalSize++;
  Console.WriteLine($"Value {value} has been added to the Date {dateString}");
  return logicalSize;
}

void EditMemoryValues(string[] dates, double[] values, int logicalSize)
{
	double value = 0.0;
  string dateString = "";
  int indexFound = 0;
  double minSize = 0.0;
  double maxSize = 10.0;

    if(logicalSize == 0)
      throw new Exception($"No Entries loaded. Please add a value into memory or load file into memory");
  dateString = PromptDate("Enter date format mm-dd-yyyy (e.g 11-23-2023): " );

  bool found = false;
  for(int i =0; i < logicalSize; i++)
    if (dates[i].Equals(dateString))
    {
        found = true;
        indexFound = i;
    }
  if (found == false)
      throw new Exception($"{dateString} is not in memory. Add entry instead. ");
    value = PromptDoubleBetweenMinMax($"Enter a double value between {minSize} and {maxSize} ", minSize, maxSize);
    values[indexFound] = value;
    Console.WriteLine($"Value {value} has been edited to the Date {dateString}");
}


void GraphValuesInMemory(string[] dates, double[] values, int logicalSize)
{
    double minValue = 0;
    double yAxisMaxValue = FindHighestValueInMemory(values, logicalSize);
    double yAxisSubract = 1;
    for (double row = yAxisMaxValue; row >= minValue; row -= yAxisSubract)
    {
        Console.Write($"\n{row:c0} |");
    }
    Console.WriteLine("");
    string lines = "---";
    int date = 0;
            for (int col = 0; col < physicalSize; col++)
        {
            lines += "---";
            date += col;
        //     for (int j = 0; j < logicalSize; j++)
        //     {
        //         string template = dates[j].Substring(3,2);
        //     }
        }
        Console.WriteLine($"{lines}");
        Console.WriteLine($" {date}", 00);

// for(int row = yAxisInMaxValue; row >= minValue; row-=yAxisInMaxValue)
//     {
//         Console.Write($"\n{row,yAxisWidth:c0} | ");
//         for (int col = 0; col < physicalSize; col++)
//             {
//                 for(int j = 0; j logicalSize; j ++)
//                 {
//                     string tempDate = dates[j].Substring(3,2);
//                     if (tempDate Substring(0,1) == 0)
//                 }
//             }
//     }

}