using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gauntlet.Models
{
    public class Video
    {
        [Key]
        public int id { get; set; }

        public string FromUserId { get; set; }

        public byte[] UserVideo { get; set; }
        

    }
}