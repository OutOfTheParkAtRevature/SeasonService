using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Season
    {
        [Key]
        [DisplayName("Season ID")]
        public Guid SeasonID { get; set; }
        [DisplayName("League ID")]
        public Guid LeagueID { get; set; }
    }
}
