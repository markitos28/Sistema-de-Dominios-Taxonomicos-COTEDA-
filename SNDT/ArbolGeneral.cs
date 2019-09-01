using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNDT
{
    public class ArbolGeneral
    {
        private NodoGeneral raiz;
        public NodoGeneral Raiz
        {
            get { return this.raiz; }
        }

        public ArbolGeneral(NodoGeneral inRaiz)
        {
            this.raiz = inRaiz;
        }


        /// <summary>
        /// Retorna el dato de tipo TipoDominio del NodoGeneral
        /// </summary>
        /// <returns></returns>
        public TipoDominioAbstrac getDatoRaiz()
        {
            return Raiz.Dato;
        }

        public Lista getHijos()
        {
            return Raiz.ListaHijos;

        }

        public void agregarHijos(ArbolGeneral nuevoHijo)
        {
            try
            {
                int ultimaPosicion = getHijos().getTamanio();
                getHijos().agregar(nuevoHijo, ultimaPosicion);
            }
            catch(Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al intentar agregar un hijo nuevo al Arbol");
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }


        /// <summary>
        /// El Metodo elimina un hijo de la Lista actual, sin recursión
        /// </summary>
        /// <param name="hijo_a_Eliminar"></param>
        
        public void eliminarHijo(ArbolGeneral hijo_a_Eliminar)
        {
            try
            {
                Recorredor rec = getHijos().recorrer();
                rec.ObjLista = getHijos();
                rec.comenzar();

                while (!rec.fin())
                {
                    ArbolGeneral unArbol = (ArbolGeneral)rec.elemento();
                    if (unArbol.Equals(hijo_a_Eliminar))
                    {
                        rec.eliminar();
                    }

                    rec.proximo();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al intentar eliminar un hijo del Arbol");
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        public bool esHoja()
        {

            if (getHijos().esVacia())
            {
                return true;
            }

            else
            {
                return false;
            }

        }



        
    }
}
