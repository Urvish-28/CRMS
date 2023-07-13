using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.Core.ViewModels
{
    public class HomeViewModel
    {
        public List<ChartViewModel> ChartData { get; set; }
        public ChartViewModel[] DataArray { get; set; }
        public List<ChartViewModel> BarChartData { get; set; }
        public ChartViewModel[] BarArray { get; set; }
    }
}
