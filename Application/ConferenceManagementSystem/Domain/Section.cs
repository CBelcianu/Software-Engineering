using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceManagementSystem.Domain
{
    public class Section
    {
        public int ID { get; set; }
        public string SectionName { get; set; }
        public string RoomName { get; set; }
        public string PaperDeadline { get; set; }
        public int ChairID { get; set; }
        public int ConferenceID { get; set; }
    }
}
