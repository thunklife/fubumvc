using System;
using System.Linq;
using System.Collections.Generic;

namespace FubuMVC.Core.Registration.Conventions
{
    public class AttachOutputPolicy : IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            graph.Behaviors.Where(x => x.HasOutput()).Each(x => x.AddToEnd(x.Output));
        }
    }
}