using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Timers;
using Testing.Models;
using Testing.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Testing.Views
{
    public partial class AboutPage : ContentPage
    {
        System.Timers.Timer timer;
        int secondsCounter = 0;
        int pointsCounter = 0;
        int currentAns = 0;
        Dictionary<string, int> additions = new Dictionary<string, int>();
        Dictionary<string, int> multiplications = new Dictionary<string, int>();
        Dictionary<string, int> deductions = new Dictionary<string, int>();
        Dictionary<string, int> divisions = new Dictionary<string, int>();
        Dictionary<string, int> operations = new Dictionary<string, int>();
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();
        public AboutPage()
        {
            InitializeComponent();
            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;

            //set default values
            FirstMinRange.Text = "2";
            FirstMaxRange.Text = "100";
            SecondMinRange.Text = "2";
            SecondMaxRange.Text = "100";
            MFirstMinRange.Text = "2";
            MFirstMaxRange.Text = "12";
            MSecondMinRange.Text = "2";
            MSecondMaxRange.Text = "100";
            useAddition.IsChecked = true;
            useDeduction.IsChecked = true;
            useMultiplication.IsChecked = true;
            useDivision.IsChecked = true;
            duration.SelectedIndex = 3;
            GetTables();
        }

        private void GetTables()
        {

            //create operations tables depending on the settings on the main page.
            additions.Clear();
            deductions.Clear();
            multiplications.Clear();
            divisions.Clear();
            operations.Clear();

            int firstMinRange = int.Parse(FirstMinRange.Text);
            int firstMaxRange = int.Parse(FirstMaxRange.Text);
            int secondMinRange = int.Parse(SecondMinRange.Text);
            int secondMaxRange = int.Parse(SecondMaxRange.Text);

            int mfirstMinRange = int.Parse(MFirstMinRange.Text);
            int mfirstMaxRange = int.Parse(MFirstMaxRange.Text);
            int msecondMinRange = int.Parse(MSecondMinRange.Text);
            int msecondMaxRange = int.Parse(MSecondMaxRange.Text);
            for (int i = firstMinRange; i <= firstMaxRange; i++)
            {
                for (int j = secondMinRange; j <= secondMaxRange; j++)
                {
                    int addresult = i + j;
                    int multiplicresult = i * j;

                    if (useAddition.IsChecked)
                    {
                        additions.Add(i.ToString() + " + " + j.ToString() + " = ?", addresult);
                    }
                    Random selecter = new Random();
                    int selectInt = selecter.Next(3);

                    if (useDeduction.IsChecked && selectInt == 0 && !deductions.ContainsKey(addresult.ToString() + " - " + j.ToString() + " = ?"))
                        deductions.Add(addresult.ToString() + " - " + j.ToString() + " = ?", i);

                    if (useDeduction.IsChecked && selectInt == 1 && !deductions.ContainsKey(addresult.ToString() + " - " + i.ToString() + " = ?"))
                        deductions.Add(addresult.ToString() + " - " + i.ToString() + " = ?", j);

                }
            }

            for (int i = mfirstMinRange; i <= mfirstMaxRange; i++)
            {
                for (int j = msecondMinRange; j <= msecondMaxRange; j++)
                {
                    int addresult = i + j;
                    int multiplicresult = i * j;


                    if (useMultiplication.IsChecked)
                    {
                        multiplications.Add(i.ToString() + " x " + j.ToString() + " = ?", multiplicresult);
                    }

                    Random selecter = new Random();
                    int selectInt = selecter.Next(3);

                    if (useDivision.IsChecked && selectInt == 0 && !divisions.ContainsKey(multiplicresult.ToString() + " / " + j.ToString() + " = ?"))
                        divisions.Add(multiplicresult.ToString() + " / " + j.ToString() + " = ?", i);

                    if (useDivision.IsChecked && selectInt == 1 && !divisions.ContainsKey(multiplicresult.ToString() + " / " + i.ToString() + " = ?"))
                        divisions.Add(multiplicresult.ToString() + " / " + i.ToString() + " = ?", j);

                }
            }

            additions.ToList().ForEach(x => operations.Add(x.Key, x.Value));
            deductions.ToList().ForEach(x => operations.Add(x.Key, x.Value));
            multiplications.ToList().ForEach(x => operations.Add(x.Key, x.Value));
            divisions.ToList().ForEach(x => operations.Add(x.Key, x.Value));

            Random random = new Random();
            int index = random.Next(0, operations.Count);

            question.Text = operations.ElementAt(index).Key;
            currentAns = operations.ElementAt(index).Value;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {

            Device.BeginInvokeOnMainThread(() => {
                secondsCounter++;
                seconds.Text = secondsCounter.ToString();
                int selectedDuration = int.Parse(duration.SelectedItem.ToString());
                if (secondsCounter >= selectedDuration)
                {
                    timer.Stop();
                    answer.IsReadOnly = true;
                    Item item = new Item();
                    item.Name = Name.Text;
                    item.TimeStamp = DateTime.Now;
                    item.Text = question.Text;
                    item.Description = "Answered " + pointsCounter + " questions within " + secondsCounter + " seconds on " + item.TimeStamp.ToLongDateString() + " at " + item.TimeStamp.ToLongTimeString() + ".";
                    item.Score = pointsCounter;
                    item.Durations = secondsCounter;
                    item.Addition = useAddition.IsChecked;
                    item.Deduction = useDeduction.IsChecked;
                    item.Multiplication = useMultiplication.IsChecked;
                    item.Division = useDivision.IsChecked;

                    item.Addition1stLowerLimit = int.Parse(FirstMinRange.Text);
                    item.Addition1stUpperLimit = int.Parse(FirstMaxRange.Text);
                    item.Addition2ndLowerLimit = int.Parse(SecondMinRange.Text);
                    item.Addition2ndUpperLimit = int.Parse(SecondMaxRange.Text);


                    item.Multiplication1stLowerLimit = int.Parse(MFirstMinRange.Text);
                    item.Multiplication1stUpperLimit = int.Parse(MFirstMaxRange.Text);
                    item.Multiplication2ndLowerLimit = int.Parse(MSecondMinRange.Text);
                    item.Multiplication2ndUpperLimit = int.Parse(MSecondMaxRange.Text);


                    DataStore.AddItemAsync(item);
                }
            });

        }
        private void answer_TextChanged(object sender, TextChangedEventArgs e)
        {
            //check anwers input by user and fetch new questions if answer is correct.
            if (e.NewTextValue == currentAns.ToString())
            {
                Random random = new Random();
                int index = random.Next(0, operations.Count);

                question.Text = operations.ElementAt(index).Key;
                currentAns = operations.ElementAt(index).Value;
                pointsCounter++;
                points.Text = pointsCounter.ToString();
                answer.Text = "";
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(Name.Text))
            {
                DisplayAlert("Alert", "Please input your name", "OK");
                return;
            }
            

            //start timer 
            if (timer.Enabled)
            { timer.Stop(); }

            timer.Start();

            pointsCounter = 0;
            secondsCounter = 0;
            seconds.Text = secondsCounter.ToString();
            points.Text = pointsCounter.ToString();
            answer.Text = "";
            answer.IsReadOnly = false;

            GetTables();
            answer.Focus();
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            //stop timer 
            if (timer.Enabled)
            { timer.Stop(); }

            answer.Text = "";
            answer.IsReadOnly = true;
        }
    }
}