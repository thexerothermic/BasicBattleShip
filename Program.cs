// See https://aka.ms/new-console-template for more information
using System.Collections;
namespace SimpleBattleship

{
    struct Coordinate
    {
        public int x;
        public int y;
        public Coordinate (int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        static public Coordinate Parse(string s)
        {
            try
            {
                string[] parts = s.Split(',');
                var x = int.Parse(parts[0]);
                var y = int.Parse(parts[1]);
                if (x < 0 || x > 99 || y < 0 || y > 99 || parts.Count() != 2)
                {
                    Console.WriteLine("Each number must be inbetween 0 and 99, and no extra characters are allowed.");
                    return new Coordinate(-1, -1);
                }
                return new Coordinate (x,y);
            }
            catch(Exception)
            {
                Console.WriteLine("You must enter a coordinate pair in this format, with both x and y between 0 and 99.");
                Console.WriteLine("NN,NN");
                return new Coordinate(-1,-1);
            }
        }
        public double getDistance(Coordinate c)
        {
            return Math.Pow(Math.Pow(c.x - x, 2) + Math.Pow(c.y - y, 2),.5);
        }

    }
    class Game
    {
        static public List<Coordinate> getPlayerCoordinates()
        {
            List<Coordinate> playerCoordinates = new List<Coordinate>();
            Console.WriteLine("Enter the coordinates of your five ships.");
            Console.WriteLine("The x and y coordinates range from 0 to 99.");
            Console.WriteLine("Enter in NN,NN format, and hit return after entering each coordinate.");
            //Set default value of each Coordinate to -1,-1 in order for exit condition for inner while loop to be false
            //so that while loop can serve as a check for valid data
            for (int k = 0; k < 5; k++)
            {
                playerCoordinates.Add(new Coordinate(-1, -1));
            }

            for (int i = 0; i < 5; i++)
            {
                while (playerCoordinates[i].x == -1)
                {
                    playerCoordinates[i] = Coordinate.Parse(Console.ReadLine());
                }
            }
            return playerCoordinates;
        }
        static public List <Coordinate> getComputerCoordinates()
        {
            Random rand = new Random();
            List<Coordinate> computerCoordinates = new List<Coordinate>();
            for (int i = 0; i < 5; i++)
            {
                computerCoordinates.Add(new Coordinate(rand.Next(100), rand.Next(100)));
            }
            return computerCoordinates;
        }
        static public Coordinate getGuess()
        {
            Console.WriteLine("Make a guess.");
            Coordinate guess = Coordinate.Parse(Console.ReadLine());
            while (guess.x == -1)
            {
                Console.WriteLine("Please enter valid data.");
                guess = Coordinate.Parse(Console.ReadLine());  
            }
            return guess;
        }
        static public void startMatch(List<Coordinate> playerCoordinates,List<Coordinate> computerCoordinates)
        {
            Random rand = new Random();
            while (playerCoordinates.Count > 0 && computerCoordinates.Count > 0)
            {
                Coordinate guess = getGuess();
                if (computerCoordinates.Contains(guess))
                {
                    computerCoordinates.Remove(guess);
                    Console.WriteLine($"Hit! There are {computerCoordinates.Count} enemy ships remaining.");
                }
                else
                {
                    double [] distances = new double [computerCoordinates.Count];
                    for (int k = 0; k < computerCoordinates.Count; k++)
                    {
                        distances[k] = guess.getDistance(computerCoordinates[k]);
                    }
                    Console.WriteLine($"The closest enemy ship to that location is {distances.Min()} miles away.");
                }
                if(rand.Next(6) == 5)
                {
                    Console.WriteLine($"Your ship at X:{playerCoordinates[0].x} Y:{playerCoordinates[0].x} was hit");
                    playerCoordinates.RemoveAt(0);
                    Console.WriteLine($"You have {playerCoordinates.Count} ships remaining.");
                }
                else
                {
                    Console.WriteLine("Your opponent missed.");
                }
            }
            if(playerCoordinates.Count > 0)
            {
                Console.WriteLine("You Win!");
            }
            else if(computerCoordinates.Count > 0)
            {
                Console.WriteLine("You didn't get it this time! You can try again any time.");
            }
            else
            {
                Console.WriteLine("It's a draw!");
            }
        }
        public static void Main(String[] args)
        {
            var play = "y";
            while (play == "y")
            {
                List<Coordinate> playerCoordinates = getPlayerCoordinates();
                List<Coordinate> computerCoordinates = getComputerCoordinates();
                startMatch(playerCoordinates, computerCoordinates);
                Console.WriteLine("If you would like to play another game, enter the letter y.");
                Console.WriteLine("If you would like to exit the program, enter anything else.");
                play = Console.ReadLine();
            }
        }
    }
}


