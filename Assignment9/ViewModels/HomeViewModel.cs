using Assignment9.Models;
using SQLite;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Assignment9.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        private SQLiteAsyncConnection database;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<MusicModel> Items { get; } = new ObservableCollection<MusicModel>();
        public Command<MusicModel> AddToCartCMD { get; }
        public HomeViewModel()
        {
            database = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "music.db"));
            AddToCartCMD = new Command<MusicModel>(AddToCart);
            LoadData();
        }
        private async void LoadData()
        {
            var allMusic = await database.Table<MusicModel>().ToListAsync();
            foreach (var music in allMusic)
            {
                allMusic.Add(music);
            }
        }
        private async void AddToCart(MusicModel item)
        {
            if(item.StockQuantity > 0)
            {
                var cartItem = new CartModel { ProfileID = 1, MusicItemID = item.ID, Quantity = 1 };
                await database.InsertAsync(cartItem);
                item.StockQuantity--;
                await database.UpdateAsync(item);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Out of stock", "OK");
            }
        }
    }
}