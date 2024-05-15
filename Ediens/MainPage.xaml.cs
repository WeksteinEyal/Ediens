using Microsoft.Maui.Controls;

namespace Ediens
{
    public partial class MainPage : ContentPage
    {
        float dailyIntake = 0;
        string strDailyIntake = "0";

        public MainPage()
        {
            InitializeComponent();
            strDailyIntake = Preferences.Default.Get("DailyIntake", "0");
            DailyHistory.Text = Preferences.Default.Get("DailyHistory", "");
            dailyIntake = float.Parse(strDailyIntake);
            labelDailyIntake.Text = $"Daily Intake: {dailyIntake} kcal";
        }

        void OnEntryFoodChanged(object sender, TextChangedEventArgs e)
        {
            string oldText = e.OldTextValue;
            string newText = e.NewTextValue;
            string myText = entryFood.Text;
        }

        void AddDailyIntake(object sender, EventArgs e)
        {
            string foodName = entryFood.Text;
            float foodQty = float.Parse(entryQty.Text);
            float foodKcal = float.Parse(entryKcal.Text);

            float intake = foodQty * foodKcal / 100;
            dailyIntake += intake;
            labelDailyIntake.Text = $"Daily Intake: {dailyIntake} kcal";
            DailyHistory.Text = $"{DailyHistory.Text}\n{foodName}, {foodQty}g : {intake}kcal";
            Preferences.Default.Set("DailyIntake", dailyIntake.ToString());
            Preferences.Default.Set("DailyHistory", DailyHistory.Text);
        }


    }

}
