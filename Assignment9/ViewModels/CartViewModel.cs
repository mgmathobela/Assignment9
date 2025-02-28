using SQLite;
using Assignment9.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Assignment9.ViewModels
{
    public class CartViewModel : INotifyPropertyChanged
    {
        private SQLiteAsyncConnection database;
        public ObservableCollection<CartModel> CartItems { get; set; } = new ObservableCollection<CartModel>();
        public Command<CartModel> RemoveItemCMD { get; }
        public CartViewModel()
        {
            database = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "music.db"));
            LoadCartData();
            RemoveItemCMD = new Command<CartModel>(OnRemoveItem);
        }
        private async void LoadCartData()
        {
            var cartItems = await database.Table<CartModel>().ToListAsync();
            foreach(var cartItem in cartItems)
            {
                var item = await database.Table<MusicModel>().FirstOrDefaultAsync(music => music.ID == cartItem.MusicItemID);
                cartItem.Add(new CartModel
                {
                    CartItemTitle = cartItem,
                    CartItemArtist = item
                });
            }
        }
        private async void OnRemoveItem(CartModel cartItem)
        {
            await database.DeleteAsync(cartItem.CartItemTitle);
            CartItems.Remove(cartItem);
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}