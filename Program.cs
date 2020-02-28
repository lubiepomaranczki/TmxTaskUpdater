using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using Refit;
using TmxTaskUpdater.Api;
using TmxTaskUpdater.Dto;
using TmxTaskUpdater.Helpers;

namespace TmxTaskUpdater
{
    class MainClass
    {
        private static string sharedQueryId = "{your-query-id}";

        public static void Main(string[] args)
        {
            const string footballPracticePath = @"/Users/lukaszlawicki/Documents/Coding/FootballPractice";

            var todos = new List<TodoTask>();
            IList<FileInfo> csharpFileInfo = new List<FileInfo>();

            string[] folders = Directory.GetDirectories(footballPracticePath, "*", SearchOption.AllDirectories);
            foreach (var folder in folders)
            {
                var currentDirectory = new DirectoryInfo(folder);

                foreach (var file in currentDirectory.GetFiles("*.cs"))
                {
                    csharpFileInfo.Add(file);
                }
            }

            foreach (var file in csharpFileInfo)
            {
                using (StreamReader sr = new StreamReader(file.FullName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.ToLower().Contains("todo") && !line.ToLower().Contains("todownload"))
                        {
                            todos.Add(new TodoTask(line.Trim().Substring(line.Trim().ToLower().IndexOf("todo")), file.Name));
                        }
                    }
                }
            }

            foreach (var todo in todos)
            {
                Console.WriteLine($"In {todo.FileName}: {todo.TodoComment}");
            }

            Console.WriteLine($"{todos.Count} TODOs");
        }

        public class TodoTask
        {
            public TodoTask(string todoComment, string file)
            {
                TodoComment = todoComment;
                FileName = file;
            }

            public string TodoComment { get; }
            public string FileName { get; }
        }

        static async Task MainAsync()
        {
            var azureDevOpsApi = RestService.For<IAzureDevOpsApi>("https://dev.azure.com");

            var token = TokenGenerator.GenerateToken();

            if (sharedQueryId == "{your-query-id}")
            {
                throw new ArgumentNullException($"In order to run shared query please provide {nameof(sharedQueryId)}");
            }

            var list = await azureDevOpsApi.FilterWithExisitinQuery(sharedQueryId, token);

            var itemsCount = 0;
            foreach (var item in list.WorkItems)
            {
                Console.WriteLine($"Updating work item with ID: {item.Id} 🚨");

                await azureDevOpsApi.UpdateItem(item.Id.ToString(), new List<UpdateItemRequest>
                {
                    new UpdateItemRequest { Operation = "add", Path = "/fields/System.State", Value = "Done" },
                    new UpdateItemRequest { Operation = "add", Path = "/fields/System.History", Value = "Updated State from AzureDevOps API. If something is wrong - sorry! 😇" }
                }, token);

                itemsCount++;
            }
            Console.WriteLine($"{itemsCount.ToString()} items were updated!");
        }
    }
}
