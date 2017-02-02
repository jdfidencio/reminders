using Reminders.Model;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Reminders.Pages
{
    public class RemindersListPage : ContentPage
    {
        //componentes
        ListView _listView;
        SearchBar _searchBar;

        public RemindersListPage()
        {
            CriarPagina();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //carrega depois que ja construiu o listview
            _listView.ItemsSource = await App.SingletonRemindersDB.GetItemsAsync();
        }

        private void CriarPagina()
        {

            _searchBar = new SearchBar
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(SearchBar)),
                Placeholder = "Filtro..",
                SearchCommand = new Command(async (e) =>
                {
                    _listView.ItemsSource = await App.SingletonRemindersDB.GetItemsSelectedAsync(_searchBar.Text);
                })
            };

            var containerFiltro = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Children = {
                    _searchBar
                }
            };

            var orderPicker = new Picker
            {
                Title = "Ordem.."
            };
            orderPicker.SelectedIndexChanged += async (sender, args) => 
            {
                if (orderPicker.SelectedIndex != -1)
                {
                    string nomeColunaOrdem = orderPicker.Items[orderPicker.SelectedIndex];
                    await App.SingletonRemindersDB.GetItemsOrderedAsync(nomeColunaOrdem);
                }
            };
            orderPicker.Items.Add("Título");
            orderPicker.Items.Add("Data Limite");

            var containerOrdem = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Spacing = 20,
                Children = {
                    new Label {
                        FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                        Text = "Ordenar por:"
                    },
                   orderPicker
                }
            };


            var btnAdd = new Button
            {
                HorizontalOptions = LayoutOptions.End,
                Text = "Add",
            };
            btnAdd.Clicked += BtnAdd_Clicked;

            var containerButtons = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                Children = {
                   btnAdd
                }
            };

            var header = new { lbltitulo = "Titulo", lbldata ="Data Limite", lblcompleto = "Completo"};

            _listView = new ListView
            {
                Header = header,
                HeaderTemplate = new DataTemplate(() =>
                {
                    Label tituloLabel = new Label();
                    tituloLabel.HorizontalOptions = LayoutOptions.StartAndExpand;
                    tituloLabel.SetBinding(Label.TextProperty, "lbltitulo");

                    Label dataLabel = new Label();
                    dataLabel.HorizontalOptions = LayoutOptions.FillAndExpand;
                    dataLabel.SetBinding(Label.TextProperty, "lbldata");

                    Label boxView = new Label();
                    boxView.HorizontalOptions = LayoutOptions.End;
                    boxView.SetBinding(Label.TextProperty, "lblcompleto");

                    // Return an assembled ViewCell.
                    return new StackLayout
                    {
                        Padding = new Thickness(0, 5),
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Orientation = StackOrientation.Horizontal,
                        Children =
                                    {
                                        tituloLabel,
                                        dataLabel,
                                        boxView
                                    }
                    };
                }),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                ItemTemplate = new DataTemplate(() =>
                {
                    Label tituloLabel = new Label();
                    tituloLabel.SetBinding(Label.TextProperty, "Titulo");

                    Label dataLabel = new Label();
                    dataLabel.SetBinding(Label.TextProperty, "DataHoraLimite");

                    Switch boxView = new Switch();
                    boxView.SetBinding(Switch.IsToggledProperty, "Completo");
                    var gridLayout = new Grid
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        ColumnDefinitions = new ColumnDefinitionCollection() {
                                   new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) },
                                   new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) },
                                   new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) },
                               },
                        RowDefinitions = new RowDefinitionCollection() {
                                   new RowDefinition() { Height = GridLength.Auto }
                               }
                    };

                    gridLayout.Children.Add(tituloLabel, 0, 0);
                    gridLayout.Children.Add(dataLabel, 1, 0);
                    gridLayout.Children.Add(boxView, 2, 0);

                    return new ViewCell
                    {
                        View = gridLayout
                    };
                })
            };
            _listView.ItemSelected += async (sender, e) =>
            {
                await Navigation.PushAsync(new EditReminderPage
                {
                    BindingContext = e.SelectedItem as Reminder
                });
            };

            var containerList = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Children = {
                    _listView
               }
           };

           Content = new StackLayout
           {
               Orientation = StackOrientation.Vertical,
               HorizontalOptions = LayoutOptions.Center,
               VerticalOptions = LayoutOptions.Center,
               Padding = 20,
               Children = {
                   containerFiltro, containerOrdem, containerButtons, containerList
               }
           };
       }

        private void BtnAdd_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditReminderPage() { BindingContext = new Reminder() });
        }
    }
}
