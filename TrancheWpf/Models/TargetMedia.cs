using System;

namespace TrancheWpf.Models
{
    public class TargetMedia
    {
        public int target_id { get; set; }
        public string alias { get; set; }
        public string content { get; set; }
        public int direction { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
    }
}
