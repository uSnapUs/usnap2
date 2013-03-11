using System;

namespace uSnapUs.Core.Model
{
    public class CurrentEvent
    {

        public DateTime SavedAt { get; set; }
        
        public int EventId { get; set; }
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
    }
}
