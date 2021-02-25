using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataTransfer
{
    public class GameDto
    {
        public Guid GameID { get; set; }

        public Guid SeasonID { get; set; }

        public Guid HomeTeamID { get; set; }

        public Guid AwayTeamID { get; set; }

        public DateTime GameDate { get; set; }

        public Guid WinningTeamID { get; set; }

        public int HomeScore { get; set; }

        public int AwayScore { get; set; }

        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public Team WinningTeam { get; set; }
    }
}
