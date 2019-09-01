using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNDT
{
    public class Recorredor: RecorredorAbst
    {
        private Lista objLista;
        private  int actual;
        
        
        public Lista ObjLista
        {
            get { return this.objLista; }
            set { this.objLista = value; }
        }

        public Recorredor() { }
       
        public override void comenzar()
        {
            try
            {
                actual = 0;
            }
            catch(Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al comenzar a recorrer la lista del arbol");
                Console.ReadKey();
            }
        }

        public override object elemento()
        {
            try
            {
                return objLista.elemento(actual);
            }
            catch(Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al intentar devolver el elemento de la lista");
                Console.WriteLine(e.Message);
                Console.ReadKey();
                return null;
            }
        }

        public override void proximo()
        {
            try
            {
                actual += 1;
            }
            catch(Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al intentar pasar al proximo elemento. (Incremento del puntero)");
                Console.ReadKey();
            }
        }

        public override bool fin()
        {

            try
            {
                if (objLista.getTamanio() - 1 < actual)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Ha ocurrido un error en el fin de la lista");
                Console.WriteLine(e.Message);
                Console.ReadKey();
                return true;
            }
            
        }

        public override void agregar(object elem)
        {
            try
            {
                objLista.agregar(elem, actual);
            }
            catch(Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al intentar agregar un elemento a la lista");
                Console.ReadKey();
            }
        }

        public override void eliminar()
        {
            try
            {
                objLista.eliminar(actual);
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al eliminar un objeto de la lista");
                Console.WriteLine(e.Message);
                Console.ReadKey();

            }
        }

        public int getPosicion()
        {
            return actual;
        }
    }
}