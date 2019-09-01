namespace SNDT
{
    public abstract class RecorredorAbst
    {
        private Lista objLista;
        private int actual;

        public abstract void comenzar();

        public abstract object elemento();

        public abstract void proximo();

        public abstract bool fin();

        public abstract void agregar(object elem);

        public abstract void eliminar();

    }
}