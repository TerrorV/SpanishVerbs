using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpanishVerb.DAL.Abstract
{
    public class Note
    {
        public string Front { get; set; }

        public string Back { get; set; }

        public string Details { get; set; }

        public long Id { get; set; }
    }
}
