using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using System;

namespace Ediens
{
    public partial class MainPage : ContentPage
    {
        float dailyIntake = 0;
        string strDailyIntake = "0";
        DateTime lastDate;

        public MainPage()
        {
            InitializeComponent();
            strDailyIntake = Preferences.Default.Get("DailyIntake", "0");
            DailyHistory.Text = Preferences.Default.Get("DailyHistory", "");
            dailyIntake = float.Parse(strDailyIntake);

            lastDate = DateTime.Parse(Preferences.Default.Get("LastDate", DateTime.MinValue.ToString()));
            StartTimer();

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

        async void StartTimer()
        {
            while (true)
            {
                // Calculate delay until the start of next minute
                DateTime now = DateTime.Now;
                int delayMilliseconds = (60 - now.Second) * 1000 - now.Millisecond;

                await Task.Delay(delayMilliseconds);

                // Check if it's a new day
                if (DateTime.Today > lastDate)
                {
                    // Clear preferences and update lastDate
                    Preferences.Default.Clear();
                    lastDate = DateTime.Today;
                    Preferences.Default.Set("LastDate", lastDate.ToString());

                    // Reset UI
                    dailyIntake = 0;
                    DailyHistory.Text = "";
                    labelDailyIntake.Text = $"Daily Intake: {dailyIntake} kcal";
                }
            }
        }


    }

}
