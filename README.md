# **Heat - Aplicación móvil**

##### Este repositorio incluye todo lo relacionado a la aplicación móvil del proyecto Heat. Heat una aplicación que busca llevar una solución que pueda brindar informaciones para mejorar la experiencia del transporte público.

# Prerequisitos
#####  * Sistema operativo: macOS 10.14 o Windows 10
#####  * [Visual Studio Community](https://visualstudio.microsoft.com/downloads/)
#####  * [Xamarin.Forms](https://dotnet.microsoft.com/learn/xamarin/hello-world-tutorial/install)

# Instalación y uso

##### Para instalar el proyecto, solo basta con tener el código fuente y cumplir los prerequisitos, Visual Studio se encargará de descargar las dependencias para el correcto funcionamiento del proyecto una vez abierto en Visual Studio. Para poder ejecutar el proyecto, dependerá desde que plataforma se está trabajando

### Visual Studio for Mac
##### Si está ejecutando en una Mac, puede apuntar a emuladores de iOS o Android casi listos para usar. Visual Studio para Mac viene con los simuladores de iOS integrados, por lo que cuando se desarrolla en una Mac, esta es la plataforma más fácil de apuntar para realizar pruebas rápidas de funcionalidad. Sin embargo, Android no requiere mucha configuración para ponerlo en funcionamiento.

##### Haga clic con el botón derecho en el proyecto de la plataforma que elija en el panel Explorador de soluciones y haga clic en Establecer como proyecto de inicio.

##### Si lo hace, podrá ejecutar la aplicación cuando haga clic en el icono de inicio `("reproducir")` en la parte superior izquierda o presione F5.

##### La primera vez que apunte a Android con una instalación nueva de Visual Studio, puede encontrar que cuando se ejecuta inicialmente, carga el Administrador de dispositivos Android con una lista en blanco de dispositivos como se muestra a continuación.

##### Haga clic en `+ Nuevo dispositivo` en la parte superior izquierda.

##### Cuando aparezca la ventana `Nuevo dispositivo`, deje todos los valores predeterminados sin modificar y haga clic en `Crear`.
##### La primera vez que ejecute emuladores de `iOS` o `Android`, la aplicación tardará un tiempo en iniciarse en frío. Android es particularmente lento, tanto al iniciar el emulador como al compilar e implementar la aplicación. Esto se debe al trabajo que se está realizando debajo que Android requiere para una aplicación; así que si lleva algún tiempo no se preocupe de que algo esté mal.

##### Si tiene un dispositivo físico conectado mediante un cable USB, puede elegir ejecutar la aplicación en él o en el simulador desde la lista desplegable a la derecha de las tres entradas diferentes que ve a la derecha del icono de reproducción. La primera opción es la plataforma elegida, la segunda es la configuración de implementación, ya sea Debug o Release, y la tercera es el dispositivo de destino.

### Visual Studio Community

#### iOS
##### Primero está iOS, ya que es el más complicado. Puede notar que no puede implementar su proyecto de iOS de inmediato. El acuerdo de licencia de Apple establece que debe utilizar "Xcode Build Tools" para compilar aplicaciones iOS. Sin embargo, Xcode solo está disponible en Mac. Esto significa que necesita acceso a una Mac para actuar como un "Agente de compilación".

##### Si ya tiene un dispositivo Mac, ya sea una Macbook, Mac o iMac, siempre que esté en la misma red que su máquina de desarrollo, podrá conectarse a ella desde Visual Studio seleccionando el menú `Herramientas > iOS` y haciendo clic en `Emparejar a Mac`.

##### Si no tiene una Mac, puede aprovechar los servicios de "alquiler" en la nube como `MacInCloud`, que le permiten usar una Mac basada en la nube por una pequeña tarifa. La Mac basada en la nube le proporcionará una dirección de red para que pueda conectarse y usarla como agente de compilación.

##### Una vez que se establezca la conexión con su dispositivo macOS, aparecerá en la ventana Emparejar a Mac con un pequeño icono de enlace de cadena a la derecha, lo que significa que su máquina está conectada correctamente.

##### Lo sorprendente de apuntar a los simuladores de iOS en Windows es que los simuladores se están ejecutando en la Mac a la que está conectado, simplemente colocando la ventana para que la vea. Si posee una computadora portátil con pantalla táctil, puede interactuar con la aplicación como un teléfono real, ¡que es un paso mejor que los simuladores en las propias Mac!

#### Android

##### Ahora para Android, que es mucho más sencillo. Si está ejecutando un emulador de Android por primera vez, verá aparecer la ventana del Administrador de dispositivos Android sin dispositivos. Haga clic en Nuevo en la parte superior derecha para crear un nuevo dispositivo. Luego presiona en `Ejecutar`.

## Librerias
* ##### [Xamarin.Essentials](https://docs.microsoft.com/en-us/xamarin/essentials/)
* ##### [Newtonsoft.Json](https://www.newtonsoft.com/json)
* ##### [Essential UI Kit](https://github.com/syncfusion/essential-ui-kit-for-xamarin.forms)
* ##### [Xamarin.Forms Maps](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/map/)

## Contribuidores
* ##### [Carlos Acosta](http://github.com/cacosta9822)
* ##### [Cesar Lachapelle](http://github.com/Cesarlachapelle)
* ##### [Elliot Ruiz](http://github.com/retr0Tech)
* ##### [Gerzon Zorrilla](http://github.com/gerzonc)
* ##### [Abraham Duran](http://github.com/abrahamduran)
