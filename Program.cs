using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Refit;
using TmxTaskUpdater.Api;
using TmxTaskUpdater.Dto;

namespace TmxTaskUpdater
{
    class MainClass
    {
        private static string sharedQueryId = "{your-query-id}"; //Readme for more info.

        public static void Main(string[] args)
        {
            Console.WriteLine("Start running TmxTaskUpdater! 🚧");

            MainAsync().GetAwaiter().GetResult();

            Console.WriteLine("Ended running TmxTaskUpdater! 🦄");
        }

        static async Task MainAsync()
        {
            var azureDevOpsApi = RestService.For<IAzureDevOpsApi>("https://dev.azure.com");

            var list = await azureDevOpsApi.FilterWithExisitinQuery(sharedQueryId);

            var itemsCount = 0;
            foreach (var item in list.WorkItems)
            {
                Console.WriteLine($"Updating work item with ID: {item.Id} 🚨");

                await azureDevOpsApi.UpdateItem(item.Id.ToString(), new List<UpdateItemRequest>
                {
                    new UpdateItemRequest { Operation = "add", Path = "/fields/System.State", Value = "Done" },
                    new UpdateItemRequest { Operation = "add", Path = "/fields/System.History", Value = "Updated State from AzureDevOps API. If something is wrong - sorry! 😇" }
                });

                itemsCount++;
            }
            Console.WriteLine($"{itemsCount.ToString()} items were updated!");
        }
    }
}
