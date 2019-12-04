using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Playground.Domain.Constants;

namespace Playground.Domain.Models.HealthChecks
{
    public class UrlPingHealthCheck : HealthCheckAbstract
    {
        public UrlPingHealthCheck(string name, string path, int[] validResponses, Dictionary<string, string> headers) : base(Constants.HealthChecks.UrlPing, name)
        {
            Path = path;
            ValidResponses = validResponses;
            Headers = headers;
        }
        
        private string Path { get; set; }
        private int[] ValidResponses { get; set; }
        private Dictionary<string, string> Headers { get; set; }
        
        protected override Notification<bool> Execute()
        {
            var notification = new Notification<bool>();
            using (var client = new HttpClient())
            {
                try
                {
                    var message = new HttpRequestMessage(HttpMethod.Get, Path);
                    if (Headers != null && Headers.Any())
                    {
                        foreach (var header in Headers)
                        {
                            message.Headers.Add(header.Key, header.Value);
                        }
                    }
                    
                    var response = client.SendAsync(message).Result;
                    if (ValidResponses.Contains((int) response.StatusCode))
                    {
                        notification.Value = true;
                    }
                    else
                    {
                        notification.Value = false;
                        notification.AddError(string.Format(ExceptionMessage.NotCorrectResult, (int)response.StatusCode));
                    }
                }
                catch (Exception e)
                {
                    notification.AddError(e);
                    notification.Value = false;
                }
            }

            return notification;
        }
    }
}