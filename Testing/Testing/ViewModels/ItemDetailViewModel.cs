using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Testing.Models;
using Xamarin.Forms;

namespace Testing.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string name;
        private string itemId;
        private string text;
        private string description;
        private DateTime timeStamp;
        private int score;
        public string Id { get; set; }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public DateTime TimeStamp
        {
            get => timeStamp;
            set => SetProperty(ref timeStamp, value);
        }

        public int Score
        {
            get => score;
            set => SetProperty(ref score, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Name = item.Name;
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
                Score = item.Score;
                TimeStamp = item.TimeStamp;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
