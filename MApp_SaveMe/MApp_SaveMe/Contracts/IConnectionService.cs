using Plugin.Connectivity.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MApp_SaveMe.Contracts
{
    public interface IConnectionService
    {
        bool IsConnected { get; }
        event ConnectivityChangedEventHandler ConnectivityChanged;
    }
}
