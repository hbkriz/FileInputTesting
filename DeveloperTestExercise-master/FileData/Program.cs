using System;

namespace FileData
{
    public static class Program
    {
        private static readonly FileInformation FileInfoObject = new FileInformation();
        public static void Main(string[] args)
        {
            Console.Write("Application Started :-\n----------------------------\n\n");
            FileInfoObject.FileInfoProcess(args);
            Console.Write("Application Stopped :-\n----------------------------\n\n");
        }
    }
}
