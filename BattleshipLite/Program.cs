
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
    RecordPlayerShot(activePlayer, opponent);

    // Determine if game should continue
    bool doesGameContinue = GameLogic.PlayerStillActive(opponent);


    // If over, set activePlayer as winner
    // Else, swap positions (activePlayer to opponent)
    if (doesGameContinue == true)
    {
        // swap using a temp variable
        //PlayerInfoModel tempHolder = opponent;
        //opponent = activePlayer;
        //activePlayer = tempHolder;

        // Using tuple
        (activePlayer, opponent) = (opponent, activePlayer);

    }
    else
    {
        winner = activePlayer;
    }

} while (winner == null);

IdentifyWinner(winner);


static void IdentifyWinner(PlayerInfoModel winner)
{
    Console.WriteLine($"Congratulations to {winner.UsersName} for winning!");
    Console.WriteLine($"{winner.UsersName} took {GameLogic.GetShotCount(winner)} shots.");
}

static void RecordPlayerShot(PlayerInfoModel activePlayer, PlayerInfoModel opponent)
{

    bool isValidShot = false;
    string row = "";
    int column = 0;

    do
    {
        string shot = AskForShot();
        (row, column) = GameLogic.SplitShotIntoRowAndColumn(shot);
        isValidShot = GameLogic.ValidateShot(activePlayer, row, column);

        if (isValidShot == false)
        {
            Console.WriteLine("Invalid shot location. Please try again.");
        }

    } while (isValidShot == false);

    bool isAHit = GameLogic.IdentifyShotResult(opponent, row, column);

    GameLogic.MarkShotResult(activePlayer, row, column, isAHit) ;
}

static string AskForShot()
{
    Console.Write("Please enter your shot selection: ");
    string output = Console.ReadLine();
    return output;
}

static void DisplayShotGrid(PlayerInfoModel activePlayer)
{
    string currentRow = activePlayer.ShotGrid[0].SpotLetter;

    foreach (var gridSpot in activePlayer.ShotGrid)
    {
        if (gridSpot.SpotLetter != currentRow)
        {
            Console.WriteLine();
            currentRow = gridSpot.SpotLetter;
        }

        if (gridSpot.Status == GridSpotStatus.Empty)
        {
            Console.Write($" { gridSpot.SpotLetter }{ gridSpot.SpotNumber } ");
        }
        else if (gridSpot.Status == GridSpotStatus.Hit)
        {
            Console.Write(" X ");
        }
        else if (gridSpot.Status == GridSpotStatus.Miss)
        {
            Console.Write(" O ");
        }
        else
        {
            Console.Write(" ? ");
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