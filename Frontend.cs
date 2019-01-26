using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProgressAPI.Models;

namespace ProgressAPI.Frontend
{
    public static class Frontend
    {
        [FunctionName("GetProgress")]
        public static async Task<IActionResult> GetProgress(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "progress")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var mediaId = req.Query["id"];

            if (string.IsNullOrWhiteSpace(mediaId)) {
                return new OkObjectResult("Looking up all media progress");
            } else {
                return new OkObjectResult("Looking up individual media progress");
            }
        }

        [FunctionName("UpdateProgress")]
        public static async Task<IActionResult> UpdateProgress(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "progress")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<ProgressUpdateRequest>(requestBody);



                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

    }
}
