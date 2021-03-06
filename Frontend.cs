using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProgressAPI.Models;

namespace ProgressAPI.Frontend
{
    public static class Frontend
    {
        [FunctionName("GetAllProgress")]
        public static async Task<IActionResult> GetAllProgress(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "progress")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Looking up all media progress");
        }

        [FunctionName("GetIndividualProgress")]
        public static async Task<IActionResult> GetIndividualProgress(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "progress/{mediaId}")] HttpRequest req,
            string mediaId,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (string.IsNullOrWhiteSpace(mediaId)) {
                return new BadRequestResult();
            } else {
                return new OkObjectResult("Looking up individual media progress");
            }
        }

        [FunctionName("UpdateProgress")]
        public static void UpdateProgress(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "progress")] ProgressUpdateRequest updateRequest,
            [CosmosDB(
                databaseName: "ProgressAPI",
                collectionName: "Progress",
                ConnectionStringSetting = "CosmosDB",
                CreateIfNotExists = true)]
                out ProgressUpdateRequest outDocument,
            ILogger log)
        {
            outDocument = updateRequest;
        }

    }
}
