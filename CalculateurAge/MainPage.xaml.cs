using System;
using CalculateurAge.Views;

namespace CalculateurAge;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnCalculerClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(entryNom.Text))
        {
            await DisplayAlertAsync(
                "Erreur",
                "Entrez un nom",
                "OK");

            return;
        }

        DateTime dateNaissance = pickerDate?.Date ?? DateTime.Today;

        if (dateNaissance.Date > DateTime.Today)
        {
            await DisplayAlertAsync(
                "Erreur",
                "La date de naissance ne peut pas être dans le futur.",
                "OK");

            return;
        }

        int age = DateTime.Today.Year - dateNaissance.Year;

        if (dateNaissance.Date > DateTime.Today.AddYears(-age))
        {
            age--;
        }

        await Shell.Current.GoToAsync(
            $"{nameof(ResultatPage)}?nom={Uri.EscapeDataString(entryNom.Text)}&age={age}");
    }
}