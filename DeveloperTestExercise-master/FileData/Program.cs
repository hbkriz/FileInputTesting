using System;

namespace FileData
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Application Started :-\n----------------------------\n\n");
            var fileInfoObject = new FileInformation();
            fileInfoObject.FileInfoProcess(args);
            Console.Write("Application Stopped :-\n----------------------------\n\n");
        }

       
    }
}
