using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceManagementSystem.Domain
{
    public class Conference
    {
        public int ID { get; set; }
        public string ConferenceName { get; set; }
        public string ConferenceAddress { get; set; }
        public string ConferenceDate { get; set; } 

    }
}
