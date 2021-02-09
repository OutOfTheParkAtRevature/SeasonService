using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Game
    {
        [Key]
        [DisplayName("Game ID")]
        public Guid GameID { get; set; }
        [DisplayName("Season ID")]
        public Guid SeasonID { get; set; }
        [DisplayName("Home Team ID")]
        public Guid HomeTeamID { get; set; }
        [DisplayName("Away Team ID")]
        public Guid AwayTeamID { get; set; }
        [DisplayName("Game Date")]
        public DateTime GameDate { get; set; }
        [DisplayName("Winning Team")]
        public Guid WinningTeam { get; set; }
        [DisplayName("Home Score")]
        public int HomeScore { get; set; }
        [DisplayName("Away Score")]
        public int AwayScore { get; set; }
        public Guid HomeStatID { get; set; }
        public Guid AwayStatID { get; set; }
    }
}
