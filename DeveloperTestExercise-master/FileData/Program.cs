using System;
using System.Collections.Generic;
using System.IO;
using ThirdPartyTools;

namespace FileData
{
    public static class Program
    {
        private const int Functionality = 0;
        private const int FilePath = 1;
        private const int ArgumentsAllowed = 2;
        private const int NoArguments = 0;
        private const int InvalidIndex = -1;
        private const string RetryCapitalised = "R";
        private const string Retry = "r";

        private static readonly List<string> VersionValueList = new List<string>()
        {
            "-v", "--v", "/v", "--version"
        };

        private static readonly List<string> SizeValueList = new List<string>()
        {
            "-s", "--s", "/s", "--size"
        };

        public static void Main(string[] args)
        {
            var loopCondition = true;
            var obj = new FileDetails();

            while (loopCondition)
            {
                if (ValidateArguments(args))
                {
                    if (VersionValueList.Contains(args[Functionality]))
                        Console.WriteLine("Version: " + obj.Version(args[FilePath]));

                    if (SizeValueList.Contains(args[Functionality]))
                        Console.WriteLine("File Size: " + obj.Size(args[FilePath]));
                }
                Console.WriteLine("\n----------------------\n");
                Console.WriteLine("Press R or r to retry. Press any other key to exit the application.");
                Console.WriteLine("\n");
                var selection = Console.ReadLine();
                Console.WriteLine("\n");
                switch (selection)
                {
                    case RetryCapitalised:
                    case Retry:
                        Console.WriteLine("Enter the arguments:");
                        var userInputs = Console.ReadLine();
                        if (ValidateRetriedArguments(args, userInputs))
                        {
                            Console.WriteLine("Please try again.");
                            goto case Retry;
                        }
                        Console.WriteLine("\n");
                        break;
                    default:
                        loopCondition = false;
                        break;
                }
            }
        }

        private static bool ValidateRetriedArguments(string[] args, string userInputs)
        {
            var read = userInputs?.Split(' ');
            if (read != null && read.Length == ArgumentsAllowed)
            {
                args[Functionality] = read[Functionality];
                args[FilePath] = read[FilePath];
            }
            else
            {
                return true;
            }
            return false;
        }

        private static bool ValidateArguments(string[] args)
        {
            try
            {
                if (args.Length == NoArguments)
                    throw new IndexOutOfRangeException("No Arguments provided");

                if (args.Length > ArgumentsAllowed)
                    throw new ArgumentException("Exceeded 2 Arguments");

                if (args[FilePath].IndexOfAny(Path.GetInvalidFileNameChars()) == InvalidIndex)
                    throw new Exception("Invalid File Path");

                //Check for valid operations
                if (!VersionValueList.Contains(args[Functionality]) && !SizeValueList.Contains(args[Functionality]))
                    throw new Exception("Functionality not found");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
