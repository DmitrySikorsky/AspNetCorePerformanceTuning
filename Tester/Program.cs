using System;
using System.IO;
using System.Net;

namespace Tester
{
  class Program
  {
    static void Main(string[] args)
    {
      int requestsNumber = 1000;
      DateTime start = DateTime.Now;

      for (int i = 0; i < requestsNumber; i++)
      {
        WebRequest request = WebRequest.Create("http://localhost:60234/");

        using (WebResponse response = request.GetResponse())
        using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
        {
          streamReader.ReadToEnd();
        }
      }

      double total = (DateTime.Now - start).TotalSeconds;
      double average = total / requestsNumber;

      Console.WriteLine($"Total: {total}, average: {average}");
      //File.WriteAllText($@"{Directory.GetCurrentDirectory()}\result.txt", $"Total: {total}, average: {average}");
    }
  }
}