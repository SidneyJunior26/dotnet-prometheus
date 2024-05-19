using Microsoft.AspNetCore.Mvc;
using Prometheus;
using System.Diagnostics;
using web_metrics.Models;

namespace web_metrics.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private static readonly Counter PageRequestsCounter = Metrics.CreateCounter("page_requests_total", "Número total de requisições para a página");
    private static readonly Histogram HttpRequestDuration = Metrics.CreateHistogram("http_request_duration_seconds_openweathermap", "Tempo de resposta das requisições HTTP");
    private static readonly Counter WeatherRequestErrorsCounter = Metrics.CreateCounter("weather_request_errors_total", "Número total de erros ao obter dados do clima");
    private static readonly Counter RequestErrorsCounter = Metrics.CreateCounter("request_errors_total", "Número total de erros na requisição");
    
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        PageRequestsCounter.Inc();

        using (var client = new HttpClient()) {
            try {
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();

                var response = await client.GetAsync("https://api.openweathermap.org/data/2.5/weather?q=London&appid=30fa42f5dde99dcfe9ece8fa5f00b70");

                stopwatch.Stop();
                HttpRequestDuration.Observe(stopwatch.Elapsed.TotalSeconds);

                if (response.IsSuccessStatusCode) {
                    var content = await response.Content.ReadAsStringAsync();
                    ViewBag.WeatherData = content;
                } else {
                    ViewBag.ErrorMessage = "Erro ao obter dados do clima";
                    WeatherRequestErrorsCounter.Inc();
                }
            } catch (Exception ex) {
                ViewBag.ErrorMessage = $"Erro na requisição: {ex.Message}";
                RequestErrorsCounter.Inc();
            }
        }

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
