using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultAdmin.Entities
{
    public class News
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string NewsBody { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime ValidToDate { get; set; }
        public int PublishedBy { get; set; }
        public string NewsImgUrl { get; set; }
        public string ShortNewsBody { get; set; }
    }
}
