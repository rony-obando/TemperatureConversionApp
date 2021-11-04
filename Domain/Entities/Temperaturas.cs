using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Temperaturas
    {
        public int pos { get; set; }
        public double Dato { get; set; }
        public TipoUnidad tipounidad { get; set; }
    }
    public class OrdenByPosicion : IComparer<Temperaturas>
    {
        public int Compare(Temperaturas x, Temperaturas y)
        {
            if (x.pos > y.pos)
            {
                return 1;
            }
            else if (x.pos < y.pos)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }


    }
}
