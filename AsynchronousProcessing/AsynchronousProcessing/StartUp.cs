using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AsynchronousProcessing
{
    public class StartUp
    {
        public static async Task Main()
        {
            //Example 1
             int n = int.Parse(Console.ReadLine());
             SyncPrintNumbers(0, 100);
             var task = Task.Run(() => SyncPrintNumbers(101, 200));
             task.Wait();
             Console.WriteLine("Done");

            //Example 2
            CreateThread();

            //Example 3
             int startNum = int.Parse(Console.ReadLine());
             int endNum = int.Parse(Console.ReadLine());
             PrintNumbersThread(startNum,endNum);

            //Example 4
             DownloadFileAsync("url","fileName");
             Console.WriteLine("I write text during downloading...");

            await GetAsync();
            await PostAsync("https://softuni.bg", "data");
            Console.WriteLine(await GetWebsiteHtmlAsync("https://softuni.bg"));
            Console.WriteLine(await ReadFileAsync("../../../simpleText.txt"));
            await SaveFileAsync("../../../simpleText.txt", new byte[200]);

            DataParallelism();
        }

        public static void SyncPrintNumbers(int startNum, int endNum)
        {
            for (int i = startNum; i <= endNum; i++)
            {
                Console.WriteLine(i);
            }
        }

        public static void CreateThread()
        {
            Thread thread = new Thread(() =>
            {
                SyncPrintNumbers(1, 1000);
            });
            thread.Start();

            Console.WriteLine("Waiting fo thread to finish work...");

            thread.Join();
        }

        public static void PrintNumbersThread(int startNum, int endNum)
        {
            Thread evens = new Thread(() =>
            {
                SyncPrintNumbers(startNum, endNum);
            });

            evens.Start();
            evens.Join();

            Console.WriteLine("Thread finished work.");
        }

        public static async Task DownloadFileAsync(string url, string fileName)
        {
            Console.WriteLine("Downloading...");

            await Task.Run(() =>
            {
                //Download the file...
            });

            Console.WriteLine("Download successfull.");
        }

        public static async Task GetAsync(string uri = "https://httpbin.org/get")
        {
            using HttpClient httpClient = new HttpClient();

            using var response = await httpClient.GetAsync(new Uri(uri));

            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
        }

        public static async Task PostAsync(string url, string data)
        {
            HttpClient httpClient = new HttpClient();

            StringContent stringContent = new StringContent(data);

            var response = await httpClient.PostAsync(new Uri(url), stringContent);

            string responseBody = await response.Content.ReadAsStringAsync();

            Console.WriteLine(responseBody);
        }

        public static async Task<string> GetWebsiteHtmlAsync(string url)
        {
            HttpClient httpClient = new HttpClient();

            string websiteHtml = await httpClient.GetStringAsync(url);

            Console.WriteLine("Downloaded html.");

            return websiteHtml;
        }

        public static async Task<string> ReadFileAsync(string fileName)
        {
            byte[] result;

            Console.WriteLine("Reading...");

            using FileStream reader = File.Open(fileName, FileMode.Open);

            result = new byte[reader.Length];
            await reader.ReadAsync(result, 0, (int)reader.Length);

            Console.WriteLine("File readed.");

            return System.Text.Encoding.UTF8.GetString(result);
        }

        public static async Task SaveFileAsync(string fileName, byte[] data)
        {
            using FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate);

            int length = data.Length;

            await stream.WriteAsync(data, 0, length);

            Console.WriteLine("File is saved.");
        }

        public static void DataParallelism()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };


            Parallel.For(1, numbers.Count, i =>
            {
                while (numbers.Count > 0)
                {
                    lock (new object())
                    {
                        numbers.RemoveAt(numbers.Count - 1);
                    }
                }
            });

            Parallel.ForEach(numbers, item =>
            {
                while (numbers.Count > 0)
                {
                    lock (new object())
                    {
                        numbers.RemoveAt(numbers.Count - 1);
                    }
                }
            });
        }
    }
}
