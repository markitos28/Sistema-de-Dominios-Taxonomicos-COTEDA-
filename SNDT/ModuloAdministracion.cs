using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SNDT
{
    class ModuloAdministracion: IModulos
    {
        public static bool Menu(ArbolGeneral SeresVivos)
        {
            ReinicioMenu:
            bool flagSalir = false;
            char eleccion;
            do
            {
                try
                {
                    Console.Clear();
                    for (int i = 0; i <= 20; i++) { Console.Write(" "); }
                    Console.Write("Modulo Administración");
                    Console.WriteLine(" ");
                    Console.WriteLine("Elija la opcion que desee con el teclado numerico:");
                    Console.WriteLine("");
                    Console.WriteLine("1). Agregar un nuevo Dominio Taxonómico al Arbol ");
                    Console.WriteLine("2). Eliminar un Dominio Taxonómico del Arbol ");
                    Console.WriteLine("3). Salir del Módulo Administración ");
                    Console.WriteLine("");

                    Console.Write("Opción ==> "); eleccion = Convert.ToChar(Console.ReadLine());


                    switch (eleccion)
                    {
                        case '1':
                            Console.Clear();
                            mod_agregarDominio(SeresVivos);
                            break;
                        case '2':
                            eliminacionTaxonomica(SeresVivos);
                            break;
                        case '3':
                            flagSalir = true;
                            break;
                        default:
                            Console.WriteLine("opcion incorrecta. Ingrese nuevamente.");
                            Console.Clear();
                            goto ReinicioMenu;
                    }
                }
                catch (FormatException e)
                {
                    Console.Clear();
                    Console.WriteLine("No ha ingresado datos en la opción.\nIntentelo nuevamente.");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            while (!flagSalir);
            return flagSalir;

        }

        private static void mod_agregarDominio(ArbolGeneral dominios)
        {   ReinicioModuloAgregar:
            bool flagSalir = false;
            do
            {
                try
                {

                    Console.Clear();
                    bool flag = false;
                    for (int i = 0; i <= 20; i++) { Console.Write(" "); }
                    Console.WriteLine("Agregar un nuevo Dominio Taxonómico al Arbol");
                    Console.WriteLine("Elija la opción deseada: ");
                    Console.WriteLine("1). Ingresar un nombre de dominio taxonómico completo: ");
                    Console.WriteLine("2). Ingresar de a una categoría: ");
                    Console.WriteLine("3). Volver al Módulo Administración: ");
                    Console.WriteLine("");

                    Console.Write("Opción ==> "); char opcion = Convert.ToChar(Console.ReadLine());

                    switch (opcion)
                    {

                        case '1':
                            insertDominioCompleto(dominios);
                            break;

                        case '2':
                            int nivel = 0;
                            bool cateNueva = false;
                            insertCategxCateg(dominios, nivel, cateNueva);
                            break;

                        case '3':
                            flagSalir = true;
                            Console.Clear();
                            break;
                        default:
                            Console.WriteLine("Opcion incorrecta. Ingrese nuevamente.");
                            Console.ReadKey(); ;
                            Console.Clear();
                            goto ReinicioModuloAgregar;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ha ocurrido un error inesperado. . .\nVuelva a intentar.");
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }
            } while (!flagSalir);
        }

        private static void insertDominioCompleto(ArbolGeneral SeresVivos)
        {
            bool flag = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Ingrese el dominio completo separado por puntos (.)");
                Console.WriteLine("El mismo debe quedar de la siguiente manera: Reino.Filo.Clase.Orden.Familia.Genero.Especie ");
                Console.WriteLine(" ");
                Console.Write("Dominio ==> "); string inDominio = Console.ReadLine();
                Console.WriteLine(" ");
                string[] dominioSeparado = inDominio.Split('.');
                if (dominioSeparado.Length.Equals(7))
                {
                    Console.WriteLine("Por favor, ingrese su tipo de Metabolismo: \n1). Anabolico \n2). Catabolico");
                    Console.WriteLine(" ");
                    Console.Write("Metabolismo ==> ");
                    int metabolismo = Convert.ToInt32(Console.ReadLine());
                    while (validacionMetRepro(metabolismo).Equals(false))
                    { Console.WriteLine("Opcion incorrecta, vuelva a ingresar el número: "); metabolismo = Convert.ToInt32(Console.ReadLine()); }
                    Console.WriteLine(" ");
                    Console.WriteLine("Por favor, ingrese su tipo de Reproduccion: \n1). Sexual \n2). Asexual");
                    Console.WriteLine(" ");
                    Console.Write("Reproduccion ==> "); int reproduccion = Convert.ToInt32(Console.ReadLine());
                    while (validacionMetRepro(reproduccion).Equals(false))
                    { Console.WriteLine("Opcion incorrecta, vuelva a ingresar el número: "); reproduccion = Convert.ToInt32(Console.ReadLine()); }


                    //Lista de hijos de 'dominios'
                    Lista hijosDelArbol = SeresVivos.getHijos();
                    int categ = 0;

                    //Dar de alta un hijo si el arbol esta vacio:

                    try
                    {
                        agregarNodo(SeresVivos, categ, dominioSeparado, metabolismo, reproduccion);


                        Console.WriteLine("El Dominio ingresado, fue dado de alta satisfactoriamente.");
                        Console.ReadKey();
                        Console.Clear();
                        //seteamos el flag para que salga del do-while luego de darlo de alta
                        flag = true;
                    }
                    catch
                    {
                        Console.Clear();
                        Console.WriteLine("Hubo un error y no se pudo dar de alta su dominio en el Arbol.");
                        Console.WriteLine("Intente Nuevamente . . .");
                        Console.ReadKey();
                        flag = false;
                    }


                    // si no esta vacio, verificar que cada categoria no exista y de ser asi, agregarla, sino ignorarla
                }

                else
                {
                    Console.Clear();
                    Console.WriteLine(inDominio);
                    Console.WriteLine("El dominio ingresado no es correcto.\nIntente nuevamente . . .");
                    Console.ReadKey();
                }


            } while (!flag);
            
        }
        private static void insertCategxCateg(ArbolGeneral dominios, int nivel, bool cateNueva)
        {
        PedirOtraVez:
            try
            {
                if (nivel <= 6)
                {
                    int numDeOpcion = 1;
                    int eleccion = 1;
                    Recorredor rec = dominios.getHijos().recorrer();
                    Console.Clear();

                    if (cateNueva.Equals(false))
                    {
                        Console.WriteLine("Elija una categoría de las siguientes opciones o ingrese una nueva:");
                        Console.WriteLine(" ");

                        //Inicia una instancia de recorredor, setea el atributo 'lista' del recorredor 
                        //y luego recorre esa lista que se le pasa por parametros a 'setLista'
                        rec.ObjLista=dominios.getHijos();
                        rec.comenzar();
                        while (!rec.fin())
                        {
                            ArbolGeneral unArbol = (ArbolGeneral)rec.elemento();
                            string datoRaiz = unArbol.getDatoRaiz().Nombre;
                            Console.WriteLine("{0}). {1}", numDeOpcion, datoRaiz);
                            numDeOpcion++;
                            rec.proximo();
                        }

                        Console.WriteLine("{0}). Ingresar una Categoria nueva ", numDeOpcion);
                        Console.WriteLine(" ");
                        Console.Write("Opcion ==> "); eleccion = Convert.ToInt32(Console.ReadLine());
                    }
                    int error = dominios.getHijos().getTamanio() + 2;

                    //si la opcion elegida por el usuario es mayor que la longitud de la lista de hijos,
                    //quiere decir que se elige agregar un nodo nuevo 
                    if (eleccion > dominios.getHijos().getTamanio() && eleccion < error)
                    {
                        TipoDominioAbstrac dato;
                        if (nivel.Equals(6))
                        {
                            Console.WriteLine(" ");
                            Console.Write("Ingrese nombre de la Especie: "); string especie = Console.ReadLine();
                            Console.WriteLine("Por favor, ingrese su tipo de Metabolismo: \n1). Anabolico \n2). Catabolico");
                            Console.WriteLine(" ");
                            Console.Write("Metabolismo ==> "); int metabolismo = Convert.ToInt32(Console.ReadLine());
                            while (validacionMetRepro(metabolismo).Equals(false))
                            { Console.WriteLine("Opcion incorrecta, vuelva a ingresar el número: "); metabolismo = Convert.ToInt32(Console.ReadLine()); }
                            Console.WriteLine(" ");
                            Console.WriteLine("Por favor, ingrese su tipo de Reproduccion: \n1). Sexual \n2). Asexual");
                            Console.Write("Reproduccion ==> "); int reproduccion = Convert.ToInt32(Console.ReadLine());
                            while (validacionMetRepro(reproduccion).Equals(false))
                            { Console.WriteLine("Opcion incorrecta, vuelva a ingresar el número: "); reproduccion = Convert.ToInt32(Console.ReadLine()); }
                            dato = new DatosDeEspecie(metabolismo, reproduccion, especie);
                        }

                        else
                        {
                            Console.WriteLine(" ");
                            Console.Write("Ingrese nombre de la Categoria: "); string nombre = Console.ReadLine();
                            cateNueva = true;
                            dato = new TipoDominio(nombre); 
                        }

                        NodoGeneral nodo = new NodoGeneral(dato, new ListaConArreglo());
                        ArbolGeneral arbol = new ArbolGeneral(nodo);
                        dominios.agregarHijos(arbol);
                        insertCategxCateg(arbol, nivel + 1, cateNueva);
                    }
                    // si eleccion es un nodo ya existente, ingresamos a ese nodo recursivamente.
                    else
                    {
                        if (eleccion >= error)
                        {
                            Console.WriteLine();
                            Console.WriteLine("El numero ingresado es incorrecto. Por favor, vuelva a intentarlo. ");
                            goto PedirOtraVez;
                        }
                        else
                        {
                            rec.comenzar();
                            while (rec.getPosicion() < eleccion - 1)
                            { rec.proximo(); }
                            ArbolGeneral arbolElegido = (ArbolGeneral)rec.elemento();
                            insertCategxCateg(arbolElegido, nivel + 1, cateNueva);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine("Ha ocurrido un error inesperado. . .\nPor favor, vuelva a intentar.");
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Este Método se utiliza para realizar la recursión cuando la categoria ingresada por consola, ya existe en el arbol.
        /// Su acción será ingresar en el/los hijos del arbol ingresado por parametropara verificar si existen y en caso contrario, crearlos.
        /// </summary>
        
        public static void agregarNodo(ArbolGeneral dominios, int categ, string[] dominioSeparado, int metabolismo, int reproduccion)
        {
            try
            {


                if (categ <= 6)
                {
                    bool existeNodo = false;
                    Recorredor rec1 = dominios.getHijos().recorrer();
                    rec1.ObjLista=dominios.getHijos();
                    rec1.comenzar();
                    while (!rec1.fin())
                    {
                        ArbolGeneral hijo = (ArbolGeneral)rec1.elemento();
                        string datoHijo = hijo.getDatoRaiz().Nombre;
                        //Verifica si la categoria ingresada por consola, no está en el arbol para no generar duplicidad
                        if (datoHijo.ToLower().Equals(dominioSeparado[categ].ToLower()))
                        {
                            existeNodo = true;
                            //Como es igual a la categoria pasada por consola, hacemos un llamado recursivo
                            agregarNodo(hijo, categ + 1, dominioSeparado, metabolismo, reproduccion);
                        }
                        rec1.proximo();
                    }

                    if (!existeNodo)
                    {
                        NodoGeneral nodo;
                        ArbolGeneral arbol;
                        if (categ.Equals(6))
                        {
                            DatosDeEspecie nombreEspecie = new DatosDeEspecie(metabolismo, reproduccion, dominioSeparado[categ]);
                            nodo = new NodoGeneral(nombreEspecie, new ListaConArreglo());
                            arbol = new ArbolGeneral(nodo);
                            dominios.agregarHijos(arbol);

                        }
                        else
                        {
                            TipoDominio nombreTipo = new TipoDominio(dominioSeparado[categ]);
                            nodo = new NodoGeneral(nombreTipo, new ListaConArreglo());
                            arbol = new ArbolGeneral(nodo);

                            dominios.agregarHijos(arbol);

                        }
                        agregarNodo(arbol, categ + 1, dominioSeparado, metabolismo, reproduccion);
                    }
                }
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine("Ha ocurrido un error inesperado. . .\nPor favor, vuelva a intentarlo.");
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }

        }

        private static bool validacionMetRepro(int numero)
        {
            if (numero.Equals(1) || numero.Equals(2)) { return true; }
            else { return false; }

        }

        private static void eliminacionTaxonomica(ArbolGeneral dominios)
        {
            Console.Clear();
            Console.WriteLine("\t\t\t Eliminar una Especie del Arbol Taxonomico  ");
            Console.WriteLine(" ");
            Console.WriteLine("Ingrese el dominio taxonomico a eliminar: ");
            Console.WriteLine(" ");

            try
            {


            VuelvoAPedir:

                string dominio_a_eliminar = Console.ReadLine();
                string[] dominio_separado = dominio_a_eliminar.Split('.');
                int nivel = 0;
                bool fue_encontrado = false;
                if (dominio_separado.Length.Equals(7))
                {
                    eliminarEspecie(dominios, dominio_separado, nivel);
                    Console.Clear();
                    Console.WriteLine("El domino Taxonómico {0} fue eliminado exitosamente . . .", dominio_a_eliminar);
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Estimado. El dominio ingresado no cumple con la cantidad de categorias necesarias para su busqueda.\nPor favor, vuelva a ingresar el dominio Taxonómico: ");
                    goto VuelvoAPedir;
                }
            }
            catch(NullReferenceException )
            {
                Console.Clear();
                Console.WriteLine("El dominio taxonomico que ha ingresado, no existe en el Arbol de dominios Taxonomicos.\n" +
                    "Por favor, intente nuevamente luego de haber consultado el Arbol de Domnios Taxonomicos.\n\n" +
                    "Para ello, vuelva al menú principal, seleccione el Modulo de Consultas, seguido de la opcion 'Imprimir el Arbol Taxonomico Completo'");
                //StreamWriter log_errores= new StreamWriter("c:\\")
                    Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine("Ha ocurrido un error inesperado. . . \nPor favor, vuelva a intentarlo ");
                Console.ReadKey();
            }

        }

        private static void eliminarEspecie(ArbolGeneral dominios, string[] dominio_separado, int nivel)
        {
            ArbolGeneral un_arbol_de_dominios;
            Recorredor rec = dominios.getHijos().recorrer(); rec.ObjLista = dominios.getHijos();
            rec.comenzar();
            bool finaliza_bucle = rec.fin(); //con el arbol conteniendo nodos, esta variable se inicializa en false
            while(finaliza_bucle.Equals(false))
            {
                
                un_arbol_de_dominios = (ArbolGeneral)rec.elemento();
                string nombre = un_arbol_de_dominios.Raiz.Dato.Nombre.ToLower();
                if (nombre.Equals(dominio_separado[nivel].ToLower()))
                {
                    if (un_arbol_de_dominios.esHoja())
                    {
                        dominios.eliminarHijo(un_arbol_de_dominios);
                        goto Salir;
                    }
                    else
                    {
                        eliminarEspecie(un_arbol_de_dominios, dominio_separado, nivel + 1);
                    }
                    finaliza_bucle = true;
                }
                else
                {
                    rec.proximo();
                }
            }
            un_arbol_de_dominios= (ArbolGeneral)rec.elemento();
            if (un_arbol_de_dominios.getHijos().getTamanio().Equals(0))
            {
                dominios.eliminarHijo(un_arbol_de_dominios);
            }
            // La etiqueta se puso para que cuando encuentre la especie a eliminar, la elimina y luego cuando sale del while,
            // no intente obtener el hijo de dominios (el cual no existirá ya que fue eliminado por la funcion "eliminarHijo"
            // solamente se realizará un goto por cada eliminacion que se pida.
        Salir:;

        }
        
    }
    
}
