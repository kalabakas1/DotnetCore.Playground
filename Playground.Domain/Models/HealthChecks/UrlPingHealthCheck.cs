using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Playground.Domain.Constants;

namespace Playground.Domain.Models.HealthChecks
{
    public class UrlPingHealthCheck : HealthCheckAbstract
    {
        public UrlPingHealthCheck() : base(Enums.HealthChecks.UrlPing)
        {
            
        }
        
        public UrlPingHealthCheck(string name, string path, int[] validResponses, Dictionary<string, string> headers) : base(Enums.HealthChecks.UrlPing, name)
        {
            Path = path;
            ValidResponses = validResponses ?? new int[0];
            Headers = headers ?? new Dictionary<string, string>();
        }
        
        public string Path { get; set; }
        public int[] ValidResponses { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        
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