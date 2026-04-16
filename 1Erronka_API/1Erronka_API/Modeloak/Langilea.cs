using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1Erronka_API.Domain
{
    public class Langilea
    {
        public virtual int Id { get; set; }
        public virtual string Izena { get; set; }
        public virtual string Abizena { get; set; }
        public virtual string Erabiltzaile_izena { get; set; }
        public virtual int Langile_kodea { get; set; }
        public virtual string Pasahitza { get; set; }
        public virtual int RolaId { get; set; }
        public virtual bool Ezabatua { get; set; }
        public virtual bool Chat { get; set; }
    }
}
