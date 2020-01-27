using System.Collections.Generic;

namespace appTudo
{
    using Models;
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using Xamarin.Forms;

    public class MainPageViewModels : MvvmHelpers.BaseViewModel
    {
        #region Constructor
        public MainPageViewModels()
        {
            //inicializo los datos
            BusesAll = new ObservableCollection<buses>();
            IsPosting = true;
            IsNotBusy = true;
            IsBusy = false;
            BusEdicion = new buses();
            initData();
        }


        #endregion


        #region Propiedades
        private Services.RestClient client = new Services.RestClient();
        //Para definir si haré un post o un Put
        private string _btnTextPost;
        public string BtnTextPost
        {
            get { return _btnTextPost; }
            set { SetProperty(ref _btnTextPost, value); }
        }
        //Para distinguir si hace post o put
        private bool _isPosting;
        public bool IsPosting
        {
            get { return _isPosting; }
            set
            {
                SetProperty(ref _isPosting, value);
                if (value)
                {
                    Title = "Agrega un nuevo bus";
                    BtnTextPost = "Agregar";
                }
                else
                {
                    Title = "Editando datos del bus";
                    BtnTextPost = "Actualizar";
                }
            }
        }

        private List<string> _estados = new List<string>() { "Nuevo", "Servible", "Inservible" };
        public List<string> Estados
        {
            get { return _estados; }
            set { SetProperty(ref _estados, value); }
        }
        private buses _busEditando;
        public buses BusEdicion
        {
            get { return _busEditando; }
            set { SetProperty(ref _busEditando, value); }
        }
        private int _capacidad;
        public int capacidad
        {
            get { return _capacidad; }
            set { SetProperty(ref _capacidad, value); }
        }
        private int _year;
        public int year
        {
            get { return _year; }
            set { SetProperty(ref _year, value); }
        }

        private buses _busSelected;
        public buses BusSeleccionado
        {
            get { return _busSelected; }
            set
            {
                if (value != null)
                {
                    SetProperty(ref _busSelected, value);
                    //Display Alerts!!
                    OpcionesSelectedItem(value);
                    //Editar o borrar
                }
            }

        }


        private ObservableCollection<buses> _buses;
        public ObservableCollection<buses> BusesAll
        {
            get { return _buses; }
            set { SetProperty(ref _buses, value); }
        }
        #endregion



        #region Metodos y comando

        private Command _RefreshCommand;
        public Command RefreshCommand
        {
            get { return _RefreshCommand ?? (_RefreshCommand = new Command(initData)); }
            set { SetProperty(ref _RefreshCommand, value); }
        }
        private async void initData()
        {
            IsBusy = true;
            try
            {
                var datos = await client.GetAll();
                BusesAll = new ObservableCollection<buses>(datos);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            IsBusy = false;
        }


        private Command _PostPutCommand;
        public Command PostPutCommand
        {
            get { return _PostPutCommand ?? (_PostPutCommand = new Command(PostPutMethod)); }
            set { SetProperty(ref _PostPutCommand, value); }
        }
        private async void PostPutMethod()
        {
            //Valido los datos antes de hacer un post o put
            IsBusy = true;
            BusEdicion.capacidad = capacidad;
            BusEdicion.year = year;
            if (BusEdicion.capacidad < 0)
            {
                IsBusy = false;
                await App.Current.MainPage.DisplayAlert("Error", "Capacidad debe ser mayor a 0", "Ok");
                return;
            }
            if (BusEdicion.year < 1900)
            {
                IsBusy = false;
                await App.Current.MainPage.DisplayAlert("Error", "Año debe ser mayor a 1900", "Ok");
                return;
            }
            if (string.IsNullOrEmpty(BusEdicion.estado))
            {
                IsBusy = false;
                await App.Current.MainPage.DisplayAlert("Error", "Estado no puede ir sin datos", "Ok");
                return;
            }
            if (string.IsNullOrEmpty(BusEdicion.color))
            {
                IsBusy = false;
                await App.Current.MainPage.DisplayAlert("Error", "Color no puede ir sin datos", "Ok");
                return;
            }

            if (IsPosting)
            {
                //Post -publcico el objeto que se ha creado
                if (await client.Post(BusEdicion))
                {
                    IsBusy = false;
                    IsPosting = true;
                    capacidad = 0;
                    year = 0;
                    BusEdicion = new buses();
                    initData();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No es posible guardar los datos", "Ok");
                }

            }
            else
            {
                //Put - Actualizo en la base de datos
                if (await client.Put(BusEdicion.idBus, BusEdicion))
                {
                    IsBusy = false;
                    IsPosting = true;
                    capacidad = 0;
                    year = 0;
                    BusEdicion = new buses();
                    initData();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No es posible actualizar los datos", "Ok");
                    
                }
            }

        }


        private async void OpcionesSelectedItem(buses value)
        {
            if (await App.Current.MainPage.DisplayAlert("Opciones", "Deseas editar el registro", "Sí", "No"))
            {
                if (await App.Current.MainPage.DisplayAlert("Opciones", "", "Eliminar", "Editar"))
                {
                    //Eliminar
                    await client.Delete(value.idBus);
                    initData();
                }
                else
                {
                    IsPosting = false;
                    BusEdicion = value;
                    year = (int) value.year;
                    capacidad = (int)value.capacidad;
                }
            }
        }

        #endregion
    }
}
