// HelperFunctions.cs
using System;

namespace Pixhell.HelperFunctions  // Add the opening brace here
{
    public static class HelperFunctions
    {
        // Generates a random string of a specified length
        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";  // Set of characters to choose from
            Random random = new Random();
            string newString = "";

            for (int i = 0; i < length; i++)
            {
                newString += chars[random.Next(chars.Length)];  // Randomly pick a character from the set
            }

            return newString;  // Return the generated string
        }
    }
}