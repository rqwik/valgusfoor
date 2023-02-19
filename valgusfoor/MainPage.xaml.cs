using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace valgusfoor
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        Frame redLight, yellowLight, greenLight;
        Label lb1, lb2, lb3;
        bool bl = false;

        public MainPage()
        {
            //InitializeComponent();
            lb1 = new Label
            {
                Text = "Punane",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                VerticalOptions = LayoutOptions.Center
            };
            lb2 = new Label
            {
                Text = "Kollane",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                VerticalOptions = LayoutOptions.Center
            };
            lb3 = new Label
            {
                Text = "Roheline",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                VerticalOptions = LayoutOptions.Center
            };

            // Создание элементов светофора
            redLight = new Frame
            {
                BackgroundColor = Color.Gray,
                Content = lb1,
                CornerRadius = 100,
                HeightRequest = 100,
                WidthRequest = 100,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            yellowLight = new Frame
            {
                BackgroundColor = Color.Gray,
                Content = lb2,
                CornerRadius = 100,
                HeightRequest = 100,
                WidthRequest = 100,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            greenLight = new Frame
            {
                BackgroundColor = Color.Gray,
                Content = lb3,
                CornerRadius = 100,
                HeightRequest = 100,
                WidthRequest = 100,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };


            // Создание контейнера для светофора
            var trafficLightContainer = new Frame
            {
                CornerRadius = 100, //50
                BackgroundColor = Color.LightGray,
                HasShadow = false,
                Padding = 30,
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.Center,
                    Children =
                    {
                        redLight,
                        yellowLight,
                        greenLight
                    }
                }

            };
            // Создание кнопок управления светофором
            var turnOffButton = new Button { Text = "Sisse" };
            turnOffButton.Clicked += TurnOffButton_Clicked;
            var turnOnButton = new Button { Text = "Välja" };
            turnOnButton.Clicked += TurnOnButton_Clicked;
            var rndButton = new Button { Text = "My variant" };
            rndButton.Clicked += RndButton_Clicked;

            // Создание заголовка
            var headerLabel = new Label { Text = "Valgusfoor App", FontSize = 24, HorizontalOptions = LayoutOptions.Center };

            // Создание контейнера для кнопок и заголовка
            var controlsContainer = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center,
                Children =
                {
                    turnOffButton,
                    turnOnButton,
                    rndButton
                }
            };
            // Создание главного контейнера страницы
            Content = new StackLayout
            {
                Padding = 30,
                Children =
                {
                    headerLabel,
                    trafficLightContainer,
                    controlsContainer
                }
            };
            TapGestureRecognizer tap = new TapGestureRecognizer();
            TapGestureRecognizer tap2 = new TapGestureRecognizer();
            TapGestureRecognizer tap3 = new TapGestureRecognizer();

            tap.Tapped += Tap_Tapped;
            tap2.Tapped += Tap2_Tapped;
            tap3.Tapped += Tap3_Tapped;

            redLight.GestureRecognizers.Add(tap);
            yellowLight.GestureRecognizers.Add(tap2);
            greenLight.GestureRecognizers.Add(tap3);
        }

        Random rnd = new Random();
        private async void RndButton_Clicked(object sender, EventArgs e)
        {
            int r = rnd.Next(0, 255);
            int g = rnd.Next(0, 255);
            int b = rnd.Next(0, 255);
            redLight.BackgroundColor = Color.FromRgb(r, g, b);
            redLight.CornerRadius = 36;
            r = rnd.Next(0, 255);
            g = rnd.Next(0, 255);
            b = rnd.Next(0, 255);
            yellowLight.BackgroundColor = Color.FromRgb(r, g, b);
            r = rnd.Next(0, 255);
            g = rnd.Next(0, 255);
            b = rnd.Next(0, 255);
            greenLight.BackgroundColor = Color.FromRgb(r, g, b);
            greenLight.CornerRadius = 2;
        }

        private async void Tap3_Tapped(object sender, EventArgs e)
        {
            if (bl)
            {
                if (greenLight.BackgroundColor == Color.Green)
                {
                    lb3.Text = "Mine";
                }
                else if (greenLight.BackgroundColor == Color.Gray)
                {
                    lb3.Text = "Roheline";
                }
            }
            else
            {
                lb3.Text = "Palun lülita valgusfoor sisse";
            }
        }

        private async void Tap2_Tapped(object sender, EventArgs e)
        {
            if (bl)
            {
                if (yellowLight.BackgroundColor == Color.Yellow)
                {
                    lb2.Text = "Oota";
                }
                else if (yellowLight.BackgroundColor == Color.Gray)
                {
                    lb2.Text = "Kollane";
                }
            }
            else
            {
                lb2.Text = "Palun lülita valgusfoor sisse";
            }
        }

        private async void Tap_Tapped(object sender, EventArgs e)
        {
            if (bl)
            {
                if (redLight.BackgroundColor == Color.Red)
                {
                    lb1.Text = "STOP";
                }
                else if (redLight.BackgroundColor == Color.Gray)
                {
                    lb1.Text = "Punane";
                }
            }
            else
            {
                lb1.Text = "Palun lülita valgusfoor sisse";
            }
        }

        private async void TurnOffButton_Clicked(object sender, EventArgs e)
        {
            bl = true;
            if (bl)
            {
                lb1.Text = "Punane";
                lb2.Text = "Kollane";
                lb3.Text = "Roheline";
            }
            if (redLight.BackgroundColor == Color.Gray)
            {
                lb1.Text = "Punane";
            }
            if (yellowLight.BackgroundColor == Color.Gray)
            {
                lb2.Text = "Kollane";
            }
            if (greenLight.BackgroundColor == Color.Gray)
            {
                lb3.Text = "Roheline";
            }

            while (bl)
            {
                redLight.BackgroundColor = Color.Red;
                await Task.Delay(3000);
                redLight.BackgroundColor = Color.Gray;
                yellowLight.BackgroundColor = Color.Yellow;
                await Task.Delay(2000);
                yellowLight.BackgroundColor = Color.Gray;
                greenLight.BackgroundColor = Color.Green;
                await Task.Delay(3000);
                greenLight.BackgroundColor = Color.Gray;
                await Task.Delay(500);
                greenLight.BackgroundColor = Color.Green;
                await Task.Delay(500);
                greenLight.BackgroundColor = Color.Gray;
                await Task.Delay(500);
                greenLight.BackgroundColor = Color.Green;
                await Task.Delay(500);
                greenLight.BackgroundColor = Color.Gray;
                yellowLight.BackgroundColor = Color.Yellow;
                await Task.Delay(2000);
                yellowLight.BackgroundColor = Color.Gray;
            }
        }

        private async void TurnOnButton_Clicked(object sender, EventArgs e)
        {
            bl = false;
            this.BackgroundColor = Color.White;
            lb1.Text = "Punane";
            lb2.Text = "Kollane";
            lb3.Text = "Roheline";

            while (bl)
            {
                //this.BackgroundColor = Color.White;
                redLight.BackgroundColor = Color.Gray;
                await Task.Delay(100);
                yellowLight.BackgroundColor = Color.Gray;
                await Task.Delay(100);
                greenLight.BackgroundColor = Color.Gray;
                await Task.Delay(100);
            }
        }
    }
}
