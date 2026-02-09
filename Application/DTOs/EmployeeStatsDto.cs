using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class EmployeeStatsDto
    {
        public int RecentHires { get; set; }
        public TrendDto? Trend { get; set; }
    }

    public class TrendDto
    {
        public decimal Value { get; set; }
        public bool IsPositive { get; set; }
    }
}
