using PruebaBackAPI.Data;
using PruebaBackAPI.Models;
using RestSharp;
using AutoMapper;
namespace PruebaBackAPI.Services;

public class GetUserService<T> : IHostedService, IDisposable  where T : User
{
    private int pageCounter;
    private Timer? _timer;
    private readonly IUserRepo<T> _repository;
    private readonly IMapper _mapper;

    public GetUserService(IServiceScopeFactory factory,  IMapper mapper)
    {
        pageCounter = 0;
        _mapper = mapper;
        _repository = factory.CreateScope().ServiceProvider.GetRequiredService<IUserRepo<T>>();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(SaveUsers, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
        return Task.CompletedTask;
    }

    public async void SaveUsers(object state)
    {
        pageCounter++;
        var client = new RestClient("https://reqres.in/api/users");
        var response = await client.GetJsonAsync<ReqUser>($"?page={pageCounter}");

        foreach (var i in response.data)
        {
            var userModel = _mapper.Map<T>(i);
            await _repository.CreateUser(userModel);
        }
        
        await _repository.SaveChanges();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
