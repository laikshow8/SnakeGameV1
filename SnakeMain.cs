namespace Snake
{
    internal class Snake
    {
        public const bool ContinueGame = true;
        private static int sizeX = Console.WindowWidth;
        private static int sizeY = Console.WindowHeight;
        private static int posx, posy, posAppleX, posAppleY, inputx, inputy;
        private static List<int[]> tale = new List<int[]>();
        private static async Task Main(string[] args)
        {
            var s = new Snake();
            await s.StartGame();
        }

        public async Task StartGame()
        {
            Console.Clear();
            Frame();
            AppleGenerator();
            while (ContinueGame)
            {
                await MoveAction();
                if (Console.KeyAvailable) Move();
            }
        }

        public static void Frame()
        {
            string startGame = "Click enter key to start the game!";
            Console.SetCursorPosition((sizeX - startGame.Length) / 2, (sizeY - 1) / 2);
            Console.Write(startGame);
            Console.Read();
            Console.Clear();
            for (int i = 0; i < sizeX; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("=");
                Console.SetCursorPosition(i, sizeY - 1);
                Console.Write("=");
            }
            for (int i = 0; i < sizeY; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("|");
                Console.SetCursorPosition(sizeX - 1, i);
                Console.Write("|");
            }
            posx = sizeX / 2;
            posy = sizeY / 2;
        }

        public static async
            Task
            MoveAction()
        {
            await Task.Delay(100);
            posx += inputx;
            posy += inputy;
            if (posy < 1 || posx < 1 || posy >= (sizeY - 1) || posx >= (sizeX - 1)) StopGame();
            if (tale.Count > 0)
            {
                int[] last = tale[tale.Count - 1];
                Console.SetCursorPosition(last[0], last[1]);
                Console.Write(" ");
            }


            for (int i = tale.Count - 1; 0 < i; i--)
            {
                tale[i][0] = tale[i - 1][0];
                tale[i][1] = tale[i - 1][1];
                Console.SetCursorPosition(tale[i][0], tale[i][1]);
                Console.Write("O");
            }


            var posHead = new int[] { posx, posy };
            if (tale.Count == 0) tale.Add(posHead);
            else tale[0] = posHead;
            Console.SetCursorPosition(tale[0][0], tale[0][1]);
            Console.Write("O");

            if (posx == posAppleX && posy == posAppleY)
            {
                int[] posNew = new int[] { tale[tale.Count - 1][0] - inputx, tale[tale.Count - 1][1] - inputy };

                tale.Add(posNew);

                tale[0][0] = posAppleX;
                tale[0][1] = posAppleY;
                AppleGenerator();
            }

            if (tale.Count > 1)
            {
                for (int i = 1; i < tale.Count; i++)
                {
                    if (posx == tale[i][0] && posy == tale[i][1]) StopGame();
                }
            }

            Console.SetCursorPosition(posAppleX, posAppleY);

            Console.Write("A");
            Console.SetCursorPosition(1, 1);
            Console.Write("Score is: " + tale.Count);
        }

        public static void Move()
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.RightArrow:
                    inputx = 1;
                    inputy = 0;
                    break;
                case ConsoleKey.LeftArrow:
                    inputx = -1;
                    inputy = 0;
                    break;
                case ConsoleKey.UpArrow:
                    inputx = 0;
                    inputy = -1;
                    break;
                case ConsoleKey.DownArrow:
                    inputx = 0;
                    inputy = 1;
                    break;
            }
        }

        public static void AppleGenerator()
        {
            var rand = new Random();
            posAppleX = rand.Next(1, sizeX - 1);
            posAppleY = rand.Next(1, sizeY - 1);
        }

        public static void StopGame()
        {
            Console.Clear();
            Console.WriteLine("Game Over! Press Enter to exit!");
            Console.Read();
            Environment.Exit(0);
        }
    }
}