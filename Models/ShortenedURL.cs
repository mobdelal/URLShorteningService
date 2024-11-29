using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ShortenedURL
    {
        public int Id { get; set; }
        public string LongUrl { get; set; } = string.Empty;
        public string ShortdUrl { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;   

        public DateTime CreatedOn { get; set; }

    }
}
