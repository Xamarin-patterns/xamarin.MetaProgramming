using System;

namespace Xamarin.MetaProgramming.Commands
{
    public class CommandExecuteChangedArgs : EventArgs
    {
        public CommandExecuteChangedArgs(string reason)
        {
            Reason = reason;
        }

        public string Reason { get; }
    }
}