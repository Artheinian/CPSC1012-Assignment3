// See https://aka.ms/new-console-template for more information
string mainMenuChoice;
string analysisMenuChoice;
bool displayMainMenu = true;
bool displayAnalysisMenu = false;
bool quit = false ;

 int salesdateMax = 31;

 double[] sales = new double[salesdateMax];
 string[] dates = new string[salesdateMax];

string month;
string year;
string filename;
int count = 0;
bool proceed;
double mean;
double largest;
double smallest;

DisplayProgramIntro();

DisplayMainMenu();

while (displayMainMenu)
{
    try
    {
        mainMenuChoice = Prompt("Enter MAIN MENU option ('D' to display menu): ").ToUpper();
        Console.WriteLine();
        if (mainMenuChoice == "N")
        {
          proceed = NewEntryDisclaimer();

			if (proceed)
			{
				// TODO: uncomment the following and call the EnterSalesEntries method below
				count = EnterSalesEntries(sales, dates);
				Console.WriteLine();
				Console.WriteLine($"Entries completed. {count} records in temporary memory.");
				Console.WriteLine();
			}
			else
			{
				Console.WriteLine("Cancelling new data entry. Returning to MAIN MENU.");
			}
        }
        else if (mainMenuChoice == "S")
         {
            Console.WriteLine("You picked S");
              }
        else if (mainMenuChoice == "E")
         {
            Console.WriteLine("You picked E");
              }
        else if (mainMenuChoice == "L")
         {
            Console.WriteLine("You picked L");
              }            
        else if (mainMenuChoice == "V")
         {
            Console.WriteLine("You picked V");
              }      
        else if (mainMenuChoice == "M")
            {
            displayAnalysisMenu = true;
            while (displayAnalysisMenu)
                {
                    try {
                        DisplayAnalysisMenu();
                            analysisMenuChoice = Prompt("Enter ANALYSIS sub-menu option: ").ToUpper();
                            Console.WriteLine();
                        if (analysisMenuChoice == "A")
                        {
                            Console.WriteLine("You picked A");
                             }
                        else if (analysisMenuChoice == "H")
                        {
                            Console.WriteLine("You picked H");
                             }
                        else if (analysisMenuChoice == "L")
                        {
                            Console.WriteLine("You picked L");
                             }
                        else if (analysisMenuChoice == "G")
                        
                        {
                            Console.WriteLine("You picked G");
                             }
                        else if (analysisMenuChoice == "R")
                        {
                            displayAnalysisMenu = false;
                             }
                        else
                        {
                            Console.WriteLine("Invalid option. Please enter a valid option.");
                             }
                            } catch (Exception ex)
                                {
                                    Console.WriteLine($"{ex.Message}");
                                }
                }
            }
        else if (mainMenuChoice == "D")
         {
            DisplayMainMenu();
              }
        else if (mainMenuChoice == "Q")
        {
            quit = Prompt("Are you sure you want to quit (y/N)? ").ToLower().Equals("y");
            Console.WriteLine();
            if (quit)
            {
                displayMainMenu = false;
            }

        }
        else
        {
            Console.WriteLine("Invalid option. Please enter a valid option.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"{ex.Message}");
    }
}

void DisplayMainMenu()
{
    Console.WriteLine("====== Menu Options ======");
    Console.WriteLine("  [N]ew Daily Sales Entry      ");
    Console.WriteLine("  [S]ave Entries to File       ");
    Console.WriteLine("  [E]dit Sales Entries       ");
    Console.WriteLine("  [L]oad Sales File            ");
    Console.WriteLine("  [V]iew Entered/Loaded Sales  ");
    Console.WriteLine("  [M]onthly Statistics         ");
    Console.WriteLine("  [D]isplay Main Menu          ");
    Console.WriteLine("  [Q]uit Program               ");
    Console.WriteLine("                               ");
}

void DisplayAnalysisMenu()
{
    Console.WriteLine("====== Menu Options ======");
    Console.WriteLine("  [A]verage Sales      ");
    Console.WriteLine("  [H]ighest Sales       ");
    Console.WriteLine("  [L]owest Sales           ");
    Console.WriteLine("  [G]raph Sales  ");
    Console.WriteLine("  [R]eturn to MAIN MENU         ");
}

void DisplayProgramIntro()
{
    Console.WriteLine("========================================");
    Console.WriteLine("=                                      =");
    Console.WriteLine("=            Monthly  Sales            =");
    Console.WriteLine("=                                      =");
    Console.WriteLine("========================================");
}



static bool NewEntryDisclaimer()
{
	bool response;
	Console.WriteLine("Disclaimer: proceeding will overwrite all unsaved data.");
	Console.WriteLine("Hint: Select EDIT from the main menu instead, to change individual days.");
	Console.WriteLine("Hint: You'll need to enter data for the whole month.");
	Console.WriteLine();
	response = Prompt("Do you wish to proceed anyway? (y/N) ").ToLower().Equals("y");
	Console.WriteLine();
	return response;
}

static int EnterSalesEntries(double[] sales, string[] dates) 
{
     Console.WriteLine("Enter daily sales entries for the month:");
    for (int i = 0; i < sales.Length; i++)
    {
        Console.WriteLine($"Day {i + 1}:");
        sales[i] = PromptDouble("Enter sales amount: ");
        dates[i] = Prompt($"Enter date for day {i + 1}: ");
    }
    return sales.Length;
}

static string Prompt(string prompt)
{   
    string response = "";
    Console.Write(prompt);
    return Console.ReadLine();
}

static double PromptDouble(String msg)
{
  bool inValidInput = true;
  double numDouble = 0;
  while (inValidInput)
  {
    try
    {
      Console.Write(msg);
      numDouble = double.Parse(Console.ReadLine());
      inValidInput = false; 
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Invalid: {ex.Message}");
    }
  }
  return numDouble;
}

static int PromptInt(String msg)
{
  bool inValidInput = true;
  int numInt = 0;
  while (inValidInput)
  {
    try
    {
      Console.Write(msg);
      numInt = int.Parse(Console.ReadLine());
      if (numInt < 0)
        throw new Exception("Must be bigger than zero.");
      inValidInput = false; 
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Invalid: {ex.Message}");
    }
  }
  return numInt;
}

createFile();

void createFile(){
   try
  {
    const string fileName = "Sample.dat"; 
    string[] csvLines = new string[salesdateMax];
    csvLines[0] = "Data, Amount";
    File.WriteAllLines(fileName, csvLines);
    Console.WriteLine($"Data successfully written to file at: {Path.GetFullPath(fileName)}");
  }
  catch (Exception ex)
  {
    Console.WriteLine($"Exception in demo1: {ex.Message}");
  }
}





