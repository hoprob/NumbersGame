using System;
//Robin Svensson SUT21
namespace NumbersGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\t\t≡≡≡Välkommen!≡≡≡ \nI detta spelet skall du" +
                " försöka att gissa rätt nummer!\n");
            Console.WriteLine("HELLO BUG");
            bool isRunning = true;
            bool rightGuess; //Bool för att veta om användaren gissat rätt
            int number; //Det "rätta" numret
            int difficulty; //Svårighetsgrad/Antal nummer att gissa mellan
            int attempts; //Antal försök att gissa rätt
            int attemptsMade;//Antal försök gjorda
            while (isRunning)
            {
                //Låter användaren bestämma svårightesgrad
                difficulty = Difficulty();
                //Omvandlar svårighetsgrad/Antal nummer till antal försök
                attempts = Attempts(difficulty);
                Console.WriteLine($"\nNumret är mellan 1 och {difficulty}!" +
                    $" Du får {attempts} försök! ");
                attemptsMade = 0;
                rightGuess = false;
                //Hämtar det "rätta" numret
                number = GetRandomNum(difficulty);
                //Medans användaren har försök kvar och inte gissar rätt
                while (attemptsMade < attempts && !rightGuess)
                {
                    int userInput = GetUserNum(attemptsMade, attempts);
                    rightGuess = CheckGuess(number, userInput);
                    attemptsMade++;
                }
                //Om användaren inte lyckats gissa rätt inom sina försök
                if (attemptsMade == attempts && !rightGuess)
                {
                    Console.WriteLine($"Tyvärr du lyckades inte gissa talet på" +
                        $" {attempts} försök!");
                }
                //Kollar om användaren vill spela igen
                isRunning = PlayAgain(isRunning);
            }
            
        }
        //Kollar användarens gissning mot drt "rätta" numret
        static bool CheckGuess(int number, int userInput)
        {
            bool rightGuess = false;
            if (number == userInput)
            {
                Console.WriteLine(Answer(1));
                rightGuess = true;
            }
            else if (number > userInput)
            {
                //Nära gissning för lågt
                if (number - 3 < userInput)
                    Console.WriteLine(Answer(4));
                else
                    Console.WriteLine(Answer(2));
            }
            else
            {
                //Nära gissning för högt
                if (number + 3 > userInput)
                    Console.WriteLine(Answer(5));
                else
                    Console.WriteLine(Answer(3));
            }
            return rightGuess;
        }
        //Hämtar användarens gissning
        static int GetUserNum(int attemptsMade, int attempts)
        {
            int userInput;
            Console.Write($"Du har {attempts - attemptsMade} gissningar kvar!" +
                $"\n\nMata in en siffra: ");
            while (!Int32.TryParse(Console.ReadLine(), out userInput))
            {
                Console.Write("\n\tERROR! Du måste skriva in en siffra!" +
                    "\nFörsök igen: ");
            }
            return userInput;
        }
        //Hämtar det "rätta" numret
        static int GetRandomNum(int difficulty)
        {
            var random = new Random();
            int number = random.Next(1, difficulty);
            return number;
        }
        //Hämtar svårighetsgrad från användaren
        static int Difficulty()
        {
            Console.WriteLine("Välj svårighetsgrad 1, 2, 3, 4 eller 5: ");
            int userInput;
            
            while(!Int32.TryParse(Console.ReadLine(), out userInput) ||
                userInput > 5 || userInput < 1)
            {
                Console.Write("\n\tERROR! Du måste skriva in siffran" +
                    " 1, 2, 3, 4 eller 5!\nFörsök igen: ");
            }
            //Gångrar med 10 för att få antal nummer att gissa mellan.
            return userInput * 10;      
        }
       
        //Gör om svårighetsgraden/Antal nummer (10 - 50) till antal försök
        static int Attempts(int difficulty)
        {
            int attempts = 5;

            switch(difficulty)
            {
                case 10:
                    attempts = 5;
                    break;
                case 20:
                    attempts = 6;
                    break;
                case 30:
                    attempts = 7;
                    break;
                case 40:
                    attempts = 6;
                    break;
                case 50:
                    attempts = 5;
                    break;
            }
            return attempts;
        }
        //Hämtar svar till användarens gissning
        static string Answer(int i)
        {
            string answer = "";
            string[] rightGuess = { "\nWOHO! Du gjorde det!",
                "\nBRAVO! Du gissade rätt!",
                "\nWOW! Vilken stjärna! Det var rätt nummer!",
                "\nBRA JOBBAT! Rätt nummer!" };
            string[] tooLow = { "\nEn bra gissning, men tyvärr för lågt!",
                "\nTyvärr, för lågt!", "\nDet var fel nummer, för lågt!",
                "\nDu behöver gissa högre än så!",
                "\nFör lågt! Numret är högre!" };
            string[] tooHigh = { "\nOj vad högt, talet är lägre!",
                "\nGissa lägre nästa gång....",
                "\nFör högt! numret är lägre!",
                "\nTyvärr! Du gissade för högt!",
                "\nBra gissat! Men tyvärr för högt!" };
            string[] closeGuessLow = { 
                    "\nOj, nu börjar det att brännas! lite högre ändå!",
                "\nNu är du nära, lite högre!",
                "\nAJAJ Det bränns! snäppet högre!",
                "\nOj vad nära det är...upp lite.." };
            string[] closeGuessHigh = { "\nDet bränns! Litelite lägre bara..",
                "\nOj vad nära.. lite lägre...",
                "\nAJAJAJ vad det bränns!! lite lägre.....",
                "\nNu är du nära, lite lägre..." };
            Random random = new Random();
            switch(i)
            {
                case 1:
                    answer = rightGuess[random.Next(0, (rightGuess.Length - 1))];
                    break;
                case 2:
                    answer = tooLow[random.Next(0, (tooLow.Length - 1))];
                    break;
                case 3:
                    answer = tooHigh[random.Next(0, (tooHigh.Length - 1))];
                    break;
                case 4:
                    answer = closeGuessLow[random.Next(0,
                        (closeGuessLow.Length - 1))];
                    break;
                case 5:
                    answer = closeGuessHigh[random.Next(0,
                        (closeGuessHigh.Length - 1))];
                    break;
            }
            return answer;
        }
        //Kollar om användaren vill spela igen
        static bool PlayAgain(bool isRunning)
        {
            Console.Write("\n\nVill du spela igen? [Y/N]: ");
            string playAgain = Console.ReadLine().ToUpper();
            while (true)
            {
                if (playAgain == "Y")
                {
                    Console.Clear();
                    break;
                }
                else if (playAgain == "N")
                {
                    //Avslutar spelet
                    Console.Clear();
                    Console.WriteLine("\n\n\n\tTack för att du spelade! :)");
                    isRunning = false;
                    break;
                }
                else
                {
                    Console.WriteLine("\n\tError! Du måste mata in Y (ja)" +
                        " eller N (nej)!");
                    playAgain = Console.ReadLine().ToUpper();
                }
            }
            return isRunning;
        }

    }
}
