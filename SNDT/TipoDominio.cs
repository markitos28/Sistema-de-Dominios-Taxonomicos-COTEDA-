using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNDT
{
    public class TipoDominio: TipoDominioAbstrac
    {
        private string nombre;

        public TipoDominio(string in_nombre)
        {
            Nombre = in_nombre;
        }

        public override string Nombre
        {
            get { return this.nombre; }
            set {this.nombre= value; }
        }

    }
}
