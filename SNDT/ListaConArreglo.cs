using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNDT
{
    class ListaConArreglo : Lista
    {
        private int inicial;
        private List<object> datos = new List<object>();

        public List<object> Datos
        {
            get { return this.datos; }
            set { this.datos = value; }
        }

        public ListaConArreglo()
        {

        }
        public override object elemento(int pos)
        {
            try
            {
                if (this.datos.Count > pos)
                {
                    return this.datos[pos];
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al devolver el elemento actual de la Lista con Arreglo.");
                Console.WriteLine(e.Message);
                Console.ReadKey();
                return null;
            }
        }

        public override void agregar(object elem, int pos)
        {
            try
            {

                if (pos == this.datos.Count)
                {
                    this.datos.Add(elem);
                    this.Tamanio += 1;
                }

                else
                {
                    this.datos[pos] = elem;
                    this.Tamanio += 1;
                }


            }

            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("Ha ocurrido un error al intentar agregar un elemento a la Lista con Arreglo");
                Console.ReadKey();
            }
        }

        public override void eliminar(int pos)
        {
            try
            {
                this.datos.RemoveAt(pos);
                this.Tamanio -= 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("No se pudo eliminar el objeto de la lista con arreglo.");
                Console.ReadKey();
            }

        }

        public override bool esVacia()
        {
            if (this.datos.Count == 0) { return true; }
            else { return false; }
        }

        public override bool incluye(object elem)
        {
            for (int i = 0; i <= this.datos.Count; i++)
            {
                if (this.datos[i] == elem) { return true; }
            }
            return false;
        }

        public override Recorredor recorrer()
        {
            return new Recorredor();

        }
    }
}