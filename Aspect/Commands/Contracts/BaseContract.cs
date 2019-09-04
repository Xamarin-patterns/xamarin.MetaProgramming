using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using PostSharp.Aspects;

namespace Xamarin.MetaProgramming.Commands.Contracts
{
    [Serializable]
    public abstract class BaseContract:LocationInterceptionAspect
    {
        public override void OnSetValue(LocationInterceptionArgs args)
        {
           ( args.Value as IMPCommand).AddContract(this);
           SetupSatisfactionChanged();
           args.ProceedSetValue();
        }

        public abstract bool IsSatisfied(object parameter);
        public abstract void SetupSatisfactionChanged();


        protected void RaiseSatisfactionChanged()
        {
            SatisfactionChanged?.Invoke(this,EventArgs.Empty);
        }

        internal event EventHandler SatisfactionChanged;
    }
    [Serializable]
    public class WifiContract : BaseContract
    {
        public override void SetupSatisfactionChanged()
        {
            CrossConnectivity.Current.ConnectivityTypeChanged += (s, e) =>
            {
                base.RaiseSatisfactionChanged();
            };
        }
        public override bool IsSatisfied(object parameter)
        {
            return CrossConnectivity.Current.ConnectionTypes.Any(connectionType => connectionType == ConnectionType.WiFi);
        }
    }
}
