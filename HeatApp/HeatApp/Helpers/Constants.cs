using System;
using System.Collections.Generic;
using System.Text;

namespace HeatApp
{
    public static class Constants
    {
        #region Properties
        public const string UrlAPI = "https://heatapi.softcorpapp.com/";
        public const string GoogleMapsAPIKey = "AIzaSyD2ZjqBONONbEATiZT8yX97iwm2biuJI5o";
        #endregion

        #region Messages
        public static class Messages
        {
            public const string Loading = "Cargando";
            public const string SearchingStops = "Buscando paradas";

            public static class Validation
            {
                #region Empty
                public const string EmptyName = "Debe ingresar su nombre";
                public const string EmptyLastName = "Debe ingresar su apellido";
                public const string EmptyPhone = "Debe ingresar su telefono";
                public const string EmptyEmail = "Debe ingresar su correo electrónico";
                public const string EmptyPassword = "Debe ingresar una contraseña";
                public const string EmptyConfirmPassword = "Debe confirmar la contraseña";
                #endregion

                #region Invalid
                public const string InvalidEmail = "Correo Electrónico invalido";
                #endregion

                #region Other
                public const string PasswordsDontMatch = "Contraseña y confirmar contraseña no coinciden";
                #endregion
            }
        }
        #endregion
    }
}