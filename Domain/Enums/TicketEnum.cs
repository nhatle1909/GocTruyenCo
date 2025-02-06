using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{  
       public enum TicketStatus
       {
                Open = 1,
                InProgress = 2,
                Done = 3,
                Closed = 4
       }
       public enum TicketType
       {
         Role = 1,
         Report = 2
       }
    
}
