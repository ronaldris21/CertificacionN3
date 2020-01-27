using System;

namespace appTudo.Models
{
    public class buses : MvvmHelpers.ObservableObject
    {
        /// <summary>
        /// Clase que administra los buses de la base de datos
        /// </summary>
        public int idBus { get; set; }
        private Nullable<int> _capacidad;
        public Nullable<int> capacidad
        {
            get { return _capacidad; }
            set { SetProperty(ref _capacidad, value); }
        }
        private Nullable<int> _year;
        public Nullable<int> year
        {
            get { return _year; }
            set { SetProperty(ref _year, value); }
        }
        public string color { get; set; }
        private string _estado;
        public string estado
        {
            get { return _estado; }
            set { SetProperty(ref _estado, value);
                //Acá me aseguro que los datos cambien el color a utilizar segudn el estado
                if (estado== "Nuevo")
                {
                    ColorX = Xamarin.Forms.Color.LightGreen;
                }
                else if (estado == "Servible")
                {
                    ColorX = Xamarin.Forms.Color.LightYellow;
                }
                else if (estado == "Inservible")
                {
                    ColorX = Xamarin.Forms.Color.LightCoral;
                }
            }
        }

        private Xamarin.Forms.Color _color;
        public Xamarin.Forms.Color ColorX
        {
            get { return _color; }
            set { SetProperty(ref _color, value); }
        }
    }
}
