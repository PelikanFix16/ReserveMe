using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedKernel.InterfaceAdapters.Dto
{
    public class StoreEvent
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public string EventName { get; set; } = null!;
        public string EventData { get; set; } = null!;
        public DateTimeOffset EventDate { get; set; }
        public int Version { get; set; }
        public string AssemblyName { get; set; } = null!;
    }
}
