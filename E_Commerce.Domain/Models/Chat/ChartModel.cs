using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Models.Chat
{
    public class ChartModel
    {

        public List<int> Data { get; set; }
        public string? Label { get; set; }
        public string? BackgroundColor { get; set; }
        public ChartModel()
        {
            Data = new List<int>();
        }

    }
}
