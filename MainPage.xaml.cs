using System.Diagnostics;
using System.ComponentModel;

namespace Hangman {
    public partial class MainPage : ContentPage, INotifyPropertyChanged {
        List<string> palabras = new List<string>() {"interes", "trece", "estilo", "queso", "zapato",
        "amarillo", "maui", "cuento", "informe", "cerezo",
        "oceano", "simbolo", "pulir", "volumen", "iglu",
        };

        string respuesta = "";

        int index = 0;

        List<char> seleccionadas = new List<char>();

        Random rand = new Random();

        private string spotlight = "";
        public string Spotlight {
            get => spotlight;
            set {
                spotlight = value;
                OnPropertyChanged();
            }
        }

        private List<char> letras = new List<char>();
        public List<char> Letras {
            get => letras;
            set {
                letras = value;
                OnPropertyChanged();
            }
        } 

        public void escogerPalabra() {
            index = rand.Next(0, palabras.Count);
            respuesta = palabras[index];
            Debug.WriteLine(respuesta);
        }

        private void EnmascararPalabra(String palabra, List<char> letras) {
            var mascara = palabra
                .Select(x => letras.Contains(x) ? x : '_')
                .ToArray();
            Spotlight = string.Join(" ", mascara);
        }

        private void ManejarLetra (char letra) {

            if (seleccionadas.IndexOf(letra) == -1) {
                seleccionadas.Add(letra);
               
                if (respuesta.IndexOf(letra) < 0) {
                    errores++;
                    Debug.WriteLine(errores);
                }

                ActualizarEstado();

            }
            if (respuesta.IndexOf(letra) >= 0) {
                EnmascararPalabra(respuesta, seleccionadas);
                
                RevisarSiGano();
            }
            
        }

        private string msj = string.Empty;
        public string Mensaje {
            get => msj;
            set {
                msj = value;
                OnPropertyChanged();
            }
        }

        private string est = "Errores: 0 de 6";
        public string Estado {
            get => est;
            set { 
                est = value; 
                OnPropertyChanged();
            }
        }

        public int errores = 0;

        private string img = "img0.jpg";
        public string Imagen {
            get => img;
            set {
                img = value;
                OnPropertyChanged();
            }
        }
        private void RevisarSiGano() {
            if (Spotlight.Replace(" ", "") == respuesta) {
                Mensaje = "Ganaste!";
            }
        }
        private void ActualizarEstado() {
            Estado = $"Errores: {errores} de 6";
            Imagen = $"img{errores}.jpg";
            if (errores == 6) {
                Mensaje = "Perdiste!";
            }
        }

        public MainPage() {
            InitializeComponent();
            BindingContext = this;
            escogerPalabra();
            Letras = new List<char>("abcdefghijklmnopqrstuvwxyz".ToCharArray());
            EnmascararPalabra(respuesta, seleccionadas);
        }

        private void Button_Clicked(object sender, EventArgs e) {
            var btn = sender as Button;
            char letra = btn.Text[0];
            ManejarLetra(letra);
            Debug.Write(letra);
        }
    }
}
