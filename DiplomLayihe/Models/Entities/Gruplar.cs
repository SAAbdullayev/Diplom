using Back_End_Layihe.AppCode.InfraStructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomLayihe.Models.Entities
{
    public class Gruplar : BaseEntity
    {
        public string Name { get; set; }
        

        public int ParentIxtisasId { get; set; }


    }
}
