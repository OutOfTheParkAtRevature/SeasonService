using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class League
    {
        [Key]
        [DisplayName("League ID")]
        public Guid LeagueID { get; set; }
        [DisplayName("League Name")]
        public string LeagueName { get; set; }
        public int SportID { get; set; }
    }
}
