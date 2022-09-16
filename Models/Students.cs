using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper_Webapi.Models
{
    public class Students
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Roll { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
    }
}