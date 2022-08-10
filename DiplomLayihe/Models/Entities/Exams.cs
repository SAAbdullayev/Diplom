using Back_End_Layihe.AppCode.InfraStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomLayihe.Models.Entities
{
    public class Exams : BaseEntity
    {
        public int GroupId { get; set; }
        public int FennId { get; set; }
        public DateTime BashlamaVaxti { get; set; }
        public DateTime BitmeVaxti { get; set; }
    }
}
