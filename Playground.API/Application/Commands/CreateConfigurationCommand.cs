namespace Playground.API.Application.Commands
{
    public class CreateConfigurationCommand
    {
        public int Retries { get; set; }
        public int SleepInMillsBetweenRetry { get; set; }
    }
}