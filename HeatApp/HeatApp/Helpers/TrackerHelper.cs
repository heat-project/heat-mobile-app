using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace HeatApp.Helpers
{
    public class TrackerHelper
    {
        public HubConnection Connection { get; private set; }

        public TrackerHelper()
        {
            Connection = new HubConnectionBuilder()
                .WithUrl($"{Constants.UrlAPI}tracker", options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(Settings.Token);
                })
                .WithAutomaticReconnect()
                .Build();
        }

        public async Task Connect()
        {
            if (Connection.State == HubConnectionState.Connected)
                return;

            await Connection.StartAsync();
        }

        public async Task Disconnect()
        {
            await Connection.StopAsync();
        }
    }
}
