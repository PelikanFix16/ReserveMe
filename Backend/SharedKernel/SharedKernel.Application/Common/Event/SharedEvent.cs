using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedKernel.Application.Common.Event
{
    public class SharedEvent
    {
        public string EventName { get; set; } = null!;
        public string EventData { get; set; } = null!;
        public string AssemblyName { get; set; } = null!;
    }
}
