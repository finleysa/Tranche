using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrancheWpf.Models
{
    public class TCom
    {
        public Int16 id_source_com_info;
        public Int16 id_appli;
        public Int16 num_com;
        public string start_time;
        public string end_time;
        public Int16 end_reason;
        public bool is_source;
        //public string gps_localization;
        public bool is_running;
        public Int16 timestamp;
        public bool is_emergency;
        public bool is_ccbs;
        public bool is_exported;
        public Int16 switch_type;
        public Int16 end_info;
        public Int16 power;
        public bool has_ciphering;
        public string networkshortname;
        public string networkfullname;
        public Int16 import_status;
        public Int16 exploitation_state;
        public bool to_be_deleted;

    }
}
