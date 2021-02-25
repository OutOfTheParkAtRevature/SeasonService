using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    public class Team
    {
        [Key]
        [DisplayName("Team ID")]
        public Guid TeamID { get; set; }
        [DisplayName("Team Name")]
        public string Name { get; set; }
        [DisplayName("Wins")]
        public int Wins { get; set; } = 0;
        [DisplayName("Losses")]
        public int Losses { get; set; } = 0;
        [DisplayName("Carpool ID")]
        [ForeignKey("RecipientListID")]
        public Guid CarpoolID { get; set; }
        [ForeignKey("LeagueID")]
        public Guid LeagueID { get; set; }
        [ForeignKey("StatLineID")]
        public Guid? StatLineID { get; set; } = null;
    }
}
