namespace SurveyApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnAgreeToggled(object sender, ToggledEventArgs e)
    {
        JumpButton.IsEnabled = e.Value;
    }

    private async void OnJumpClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameEntry.Text))
        {
            await DisplayAlertAsync("Ошибка", "Введите имя", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(BirthEntry.Text))
        {
            await DisplayAlertAsync("Ошибка", "Введите год рождения", "OK");
            return;
        }

        if (TargetPicker.SelectedIndex == -1)
        {
            await DisplayAlertAsync("Ошибка", "Выберите цель путешествия", "OK");
            return;
        }

        RadioButton selectedStatus = TouristRadio;
        if (RefugeeRadio.IsChecked)
            selectedStatus = RefugeeRadio;
        else if (InterfererRadio.IsChecked)
            selectedStatus = InterfererRadio;

        var readyForList = new List<string>();
        if (SelfCheck.IsChecked)
            readyForList.Add("Встрече с самим собой");
        if (RadCheck.IsChecked)
            readyForList.Add("Радиации");
        string readyForText = readyForList.Count > 0
            ? string.Join(", ", readyForList)
            : "Не выбрано";

        string paradox = string.IsNullOrWhiteSpace(ParadoxEditor.Text)
            ? "Нет"
            : ParadoxEditor.Text;

        ResultLabel.Text =
            $"Имя: {NameEntry.Text}\n" +
            $"Год рождения: {BirthEntry.Text}\n" +
            $"Созданный парадокс: {paradox}\n" +
            $"Цель путешествия: {TargetPicker.SelectedItem}\n" +
            $"Временной статус: {selectedStatus.Value}\n" +
            $"Готов к: {readyForText}\n" +
            $"Дата прибытия: {DatePicker.Date:dd.MM.yyyy}\n" +
            $"Уровень временной дезориентации: {DisorientationStepper.Value:F0}\n" +
            $"Согласие с созданием временных петель: {(AgreeSwitch.IsToggled ? "Да" : "Нет")}";

        ResultLabel.IsVisible = true;
    }

    private void OnClearClicked(object sender, EventArgs e)
    {
        NameEntry.Text = string.Empty;
        BirthEntry.Text = string.Empty;
        ParadoxEditor.Text = string.Empty;
        TargetPicker.SelectedIndex = -1;
        TouristRadio.IsChecked = true;
        SelfCheck.IsChecked = true;
        RadCheck.IsChecked = false;
        DatePicker.Date = new DateTime(2000, 01, 01);
        DisorientationStepper.Value = 5;
        AgreeSwitch.IsToggled = false;

        ResultLabel.Text = string.Empty;
        ResultLabel.IsVisible = false;
    }
}