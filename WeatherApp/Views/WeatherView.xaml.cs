using Microsoft.Maui.Controls;
using WeatherApp.ViewModels;

namespace WeatherApp.Views;

public partial class WeatherView : ContentPage
{
    private WeatherViewModel _viewModel;

    public WeatherView()
    {
        InitializeComponent();
        _viewModel = new WeatherViewModel();
        BindingContext = _viewModel;
    }

    private void OnSearchCompleted(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(_viewModel.City) && _viewModel.SearchCommand.CanExecute(null))
            _viewModel.SearchCommand.Execute(null);
    }
}
