using AutoMapper;
using PruebaBackAPI.Data;
using PruebaBackAPI.Models;
using RestSharp;

namespace PruebaBackAPI.Services;

public class GetUserService<T> : IHostedService, IDisposable where T : User
{
    private readonly IMapper _mapper;
    private readonly IRepository<T> _repository;
    private Timer? _timer;
    private int _pageCounter;

    public GetUserService(IServiceScopeFactory factory, IMapper mapper)
    {
        _pageCounter = 0;
        _mapper = mapper;
        _repository = factory.CreateScope().ServiceProvider.GetRequiredService<IRepository<T>>();
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(SaveUsers, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    private async void SaveUsers(object? state)
    {
        _pageCounter++;
        var client = new RestClient("https://reqres.in/api/users");
        var response = await client.GetJsonAsync<ReqUser>($"?page={_pageCounter}");

        foreach (var userModel in response?.Data!.Select(i => _mapper.Map<T>(i))!)
        {
            await _repository.CreateUser(userModel);
        }

        await _repository.SaveChanges();
    }
}