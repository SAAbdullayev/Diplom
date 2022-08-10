using Back_End_Layihe.AppCode.InfraStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomLayihe.Models.Entities
{
    public class Qiymetler : BaseEntity
    {
        public int Qiymet { get; set; }
        public int GroupId { get; set; }
        public int TelebeId { get; set; }
        public int LessonId { get; set; }
        public int TeacherId { get; set; }
    }
}
