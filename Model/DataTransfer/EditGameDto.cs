using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DataTransfer
{
    public class EditGameDto
    {
        public DateTime? GameDate { get; set; }
        [DisplayName("Winning Team ID")]
        public Guid? WinningTeamID { get; set; }
        [DisplayName("Home Score")]
        public int? HomeScore { get; set; }
        [DisplayName("Away Score")]
        public int? AwayScore { get; set; }
        [DisplayName("Home Stats")]
        public Guid? HomeStatID { get; set; }
        [DisplayName("Away Stats")]
        public Guid? AwayStatID { get; set; }
    }
}
