namespace Assignment9.Views;

public partial class InitialPage : ContentPage
{
	public InitialPage()
	{
		InitializeComponent();
	}
	private async void NavToHome(object obj, EventArgs args)
	{
		await Navigation.PushAsync(new HomePage());
	}
}