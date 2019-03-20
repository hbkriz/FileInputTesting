using System;
using System.Collections.Generic;
using System.IO;
using ThirdPartyTools;

namespace FileData
{
    public class FileInformation
    {
        private const int Functionality = 0;
        private const int FilePath = 1;
        private const int ArgumentsAllowed = 2;
        private const int NoArguments = 0;
        private const int InvalidIndex = -1;
        private const string RetryCapitalised = "R";
        private const string Retry = "r";

        private readonly List<string> VersionValueList = new List<string>()
        {
            "-v", "--v", "/v", "--version"
        };

        private readonly List<string> SizeValueList = new List<string>()
        {
            "-s", "--s", "/s", "--size"
        };

        public void FileInfoProcess(string[] args)
        {
            var loopCondition = true;
            var obj = new FileDetails();

            while (loopCondition)
            {
                if (!DisplayErrorMessage(args))
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
                        if (DisplayRetryArguments(args, userInputs))
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

        private bool DisplayRetryArguments(string[] existingArgs, string newUserInputs)
        {
            var read = newUserInputs?.Split(' ');
            if (!DisplayErrorMessage(read) && read != null)
            {
                existingArgs[Functionality] = read[Functionality];
                existingArgs[FilePath] = read[FilePath];
            }
            else
            {
                return true;
            }
            return false;
        }

        private bool DisplayErrorMessage(string[] args)
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
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return true;
            }
        }
    }
}
