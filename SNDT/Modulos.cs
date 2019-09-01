using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SNDT
{
    
    public class Modulos : IModulos
    {
        public void Menu(ArbolGeneral sv) { }
        
        public static void modAdministracion(ArbolGeneral dominios) //Pasado
        {
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
                            moduloAgregar(dominios);
                            break;
                        case '2':
                            eliminacionTaxonomica(dominios);
                            break;
                        case '3':
                            flagSalir = true;
                            break;
                        default:
                            Console.WriteLine("opcion incorrecta. Ingrese nuevamente.");
                            Thread.Sleep(2000);
                            Console.Clear();
                            modAdministracion(dominios);
                            break;
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

        }

        #region Agregar un nuevo Dominio Taxonómico al Arbol
        public static void moduloAgregar(ArbolGeneral dominios)
        {
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
                                    Lista hijosDelArbol = dominios.getHijos();
                                    int categ = 0;

                                    //Dar de alta un hijo si el arbol esta vacio:
                                    if (hijosDelArbol.getTamanio().Equals(0))
                                    {
                                        try
                                        {
                                            agregarNodo(dominios, categ, dominioSeparado, metabolismo, reproduccion);


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

                                    }
                                    // si no esta vacio, verificar que cada categoria no exista y de ser asi, agregarla, sino ignorarla
                                    else
                                    {
                                        agregarNodo(dominios, categ, dominioSeparado, metabolismo, reproduccion);
                                        Console.WriteLine("El Dominio ingresado, fue dado de alta satisfactoriamente.");
                                        Console.ReadKey();
                                        flag = true;
                                    }


                                }

                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine(inDominio);
                                    Console.WriteLine("El dominio ingresado no es correcto.\nIntente nuevamente . . .");
                                    Console.ReadKey();
                                }


                            } while (!flag);
                            break;

                        case '2':
                            int nivel = 0;
                            bool cateNueva = false;
                            categXcateg(dominios, nivel, cateNueva);
                            break;

                        case '3':
                            flagSalir = true;
                            Console.Clear();
                            break;
                        default:
                            Console.WriteLine("opcion incorrecta. Ingrese nuevamente.");
                            Console.ReadKey(); ;
                            Console.Clear();
                            modAdministracion(dominios);
                            break;
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

        public static void categXcateg(ArbolGeneral dominios, int nivel, bool cateNueva)
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
                            dato = new TipoDominio(); dato.setNombre(nombre);
                        }

                        NodoGeneral nodo = new NodoGeneral(dato, new ListaConArreglo());
                        ArbolGeneral arbol = new ArbolGeneral(nodo);
                        dominios.agregarHijos(arbol);
                        categXcateg(arbol, nivel + 1, cateNueva);
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
                            categXcateg(arbolElegido, nivel + 1, cateNueva);
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
                        ArbolGeneral rama = aux_agregado(dominioSeparado[categ], dominios, categ, metabolismo, reproduccion);
                        agregarNodo(rama, categ + 1, dominioSeparado, metabolismo, reproduccion);
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

        public static ArbolGeneral aux_agregado(string nombre, ArbolGeneral alCualAgregar, int categ, int metabolismo, int reproduccion)
        {
            try
            {
                NodoGeneral nodo;
                ArbolGeneral arbol;
                if (categ.Equals(6))
                {
                    DatosDeEspecie nombreEspecie = new DatosDeEspecie(metabolismo, reproduccion, nombre);
                    nodo = new NodoGeneral(nombreEspecie, new ListaConArreglo());
                    arbol = new ArbolGeneral(nodo);
                    alCualAgregar.agregarHijos(arbol);

                }
                else
                {
                    TipoDominio nombreTipo = new TipoDominio(); nombreTipo.setNombre(nombre);
                    nodo = new NodoGeneral(nombreTipo, new ListaConArreglo());
                    arbol = new ArbolGeneral(nodo);

                    alCualAgregar.agregarHijos(arbol);

                }
                return arbol;
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine("Ha ocurrido un error inesperado. . .\nPor favor, vuelva a intentar.");
                Console.WriteLine(e.Message);
                Console.ReadKey();
                return null;
            }
        }
        #endregion

        #region Eliminar un Dominio Taxonómico
        public static void eliminacionTaxonomica(ArbolGeneral dominios)
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
                    eliminarEspecie(dominios, dominio_separado, nivel, fue_encontrado);
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
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine("Ha ocurrido un error inesperado. . . \nPor favor, vuelva a intentarlo ");
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }

        }

        public static void eliminarEspecie(ArbolGeneral dominios, string[] dominio_separado,int nivel, bool fue_encontrado)
        {
            try
            {
                //Iniciamos a recorrer los hijos del Elemento "Seres Vivos" (primer elemento y padre de todos los nodos)
                // 'dominios' contiene todo el arbol taxonomico
                Recorredor rec = new Recorredor(); rec.setLista(dominios.getHijos());
                rec.comenzar();
                ArbolGeneral unArbol;

                //al volver de la recursión, elimina a dos especies si las hay.
                while (!rec.fin() & fue_encontrado.Equals(false))
                {
                    if (nivel <= 6)
                    {
                        unArbol = (ArbolGeneral)rec.elemento();

                        if (unArbol.getDatoRaiz().getNombre().Equals(dominio_separado[nivel]))
                        {
                            if (unArbol.esHoja())
                            {
                                dominios.eliminarHijo(unArbol);
                                
                            }
                            else
                            {
                                eliminarEspecie(unArbol, dominio_separado, nivel + 1, fue_encontrado);

                            }

                            fue_encontrado = true;

                        }
                        else
                        {
                            rec.proximo();
                        }
                    }
                    
                    
                }
                Type tipoDominio = typeof(TipoDominio);
                unArbol = (ArbolGeneral)rec.elemento();
                if (unArbol.esHoja() & unArbol.getDatoRaiz().GetType().Equals(tipoDominio))
                {
                    dominios.eliminarHijo(unArbol);
                }
                
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine("Ha ocurrido un error inesperado en el eliminar especie. . .\nPor favor, vuelva a intentarlo.");
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        public static void buscarEspecies(ArbolGeneral dominios, int nivel, List<object> lista)
        {
            try
            {
                if (nivel <= 6)
                {
                    Recorredor rec = dominios.getHijos().recorrer(); rec.setLista(dominios.getHijos());
                    rec.comenzar();
                    while (!rec.fin())
                    {
                        ArbolGeneral unArbol = (ArbolGeneral)rec.elemento();
                        buscarEspecies(unArbol, nivel + 1, lista);
                        rec.proximo();

                    }
                }
                else
                {
                    lista.Add(dominios.getDatoRaiz().getNombre());
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
        #endregion

        public static void modConsultas(ArbolGeneral dominios)
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
                            imprimirArbolEntero(dominios);
                            flagSalir = true;
                            Console.ReadKey();
                            break;
                        case 2:
                            imprimirTaxonomiaSegunDominio(dominios);
                            flagSalir = true;
                            Console.ReadKey();
                            break;
                        case 3:
                            imprimirEspecieSegunClase(dominios);
                            flagSalir = true;
                            Console.ReadKey();
                            break;
                        case 4:
                            imprimirCategoriaAProfundidad(dominios);
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
                Recorredor rec = new Recorredor(); rec.setLista(dominios.getHijos());
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

        public static void pre_orden(ArbolGeneral arbol)
        {
            try
            {

                if (arbol.esHoja())
                {

                    if (arbol.getDatoRaiz().getNombre().ToLower().Equals("Seres Vivos".ToLower()))
                    {
                        Console.WriteLine(arbol.getDatoRaiz().getNombre());
                    }
                    else
                    {
                        DatosDeEspecie dato = (DatosDeEspecie)arbol.getDatoRaiz();
                        Console.WriteLine(" ");
                        Console.Write("Especie ==> {0} \n\t\tMetabolismo: {1}\n\t\tReproduccion: {2}", dato.getNombre(), dato.getMetabolismo(), dato.getReproduccion());
                    }
                }

                else
                {
                    Console.Write(arbol.getDatoRaiz().getNombre() + ".");
                    Lista hijosArbol = arbol.getHijos();
                    Recorredor rec = new Recorredor();
                    rec.setLista(hijosArbol);
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
        public static bool validacionMetRepro(int numero)
        {
            if (numero.Equals(1) || numero.Equals(2)) { return true; }
            else { return false; }

        }

        public static void imprimirTaxonomiaSegunDominio(ArbolGeneral dominios)
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
                DatosDeEspecie especieDelUsuario= new DatosDeEspecie();
                if (dominioSeparado.Length.Equals(7))
                {
                    especieDelUsuario = buscarEspecieEnArbol(dominios, dominioSeparado, nivel, especieDelUsuario);
                    Console.WriteLine("Para la especie {0}: \nSu reproduccion es: {1}\nSu Metabolismo es: {2}",
                                       especieDelUsuario.getNombre(), especieDelUsuario.getReproduccion(), especieDelUsuario.getMetabolismo());
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

        public static DatosDeEspecie buscarEspecieEnArbol(ArbolGeneral dominios, string[] dominios_separados, int nivel, DatosDeEspecie especieEncontrada)
        {

            Recorredor rec = dominios.getHijos().recorrer();
            rec.setLista(dominios.getHijos());
            rec.comenzar();
            ArbolGeneral unArbol = (ArbolGeneral)rec.elemento();

            while (!rec.fin())
            {
                unArbol = (ArbolGeneral)rec.elemento();
                if (unArbol.getDatoRaiz().getNombre().Equals(dominios_separados[nivel]))
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

        public static void imprimirEspecieSegunClase(ArbolGeneral dominios)
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

        public static void buscarOrdenesEnArbol(ArbolGeneral dominios, string[] dominioSeparado, int nivel, List<List<ArbolGeneral>> listaDeArboles, List<ArbolGeneral> listaDeOrdenes)
        {
            Recorredor rec = dominios.getHijos().recorrer();
            rec.setLista(dominios.getHijos());
            rec.comenzar();
            ArbolGeneral puntero;
            List<ArbolGeneral> aux = listaDeOrdenes;
            if (nivel < 3)
            {
                while (!rec.fin())
                {
                    puntero = (ArbolGeneral)rec.elemento();
                    if (puntero.getDatoRaiz().getNombre().Equals(dominioSeparado[nivel]))
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

        public static void imprimirCategoriaAProfundidad(ArbolGeneral dominios)
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

        public static void buscarCategoria (ArbolGeneral dominios, int profundidad, int nivel)
        {
            Recorredor rec = dominios.getHijos().recorrer();
            rec.setLista(dominios.getHijos());
            rec.comenzar();
            ArbolGeneral puntero;
            while(!rec.fin())
            {
                puntero = (ArbolGeneral)rec.elemento();
                if (profundidad-1 == nivel)
                {
                    Console.WriteLine(puntero.getDatoRaiz().getNombre());
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
