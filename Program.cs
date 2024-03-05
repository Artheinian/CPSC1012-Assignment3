// See https://aka.ms/new-console-template for more information
string mainMenuChoice;
string analysisMenuChoice;
bool displayMainMenu = true;
bool displayAnalysisMenu;
bool quit;

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



void DisplayMainMenu()
{
    while (displayMainMenu)
    {
        mainMenuChoice = Prompt("Enter MAIN MENU option ('D' to display menu): ").ToUpper();
        Console.WriteLine();
        switch (mainMenuChoice)
        {
            case "N":

                Console.WriteLine("You picked N");
                break;
            case "Q": //[Q]uit Program
                quit = Prompt("Are you sure you want to quit (y/N)? ").ToLower().Equals("y");
                Console.WriteLine();
                if (quit)
                {
                    displayMainMenu = false;
                }
                break;
            default: //invalid entry. Reprompt.
                Console.WriteLine("Invalid reponse. Enter one of the letters to choose a menu option.");
                break;
        }

    }
}

void DisplayProgramIntro()
{
    Console.WriteLine("========================================");
    Console.WriteLine("=                                      =");
    Console.WriteLine("=            Monthly  Sales            =");
    Console.WriteLine("=                                      =");
    Console.WriteLine("========================================");
}



string Prompt(string prompt)
{
    string response = "";
    Console.Write(prompt);
    response = Console.ReadLine();
    return response.ToUpper();
}

