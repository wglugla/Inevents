using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inevent.Models
{
    public class Event
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Place { get; set; }
        public string Description { get; set; }
        public string[] Tags { get; set; }
        public string FormatedDate { get; set; }
        public string FormatedDay { get; set; }
        public string FormatedDayName { get; set; }
    }
}
