using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceManagementSystem.Domain
{
    public class Paper
    {
        public int ID { get; set; }
        public string ContentLoc { get; set; }
        public string AbstractLoc { get; set; }
        public string Topic { get; set; }
        public string PaperName { get; set; }
        public int isAccepted { get; set; }
        public int SectionID { get; set; }
    }
}
