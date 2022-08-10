using Back_End_Layihe.AppCode.InfraStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomLayihe.Models.Entities
{
    public class Lessons : BaseEntity
    {
        public int FennId { get; set; }
        public int MuellimId { get; set; }
        public int GroupId { get; set; }
        public DateTime BashlamaVaxti { get; set; }
        public DateTime BitmeVaxti { get; set; }
    }
}
