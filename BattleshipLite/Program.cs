
using BattleshipLiteNewLibrary;
using BattleshipLiteNewLibrary.Models;

WelcomeMessage();

PlayerInfoModel activePlayer = CreatePlayer("Player 1");
PlayerInfoModel opponent = CreatePlayer("Player 2");
PlayerInfoModel winner = null;

do
{
    // Display grid from activePlayer on where they fired
    DisplayShotGrid(activePlayer);

    // Ask activePlayer for a shot
    // Determine if it is a valid shot
    // Determine shot results
    // Determine if game is over
    // If over, set activePlayer as winner
    // Else, swap positions (activePlayer to opponent)


} while (winner == null);

static void DisplayShotGrid(PlayerInfoModel activePlayer)
{
    string currentRow = activePlayer.ShotGrid[0].SpotLetter;

    foreach (var gridSpot in activePlayer.ShotGrid)
    {
        if (gridSpot.Status == GridSpotStatus.Empty)
        {
            Console.Write($" { gridSpot.SpotLetter }{ gridSpot.SpotNumber } ");
        }
    }
}

static void WelcomeMessage()
{
    Console.WriteLine("Welcome to Battleship Lite!");
    Console.WriteLine("created by Mike Robards");
    Console.WriteLine();
}

static PlayerInfoModel CreatePlayer(string playerTitle)
{
    PlayerInfoModel output = new PlayerInfoModel();

    Console.WriteLine($"Player information for { playerTitle }");

    // Ask the user for their name
    output.UsersName = AskForUsersName();

    // Load up the shot grid
    GameLogic.InitializeGrid(output);

    // Ask the user for their 5 ship placements
    PlaceShips(output);


    // Clear the screen and done
    Console.Clear();

    return output;

}

static string AskForUsersName()
{
    Console.Write("What is your name: ");
    string output = Console.ReadLine();

    return output;

}

static void PlaceShips(PlayerInfoModel model)
{
    do
    {
        Console.Write($"Where do you want to place ship number { model.ShipLocations.Count + 1 }: ");
        string location = Console.ReadLine();

        bool isValidLocation = GameLogic.PlaceShip(model, location);

        if (isValidLocation == false)
        {
            Console.WriteLine("That was not a valid location. Please try again.");
        }

    } while (model.ShipLocations.Count < 5);
}