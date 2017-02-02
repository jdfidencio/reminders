using Reminders.Model;
using System;
using Xamarin.Forms;

namespace Reminders.Pages
{
    public class EditReminderPage : ContentPage
    {
        // styles
        readonly double FONTE_TEXTOS = Device.GetNamedSize(NamedSize.Large, typeof(Label));

        // variaveis
        bool pagCarregada = false;

        // componentes
        Entry edtTitulo;
        Entry edtDescricao;
        Entry edtDetalhes;
        DatePicker dtpkLimite;
        TimePicker tmpkLimite;
        Switch swtConcluido;
        
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (!pagCarregada)
            {
                pagCarregada = true;
                CriarPagina();
            }
        }

        private void CriarPagina()
        {
            /*var alturaTela = Height;
            var paddingTela = alturaTela * .05f;
            var alturaLogo = alturaTela * .5f;
            var alturaMarginTopTextos = alturaTela * .1f;
            var alguraTextos = alturaTela * .3f;*/

            //Padding = new Thickness(paddingTela);

            edtTitulo = new Entry
            {
                FontSize = FONTE_TEXTOS,
                Placeholder = "Título:",
            };
            edtTitulo.SetBinding(Entry.TextProperty, "Titulo");

            edtDescricao = new Entry
            {
                FontSize = FONTE_TEXTOS,
                Placeholder = "Descrição:",
            };
            edtDescricao.SetBinding(Entry.TextProperty, "Descricao");

            edtDetalhes = new Entry
            {
                FontSize = FONTE_TEXTOS,
                Placeholder = "Detalhes:",
            };
            edtDetalhes.SetBinding(Entry.TextProperty, "Detalhes");

            dtpkLimite = new DatePicker {};
            dtpkLimite.SetBinding(DatePicker.DateProperty, "DataLimite");

            tmpkLimite = new TimePicker { };
            tmpkLimite.SetBinding(TimePicker.TimeProperty, "HoraLimite");


            var lblSwt = new Label
            {
                FontSize = FONTE_TEXTOS,
                Text = "Concluído"
            };
            swtConcluido = new Switch {};
            swtConcluido.SetBinding(Switch.IsToggledProperty, "Completo");
            var swtContainer = new StackLayout {
                Orientation = StackOrientation.Horizontal,
                Children = {
                    lblSwt, swtConcluido
                }
            };

            var btnSave = new Button
            {
                Text = "Salvar",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BorderRadius = 20,
                FontSize = FONTE_TEXTOS,
            };
            btnSave.Clicked += BtnSave_Clicked;

            var btnRemove = new Button
            {
                Text = "Excluir",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BorderRadius = 20,
                FontSize = FONTE_TEXTOS,
                IsVisible = ((Reminder)BindingContext).IdReminder != 0 // so mostra se for edicao
            };
            btnRemove.Clicked += BtnRemove_Clicked; ;

            var containerBtn = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Padding = new Thickness(40,0),
                Children = {
                    btnSave, btnRemove
                }
            };

            // set conteudo da página
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Spacing = 20,
                Children = {
                   edtTitulo, edtDescricao, edtDetalhes, swtContainer, dtpkLimite, tmpkLimite, containerBtn
                }
            };
        }

        private async void BtnRemove_Clicked(object sender, EventArgs e)
        {
            var reminder = (Reminder)BindingContext;
            await App.SingletonRemindersDB.DeleteItemAsync(reminder);
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            var reminder = (Reminder)BindingContext;
            if (reminder.IdReminder == 0)
                await App.SingletonRemindersDB.InsertItemAsync(reminder);
            else
                await App.SingletonRemindersDB.UpdateItemAsync(reminder);

            AgendarReminder(reminder);
        }

        private void AgendarReminder(Reminder reminder)
        {
            //se ja tinha data agendada, cancela
            Plugin.LocalNotifications.CrossLocalNotifications.Current.Cancel(reminder.IdReminder);
            
            //agenda
            Plugin.LocalNotifications.CrossLocalNotifications.Current.Show(reminder.Titulo, reminder.Descricao, reminder.IdReminder, reminder.DataHoraLimite);
        }
    }
}
