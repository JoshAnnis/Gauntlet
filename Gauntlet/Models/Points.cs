using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gauntlet.Models
{
    public class Points
    {
        [Key]
        public int Id { get; set; }
        public int points { get; set; }
    }
}