using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomLayihe.Models.Entities.Membership
{
    public class DiplomUser : IdentityUser<int>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string ProfileImg { get; set; }


        //telebeler ucun
        public int IxtisasId { get; set; }
        public int GrupId { get; set; }


        //muellimler ucun
        public int TedrisFenniId { get; set; }
    }
}
