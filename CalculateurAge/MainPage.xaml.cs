namespace CalculateurAge;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnCalculerClicked(object? sender, EventArgs e)
    {
        // Vérification du nom
        if (string.IsNullOrWhiteSpace(entryNom.Text))
        {
            await DisplayAlertAsync(
                "Erreur",
                "Entrez un nom",
                "OK");

            return;
        }

        // Vérification de la date
        DateTime dateNaissance = pickerDate.Date ?? DateTime.Today;

        if (dateNaissance.Date > DateTime.Today)
        {
            await DisplayAlertAsync(
                "Erreur",
                "La date de naissance ne peut pas être dans le futur.",
                "OK");

            return;
        }

        // Calcul de l'âge
        int age = DateTime.Today.Year - dateNaissance.Year;

        // Si l'anniversaire n'est pas encore passé cette année
        if (dateNaissance.Date > DateTime.Today.AddYears(-age))
        {
            age--;
        }

        // Affichage du résultat
        lblResultat.Text =
            $"{entryNom.Text}, vous avez {age} ans";

        lblResultat.IsVisible = true;
    }
}