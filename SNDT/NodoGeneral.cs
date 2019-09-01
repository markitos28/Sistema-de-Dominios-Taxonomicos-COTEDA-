using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNDT
{
    public class NodoGeneral
    {
        private TipoDominioAbstrac dato;
        private Lista listaHijos;
        public TipoDominioAbstrac Dato
        {
            get { return this.dato; }
            set { this.dato = value; }
        }
        public Lista ListaHijos
        {
            get { return this.listaHijos; }
            set { this.listaHijos = value; }
        }

        public NodoGeneral(TipoDominioAbstrac inDato, Lista inHijos)
        {
            this.dato = inDato;
            this.listaHijos = inHijos;
        }
        public NodoGeneral (DatosDeEspecie nueva_especie)
        {
            this.dato = nueva_especie;
        }


        
    }
}
