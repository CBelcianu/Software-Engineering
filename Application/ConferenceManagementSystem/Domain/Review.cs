using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceManagementSystem.Domain
{
    public class Review
    {
        public int PaperId { get; set; }
        public int ReviewerId { get; set; }
        public string Qualifier { get; set; }
        public string Comments { get; set; }

        public int ReevalRequest { get; set; }
    }
}
