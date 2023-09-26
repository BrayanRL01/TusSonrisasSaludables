using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public partial class UsersTrigger
    {
        public int TriggerId { get; set; }
        public string UserData { get; set; } = null!;
        public string TriggerReason { get; set; } = null!;
        public DateTime TriggerDate { get; set; }
    }
}
