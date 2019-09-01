using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNDT
{
    public class DatosDeEspecie: TipoDominioAbstrac
    {
        private string nombre;
        private string metabolismo;
        private string reproduccion;

        public override string Nombre
        {
            get { return this.nombre; }
            set { this.nombre = value; }
        }
        public string Metabolismo
        {
            get { return this.metabolismo; }
            set {this.metabolismo= value; }
        }
        public string Reproduccion
        {
            get {return this.reproduccion; }
            set { this.reproduccion = value; }
        }

        /// <summary>
        /// Se utiliza para setear las variables metabolismo y reproducccion de la clase DatosDeEspecie.
        /// Datos de entrada: 
        /// param1 : (1) Anabolico ... (2) Catabolico
        /// param2 : (1) Sexual    ... (2) Asexual
        /// param3 : Nombre 
        /// </summary>
        /// <param name="met"></param>
        /// <param name="rep"></param>
        public DatosDeEspecie(int met, int reprod, string in_nombre)
        {
            this.nombre = in_nombre;

            if (met.Equals(1)) {this.metabolismo = "Anabolico";}

            else { if (met.Equals(2)) {this.metabolismo= "Catabolico";} }

            if (reprod.Equals(1)) {this.reproduccion= "Sexual";}

            else { if (reprod.Equals(2)) { this.reproduccion= "Asexual"; } }
        }
        public DatosDeEspecie() { }

    }
}
