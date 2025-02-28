using System.ComponentModel;
using System.Runtime.CompilerServices;
using Assignment9.Models;
using SQLite;

namespace Assignment9.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private SQLiteAsyncConnection database;

        private ProfileModel _profile;
        public ProfileModel Profile
        {
            get => _profile;
            set
            {
                _profile = value;
                OnPropertyChanged();
            }
        }
        public Command OnSaveCMD { get; }
        public ProfileViewModel()
        {
            database = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "shoppingapp.db"));
            database.CreateTableAsync<ProfileModel>().Wait();
            LoadProfileData();

            OnSaveCMD = new Command(OnSaveProfile);
        }
        private async void LoadProfileData()
        {
            Profile = await database.Table<ProfileModel>().FirstOrDefaultAsync() ?? new ProfileModel();
        }
        private async void OnSaveProfile()
        {
            await database.InsertOrReplaceAsync(Profile);
            await Application.Current.MainPage.DisplayAlert("Success", "Profile saved!", "OK");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}