using System;

namespace AppCore.Utilities
{
    public class Helper
    {
        public static bool ValidUserInput(ConsoleKeyInfo userInput, string outputMessage)
        {
            if (!char.IsNumber(userInput.KeyChar))
            {
                if(userInput.Key != ConsoleKey.N)
                    System.Console.Write($"\n {outputMessage} \n");

                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool ValidUserInput(string userInput, string outputMessage)
        {
            if (!int.TryParse(userInput,out _))
            {
                System.Console.Write($"\n {outputMessage} \n");

                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
