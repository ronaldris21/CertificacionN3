using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace appCalculadora
{
    public class MainPageViewModel : MvvmHelpers.BaseViewModel
    {
        //Constructor
        public MainPageViewModel()
        {
            Num1 = 0;
            Num2 = 0;

        }

        #region Propiedades
        private string _operacion;
        public string Operacion
        {
            get { return _operacion; }
            set { SetProperty(ref _operacion, value); }
        }
        //Los datos qye voy a ir mostrando en la pantalla
        private string _textOnScreen;
        public string TextOnScreen
        {
            get { return _textOnScreen; }
            set { SetProperty(ref _textOnScreen, value); }
        }
        //Variable para hacer las operaciones!
        private decimal _Num1;
        public decimal Num1
        {
            get { return _Num1; }
            set { SetProperty(ref _Num1, value); }
        }
        private decimal _Num2;
        public decimal Num2
        {
            get { return _Num2; }
            set { SetProperty(ref _Num2, value); }
        }
        private decimal resultado;
        private string lastOperacion;
        #endregion

        #region Commandos y metodos
        //Cuando agrega un numero o un punto

        private Xamarin.Forms.Command _NumeroAdded;
        public Xamarin.Forms.Command NumeroAdded
        {
            get => _NumeroAdded ?? (_NumeroAdded = new Xamarin.Forms.Command<string>(NumeroAddedMethod));
        }
        //Agreggo los numeros
        private void NumeroAddedMethod(string dato)
        {
            try
            {
                if (dato==".")
                {
                    if (!TextOnScreen.Contains(".")) //Valido si no tiene ya un punto en los datos
                    {
                        TextOnScreen += ".";
                    }
                }
                else
                {
                    string temp = TextOnScreen + dato;
                    decimal tempd = Convert.ToDecimal(temp);
                    TextOnScreen = tempd.ToString();
                }
                
                
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }


        }



        /// <summary>
        /// Para cuando elige una operacion y es la que se hara a continuación
        /// </summary>
        private Xamarin.Forms.Command _operacionCommand;
        public Xamarin.Forms.Command OperacionCommand
        {
            get => _operacionCommand ?? (_operacionCommand = new Xamarin.Forms.Command<string>(OperacionCommandMethod));
        }
        private void OperacionCommandMethod(string ope)
        {
            switch (ope)
            {
                case "/":
                    Num1 = Convert.ToDecimal(TextOnScreen);
                    lastOperacion = ope;
                    TextOnScreen = string.Empty;
                    break;

                case "*":
                    Num1 = Convert.ToDecimal(TextOnScreen);
                    TextOnScreen = string.Empty;
                    lastOperacion = ope;

                    break;

                case "-":
                    Num1 = Convert.ToDecimal(TextOnScreen);
                    TextOnScreen = string.Empty;
                    lastOperacion = ope;

                    break;

                case "+":
                    Num1 = Convert.ToDecimal(TextOnScreen);
                    lastOperacion = ope;
                    TextOnScreen = string.Empty;

                    break;

                case "=":
                    Num2 = Convert.ToDecimal(TextOnScreen);
                    OperacionResultado();
                    lastOperacion = string.Empty;
                    TextOnScreen = string.Empty;
                    break;

                case "C":
                    TextOnScreen = string.Empty;

                    break;
            }
        }

        private void OperacionResultado()
        {
            switch (lastOperacion)
            {
                case "/":
                    try
                    {
                        TextOnScreen = (Num1 / Num2).ToString();
                    }
                    catch (Exception ex)
                    {
                        App.Current.MainPage.DisplayAlert("Error", "División entre cero", "Ok");
                    }
                    break;

                case "*":
                    TextOnScreen = (Num1 * Num2).ToString();

                    break;

                case "-":
                    TextOnScreen = (Num1 - Num2).ToString();

                    break;

                case "+":
                    TextOnScreen = (Num1 + Num2).ToString();

                    break;


            }
        }
        #endregion
    }
}
