using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DataTransfer
{
    public class CreateGameDto
    {
        [DisplayName("Game Date")]
        [DataType(DataType.DateTime)]
        public DateTime GameDate { get; set; }
        [DisplayName("Home Team ID")]
        public Guid HomeTeamID { get; set; }
        [DisplayName("Away Team ID")]
        public Guid AwayTeamID { get; set; }        
    }
}
