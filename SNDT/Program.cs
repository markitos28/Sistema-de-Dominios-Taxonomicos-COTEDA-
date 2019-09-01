using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SNDT
{
    class Program
    {
        static void Main(string[] args)
        {
            TipoDominio a = new TipoDominio("Seres Vivos");

            ArbolGeneral dominios = new ArbolGeneral(new NodoGeneral(a, new ListaConArreglo()));

            
            
            string[] dominio_separado = new string[] { "a", "b", "c", "d", "e", "f", "g"};
            ModuloAdministracion.agregarNodo(dominios, 0, dominio_separado, 1, 2);

            dominio_separado = new string[]{ "a", "b", "c", "d", "e", "f", "t"};
            ModuloAdministracion.agregarNodo(dominios, 0, dominio_separado, 2, 2);

            dominio_separado = new string[] { "a", "b", "c", "d", "q", "w", "e" };
            ModuloAdministracion.agregarNodo(dominios, 0, dominio_separado, 2, 1);

            dominio_separado = new string[] { "a", "b", "c", "d", "e", "j", "l" };
            ModuloAdministracion.agregarNodo(dominios, 0, dominio_separado, 1, 1);

            dominio_separado = new string[] { "a", "z", "x", "v", "n", "m", "h" };
            ModuloAdministracion.agregarNodo(dominios, 0, dominio_separado, 2, 2);

            dominio_separado = new string[] { "p", "o", "ct", "dx", "eq", "fe", "gr" };
            ModuloAdministracion.agregarNodo(dominios, 0, dominio_separado, 1, 2);

            bool flagSalir=false;
            do
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Bienvenido al Sistema de Nombres de Dominio Taxonomico");
                    Console.WriteLine(" ");
                    Console.WriteLine("Elija la opcion a la que desee ingresar: ");
                    Console.WriteLine(" ");
                    Console.WriteLine("1). Modulo Administración.");
                    Console.WriteLine("2). Modulo Consultas.");
                    Console.WriteLine("3). Salir de la Aplicación.");
                    Console.WriteLine(" ");
                    Console.Write("Opcion ==> "); int eleccion = Convert.ToInt32(Console.ReadLine());

                    switch(eleccion)
                    {
                        case 1:
                            //Modulos.modAdministracion(dominios);
                            ModuloAdministracion.Menu(dominios);
                            break;
                        case 2:
                            //Modulos.modConsultas(dominios);
                            ModuloConsultas.Menu(dominios);
                            break;
                        case 3:
                            Console.WriteLine("Hasta Luego! . . .");
                            flagSalir = true;
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("La opcion ingresada es incorrecta. . .\nPor favor, vuelva a intentarlo");
                            flagSalir = false;
                            Console.ReadKey();
                            break;
                    }

                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Ha ocurrido un error inesperado. . . \nPor favor, vuelva a intentarlo");
                    Console.ReadKey();
                }
            } while (!flagSalir);
         }



    }
}
