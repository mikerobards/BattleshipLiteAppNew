
using BattleshipLiteNewLibrary.Models;

WelcomeMessage();

static void WelcomeMessage()
{
    Console.WriteLine("Welcome to Battleship Lite!");
    Console.WriteLine("created by Mike Robards");
    Console.WriteLine();
}

static PlayerInfoModel CreatePlayer()
{
    PlayerInfoModel output = new PlayerInfoModel();

    // Ask the user for their name
    output.UsersName = AskForUsersName();
    // Load up the shot grid
    // Ask the user for their 5 ship placements
    // Clear the screen and done


}

static string AskForUsersName()
{
    Console.Write("What is your name: ");
    string output = Console.ReadLine();

    return output;

}

