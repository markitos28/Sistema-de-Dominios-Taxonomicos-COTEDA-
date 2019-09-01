using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNDT
{
    class ModuloConsultas: IModulos
    {
        
        public static void Menu(ArbolGeneral SeresVivos)
        {
                bool flagSalir = false;
                do
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("Modulo Consultas");
                        Console.WriteLine("");
                        Console.WriteLine("1). Imprimir el Arbol Taxonomico Completo");
                        Console.WriteLine("2). Imprimir Metabolismo y Reproduccion de una especie dada");
                        Console.WriteLine("3). Imprimir todas las Especies correspondientes a una Clase");
                        Console.WriteLine("4). Imprimir categorías Taxonómicas a una determinada profundidad");
                        Console.WriteLine("5). Salir");
                        Console.WriteLine("");
                        Console.Write("Opcion ==> "); int eleccion = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("");

                        switch (eleccion)
                        {

                            case 1:
                                imprimirArbolEntero(SeresVivos);
                                flagSalir = true;
                                Console.ReadKey();
                                break;
                            case 2:
                                imprimirTaxonomiaSegunDominio(SeresVivos);
                                flagSalir = true;
                                Console.ReadKey();
                                break;
                            case 3:
                                imprimirEspecieSegunClase(SeresVivos);
                                flagSalir = true;
                                Console.ReadKey();
                                break;
                            case 4:
                                imprimirCategoriaAProfundidad(SeresVivos);
                                flagSalir = true;
                                Console.ReadKey();
                                break;
                            case 5:
                                flagSalir = true;
                                Console.WriteLine("Ha salido del Modulo Consultas . . .");
                                Console.ReadKey();
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("Ha ingresado una opcion invalida.\nPor favor, vuelva a ingresar la opcion.");
                                flagSalir = false;
                                Console.ReadKey();
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.Clear();
                        Console.WriteLine("Ha ocurrido un error inesperado. . .\nPor favor, vuelva a intentarlo.");
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                    }
                } while (!flagSalir);
        }

        public static void imprimirArbolEntero(ArbolGeneral dominios)
        {
            try
            {
                Recorredor rec = new Recorredor(); rec.ObjLista=dominios.getHijos();
                rec.comenzar();
                while (!rec.fin())
                {
                    ArbolGeneral unArbol = (ArbolGeneral)rec.elemento();
                    pre_orden(unArbol);
                    rec.proximo();
                }
            }
            catch (Exception e)
            {
                //Console.Clear();
                Console.WriteLine("Ha ocurrido un error al listar el Arbol de dominios Taxonomicos. Favor de comunicarse con el señor informatico.");
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        private static void pre_orden(ArbolGeneral arbol)
        {
            try
            {

                if (arbol.esHoja())
                {

                    if (arbol.getDatoRaiz().Nombre.ToLower().Equals("Seres Vivos".ToLower()))
                    {
                        Console.WriteLine(arbol.getDatoRaiz().Nombre);
                    }
                    else
                    {
                        DatosDeEspecie dato = (DatosDeEspecie)arbol.getDatoRaiz();
                        Console.WriteLine(" ");
                        Console.Write("Especie ==> {0} \n\t\tMetabolismo: {1}\n\t\tReproduccion: {2}", dato.Nombre, dato.Metabolismo, dato.Reproduccion);
                    }
                }

                else
                {
                    Console.Write(arbol.getDatoRaiz().Nombre + ".");
                    Lista hijosArbol = arbol.getHijos();
                    Recorredor rec = new Recorredor();
                    rec.ObjLista=hijosArbol;
                    rec.comenzar();
                    while (!rec.fin())
                    {
                        ArbolGeneral unHijo = (ArbolGeneral)rec.elemento();
                        pre_orden(unHijo);

                        rec.proximo();
                    }
                    Console.WriteLine(" ");


                }
            }
            catch (Exception e)
            {
                //Console.Clear();
                Console.WriteLine("Ha ocurrido un error en el metodo pre-orden. Comuniquese con el señor informatico.");
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        //funcion para validar que sea metabolismo y reproduccion validos
        private static bool validacionMetRepro(int numero)
        {
            if (numero.Equals(1) || numero.Equals(2)) { return true; }
            else { return false; }

        }

        private static void imprimirTaxonomiaSegunDominio(ArbolGeneral dominios)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("                    Imprimir Metabolismo y Reproduccion de una especie dada \n");
                Console.WriteLine("Por favor, ingrese el dominio Taxonómico completo de la especie de la cual requiera saber su Metabolismo y Reproducción Sexual . . .");
                Console.WriteLine("La estructura correcta del dominio Taxonómico es Reino.Filo.Clase.Orden.Familia.Genero.Especie \n");
            ReingresodeDominio:
                Console.WriteLine("Dominio Taxonómmico: "); string inDominio = Console.ReadLine();
                Console.WriteLine(" ");
                string[] dominioSeparado = inDominio.Split('.');
                int nivel = 0;
                DatosDeEspecie especieDelUsuario = new DatosDeEspecie();
                if (dominioSeparado.Length.Equals(7))
                {
                    especieDelUsuario = buscarEspecieEnArbol(dominios, dominioSeparado, nivel, especieDelUsuario);
                    Console.WriteLine("Para la especie {0}: \nSu reproduccion es: {1}\nSu Metabolismo es: {2}",
                                       especieDelUsuario.Nombre, especieDelUsuario.Reproduccion, especieDelUsuario.Metabolismo);
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Estimado. El dominio ingresado no cumple con la cantidad de categorias necesarias para su busqueda.\nPor favor, vuelva a ingresar el dominio Taxonómico: ");
                    goto ReingresodeDominio;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al buscar la rama taxonómica ingresada");
                Console.WriteLine(e.Message);
            }
        }

        private static DatosDeEspecie buscarEspecieEnArbol(ArbolGeneral dominios, string[] dominios_separados, int nivel, DatosDeEspecie especieEncontrada)
        {

            Recorredor rec = dominios.getHijos().recorrer();
            rec.ObjLista=dominios.getHijos();
            rec.comenzar();
            ArbolGeneral unArbol = (ArbolGeneral)rec.elemento();

            while (!rec.fin())
            {
                unArbol = (ArbolGeneral)rec.elemento();
                if (unArbol.getDatoRaiz().Nombre.Equals(dominios_separados[nivel]))
                {

                    especieEncontrada = buscarEspecieEnArbol(unArbol, dominios_separados, nivel + 1, especieEncontrada);

                }
                rec.proximo();

            }

            Type tipoDDE = typeof(DatosDeEspecie);
            if (dominios.getDatoRaiz().GetType().Equals(tipoDDE))
            {
                especieEncontrada = (DatosDeEspecie)dominios.getDatoRaiz();
            }
            return especieEncontrada;
        }

        private static void imprimirEspecieSegunClase(ArbolGeneral dominios)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("                    Imprimir todas las Especies correspondientes a una Clase \n");
                Console.WriteLine("Por favor, ingrese el dominio Taxonómico hasta la Clase de la cual requiera saber Orden.Familia.Genero.Especie . . .");
                Console.WriteLine("La estructura correcta del dominio Taxonómico es Reino.Filo.Clase \n");
            ReingresodeDominio:
                Console.WriteLine("Dominio Taxonómmico: "); string inDominio = Console.ReadLine();
                Console.WriteLine(" ");
                string[] dominioSeparado = inDominio.Split('.');
                int nivel = 0;
                // La estructura quedaría: listaDeArboles = [listaDeOrdenes , listaDeOrdenes , listaDeOrdenes , listaDeOrdenes]
                List<List<ArbolGeneral>> listaDeArboles = new List<List<ArbolGeneral>>(); //Lista que contiene lista de arboles
                List<ArbolGeneral> listaDeOrdenes = new List<ArbolGeneral>();             // Lista que contine los dominios a partir de cada orden
                if (dominioSeparado.Length.Equals(3))
                {
                    Console.Clear();
                    Console.WriteLine("Las categorias que porceden del dominio taxonomico {0} son:", inDominio);
                    buscarOrdenesEnArbol(dominios, dominioSeparado, nivel, listaDeArboles, listaDeOrdenes);
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Estimado. El dominio ingresado no cumple con la cantidad de categorias necesarias para su busqueda.\nPor favor, vuelva a ingresar el dominio Taxonómico: ");
                    goto ReingresodeDominio;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al buscar la rama taxonómica ingresada");
                Console.WriteLine(e.Message);
            }
        }

        private static void buscarOrdenesEnArbol(ArbolGeneral dominios, string[] dominioSeparado, int nivel, List<List<ArbolGeneral>> listaDeArboles, List<ArbolGeneral> listaDeOrdenes)
        {
            Recorredor rec = dominios.getHijos().recorrer();
            rec.ObjLista=dominios.getHijos();
            rec.comenzar();
            ArbolGeneral puntero;
            List<ArbolGeneral> aux = listaDeOrdenes;
            if (nivel < 3)
            {
                while (!rec.fin())
                {
                    puntero = (ArbolGeneral)rec.elemento();
                    if (puntero.getDatoRaiz().Nombre.Equals(dominioSeparado[nivel]))
                    {
                        buscarOrdenesEnArbol(puntero, dominioSeparado, nivel + 1, listaDeArboles, aux);
                    }
                    rec.proximo();
                }
            }

            else
            {
                while (!rec.fin())
                {
                    puntero = (ArbolGeneral)rec.elemento();
                    pre_orden(puntero);
                    rec.proximo();
                }
            }
        }

        private static void imprimirCategoriaAProfundidad(ArbolGeneral dominios)
        {
            Console.Clear();
            Console.WriteLine("                    Imprimir categorías Taxonómicas a una determinada profundidad");
            Console.WriteLine("Ingrese la opcion de la profundidad que desea imprimir :");
            Console.WriteLine("1) Reino");
            Console.WriteLine("2) Filo");
            Console.WriteLine("3) Clase");
            Console.WriteLine("4) Orden");
            Console.WriteLine("5) Familia");
            Console.WriteLine("6) Genero");
            Console.WriteLine("7) Especie");
        PedirCategoria:
            Console.WriteLine("Opcion ==> "); int profundidad = Convert.ToInt16(Console.ReadLine());
            int nivel = 0;
            if (profundidad > 7 | profundidad < 1)
            {
                Console.WriteLine("La opcion ingresada es incorrecta, por favor vuelva a ingresar una opcion");
                goto PedirCategoria;
            }
            Console.WriteLine("Los nodos que se encuentran a la profundidad {0} son: ", profundidad);
            buscarCategoria(dominios, profundidad, nivel);
        }

        private static void buscarCategoria(ArbolGeneral dominios, int profundidad, int nivel)
        {
            Recorredor rec = dominios.getHijos().recorrer();
            rec.ObjLista=dominios.getHijos();
            rec.comenzar();
            ArbolGeneral puntero;
            while (!rec.fin())
            {
                puntero = (ArbolGeneral)rec.elemento();
                if (profundidad - 1 == nivel)
                {
                    Console.WriteLine(puntero.getDatoRaiz().Nombre);
                }
                else
                {
                    buscarCategoria(puntero, profundidad, nivel + 1);
                }
                rec.proximo();
            }

        }



    }
}
