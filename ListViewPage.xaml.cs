using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace naidis_TARge25

{
    public class Telefon
    {
        public string Nimetus { get; set; }
        public string Tootja { get; set; }
        public int Hind { get; set; }
        public string Pilt { get; set; }
    }
    public partial class ListViewPage : ContentPage
    {
        ListView list;
        ObservableCollection<Telefon> telefonid;
        Entry entryNimetus, entrytootja, entryHind, entryPilt;

        public ListViewPage()
        {
            // Konstruktoris andmete algväärtustamine
            telefonid = new ObservableCollection<Telefon>
                {
            new Telefon { Nimetus="Samsung Galaxy S22 Ultra", Tootja="Samsung", Hind=1349, Pilt="Galaxy.png" },
            new Telefon { Nimetus="Xiaomi Mi 11 Lite 5G NE", Tootja="Xiaomi", Hind=399, Pilt="Xiaomi5GNE.png" },
            new Telefon { Nimetus="iPhone 13 mini", Tootja="Apple", Hind=1179, Pilt="iPhone13.png" }
            };

            ListView list = new ListView
            {
                HasUnevenRows = true, // Lubab ridadel olla erineva kõrgusega
                ItemsSource = telefonid,
                ItemTemplate = new DataTemplate(() =>
                {
                    Label nimetus = new Label { FontSize = 20 };
                    nimetus.SetBinding(Label.TextProperty, "Nimetus"); // Seome klassi omadusega "Nimetus"

                    Label hind = new Label();
                    hind.SetBinding(Label.TextProperty, "Hind");

                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Vertical,
                            Children = { nimetus, hind }
                        }
                    };
                })
            };
            // Seome sündmuse ListView-ga
            list.ItemTapped += List_ItemTapped;

            Button btnkustuta = new Button
            {
                Text = "Kustuta valitud",
                BackgroundColor = Colors.HotPink,
                TextColor = Colors.Plum
            };
            btnkustuta.Clicked += Btnkustuta_Clicked;
            entryNimetus = new Entry { Placeholder = "Nimetus" };
            //entrytootja = new Entry { Placeholder = "Tootja" };
            entryHind = new Entry { Placeholder = "Hind" };
            //entryPilt = new Entry { Placeholder = "Pilt" };

            Content = new StackLayout
            {
                Children =
                {
                    list,
                    btnkustuta
                }
            };

        }

        private async void Btnkustuta_Clicked(object? sender, EventArgs e)
        {
            Telefon phone = list.SelectedItem as Telefon;

            if (phone != null)
            {
                telefonid.Remove(phone);
                list.SelectedItem = null; // tühistame valiku visuaalselt
            }
             else
            {
                await DisplayAlertAsync("Viga", "Palun vali nimekirjast telefon", "OK");
            }
        }

        // Sündmuse töötleja (Event handler)
        private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // Konverteerime valitud elemendi (e.Item) Telefon objektiks
            Telefon selectedPhone = e.Item as Telefon;

            // Kontrollime alati, kas konverteerimine õnnestus ega poleks null
            if (selectedPhone != null)
            {
                // Kuvame ekraanil hüpikakna
                await DisplayAlert("Valitud mudel", $"{selectedPhone.Tootja} - {selectedPhone.Nimetus}", "OK");
            }
           
        }
    }
}