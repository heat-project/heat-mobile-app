using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HeatApp.Models.HeatModels.Help;
using Xamarin.Forms;

namespace HeatApp.ViewModels.HeatViewModels.Common
{
    public class HelpViewModel : BaseViewModel
    {
        #region Constructors
        public HelpViewModel(INavigation navigation) : base(navigation)
        {
            SetHelp();
        }
        #endregion

        #region Properties
        private ObservableCollection<CommonHelp> helps = new ObservableCollection<CommonHelp>();
        public ObservableCollection<CommonHelp> Helps
        {
            get => helps;
            set => SetProperty(ref helps, value);
        }
        #endregion

        #region Methods
        private void SetHelp()
        {
            Helps.Add(new CommonHelp
            {
                Title = "Rutas",
                Content = new List<HelpContent>
                    {
                       new HelpContent
                       {
                           Title = "General",
                           Description = "Las rutas presentes en la aplicación son aquellas que se encuentran en el Gran Santo Domingo. Es decir, que este sistema esta limitado a esta demarcación."
                       },
                       new HelpContent
                       {
                           Title = "¿Cómo puedo ver las rutas?",
                           Description = "En el mapa de la aplicación, en la parte inferior, al seleccionar una ruta, inmediatamente, puedes ver el recorrido total de la ruta y sus paradas. Para más información sobre las paradas, dirijete a la sección Paradas.",
                           HasImage = true,
                           Image ="HelpRutas.svg"
                       },
                       new HelpContent
                       {
                           Title = "¿Cómo puedo ver la parada más cercana de una ruta?",
                           Description = "Cuando seleccionas una ruta, puedes ver dos botones ubicados en la parte de la derecha del map, incluyendo uno que dice 'Parada recomendad'. Al presionar este botón, se traza la ruta más corta desde tu posición hasta la parada más cercana."
                       }
                    }
            });

            Helps.Add(new CommonHelp
            {
                Title = "Paradas",
                Content = new List<HelpContent>
                    {
                       new HelpContent
                       {
                           Title = "¿Cómo puedo ver dirección en de una parada?",
                           Description = "Para ver todo la información que corresponde a una parada, basta con seleccionarla, y automaticamente podrás ver tales informaciones como: Ruta a la que pertenece, nombre, dirección, etc.",
                           HasImage = true,
                           Image ="HelpParadas.svg"
                       },
                       new HelpContent
                       {
                           Title = "Ir a parada",
                           Description = "Para ver la ruta desde tu posición hasta una parada en específico, solo debes seleccionarla, y en la parta inferior verás un botón con el titulo 'Ir a parada', seleccionas el mismo, y automaticamente tendrás la ruta más cercana a la parada."
                       }
                    }
            });


            Helps.Add(new CommonHelp
            {
                Title = "Vehiculos",
                Content = new List<HelpContent>
                    {
                       new HelpContent
                       {
                           Title = "General",
                           Description = "Los vehiculos que se pueden ver en movimiento en el mapa, son todos los vehiculos activos, y cada movimiento de estos es en tiempo real. Cada vehiculo pertenece a una ruta en específico. Para ver las caracteristicas del mismo basta con seleccionarlo."
                       },
                       new HelpContent
                       {
                           Title = "Conductor",
                           Description = "Cada vehiculo tiene un conductor diferente, y cuando seleccionas el vehiculo, podemos ver su nombre, como también un puntuación del mismo."
                       },
                       new HelpContent
                       {
                           Title = "Características",
                           Description = "Para ver las características de un vehiculo basta con seleccionarlo, ahí se ven los atributos, como: Wi-Fi, aire acondicionado, cobro con tarjeta, etc.",
                           HasImage = true,
                           Image ="HelpVehiculo.svg"
                       },
                       new HelpContent
                       {
                           Title = "Comentarios del vehiculo",
                           Description = "Puedes agregar un comentario acerca del vehiculo y/o de su conductor actual. También, puedes reportar comentarios ya existentes. Para más información, dirijete a la sección Comentarios.",
                           HasImage = true,
                           Image ="HelpNuevoComentario.svg"
                       }
                    }
            });

            Helps.Add(new CommonHelp
            {
                Title = "Comentarios",
                Content = new List<HelpContent>
                    {
                       new HelpContent
                       {
                           Title = "General",
                           Description = "Los comentarios son una de las funcionalidades más importantesn de esta aplicación,"+
                           " ya que con ellos buscamos que cada usuario reporte su experiencia presente o pasada respecto a un vehiculo o su conductor. "+
                           "Es por esto, que los usuarios pueden realizar un comentario al seleccionar un vehiculo. Por otro lado, los comentarios pueden ser reportados, para que asi nuestra comunidad de usuarios no se vean afectados.",
                           HasImage = true,
                           Image ="HelpComentarios.svg"
                       },
                       new HelpContent
                       {
                           Title = "¿Cómo puedo reportar un comentario?",
                           Description = "Para reportar un comentario, simplemente puedes presionar el botón que se encuentra en la parte superior derecha de dicho comentario, y te apareceran las distintas razones por la que el comentario debe ser reportado."+
                           " Luego seleccionar la razon, automaticamente, se realizara el reporte, y no podrás ver el comentario.",
                           HasImage = true,
                           Image ="HelpReporte.svg"
                       }
                    }
            });

        }
        #endregion
    }
}
