using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PlayerGame
    {
        [Key]
        [DisplayName("User ID")]
        public Guid UserID { get; set; }
        [DisplayName("Game ID")]
        public Guid GameID { get; set; }
        public Guid StatLineID { get; set; }
    }
}
