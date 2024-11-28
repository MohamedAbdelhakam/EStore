using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Services.SharedResponces
{
    public class ServiceResponce
    {
        public string Messege { get; set; }
        public bool IsSucceed { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public Dictionary<string, object> Values { get; set; } = new Dictionary<string, object>();
    }
}
