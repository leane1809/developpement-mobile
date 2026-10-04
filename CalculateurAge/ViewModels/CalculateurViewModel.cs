namespace CalculateurAge.ViewModels;

public class CalculateurViewModel : BaseViewModel
{
    private string _nom = string.Empty;

    private DateTime _dateNaissance =
        DateTime.Today.AddYears(-20);

    private string _resultat = string.Empty;

    private string _message = string.Empty;

    private string _joursAvantAnniversaire =
        string.Empty;

    private bool _resultatVisible;

    private bool _messageVisible;

    private bool _joursVisible;

    public string Nom
    {
        get => _nom;

        set
        {
            if (SetField(ref _nom, value))
            {
                CalculerCommand.Rafraichir();
            }
        }
    }

    public DateTime DateNaissance
    {
        get => _dateNaissance;

        set
        {
            if (SetField(
                ref _dateNaissance,
                value))
            {
                CalculerCommand.Rafraichir();
            }
        }
    }

    public string Resultat
    {
        get => _resultat;

        set => SetField(
            ref _resultat,
            value);
    }

    public string Message
    {
        get => _message;

        set => SetField(
            ref _message,
            value);
    }

    public string JoursAvantAnniversaire
    {
        get => _joursAvantAnniversaire;

        set => SetField(
            ref _joursAvantAnniversaire,
            value);
    }

    public bool ResultatVisible
    {
        get => _resultatVisible;

        set => SetField(
            ref _resultatVisible,
            value);
    }

    public bool MessageVisible
    {
        get => _messageVisible;

        set => SetField(
            ref _messageVisible,
            value);
    }

    public bool JoursVisible
    {
        get => _joursVisible;

        set => SetField(
            ref _joursVisible,
            value);
    }

    public RelayCommand CalculerCommand { get; }

    public RelayCommand EffacerCommand { get; }

    public CalculateurViewModel()
    {
        CalculerCommand = new RelayCommand(
            Calculer,
            PeutCalculer);

        EffacerCommand = new RelayCommand(
            Effacer);
    }

    private bool PeutCalculer()
    {
        return !string.IsNullOrWhiteSpace(Nom)
               && DateNaissance.Date <= DateTime.Today;
    }

    private void Calculer()
    {
        ResultatVisible = false;
        MessageVisible = false;
        JoursVisible = false;

        // Refus d'une date future
        if (DateNaissance.Date > DateTime.Today)
        {
            Message =
                "La date de naissance ne peut pas être dans le futur.";

            MessageVisible = true;

            return;
        }

        // Calcul de l'âge
        int age =
            DateTime.Today.Year -
            DateNaissance.Year;

        if (DateNaissance.Date >
            DateTime.Today.AddYears(-age))
        {
            age--;
        }

        Resultat =
            $"{Nom}, vous avez {age} ans";

        ResultatVisible = true;

        // Majeur ou mineur
        if (age >= 18)
        {
            Message = "Majeur";
        }
        else
        {
            Message = "Mineur";
        }

        MessageVisible = true;

        // Calcul du prochain anniversaire
        DateTime prochainAnniversaire =
            new DateTime(
                DateTime.Today.Year,
                DateNaissance.Month,
                DateNaissance.Day);

        if (prochainAnniversaire.Date <
            DateTime.Today)
        {
            prochainAnniversaire =
                prochainAnniversaire.AddYears(1);
        }

        int jours =
            (prochainAnniversaire.Date -
             DateTime.Today).Days;

        JoursAvantAnniversaire =
            $"Prochain anniversaire dans {jours} jour(s).";

        JoursVisible = true;
    }

    private void Effacer()
    {
        Nom = string.Empty;

        DateNaissance =
            DateTime.Today.AddYears(-20);

        Resultat = string.Empty;

        Message = string.Empty;

        JoursAvantAnniversaire = string.Empty;

        ResultatVisible = false;

        MessageVisible = false;

        JoursVisible = false;

        CalculerCommand.Rafraichir();
    }
}