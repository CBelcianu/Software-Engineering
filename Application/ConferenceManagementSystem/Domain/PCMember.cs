using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceManagementSystem.Entities
{
    public class PCMember : User
    {
        public string Affiliation { get; set; }
        public string website { get; set; }
        public bool isReviewer { get; set; }
        
    }
}
