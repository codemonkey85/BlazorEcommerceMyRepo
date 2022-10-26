namespace BlazorECommerce.Rcl.Pages;

public partial class FetchData
{
    private WeatherForecast[]? _forecasts;

    protected override async Task OnInitializedAsync() =>
        _forecasts = await Http.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");
}
