using AuthUserServiceApplication.Interfaces;
using AuthUserServiceApplication.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using AuthUserServiceApplication.Exceptions;


namespace AuthUserServiceApplication.Services.clients.Queries.GetClientById;


public class GetClientByIdQueryHandler
    : IRequestHandler<GetClientByIdQuery, ClientsDTO?>
{

    private readonly IApplicationDbContext _context;
    private readonly IMemoryCache _cache;
    private readonly ICurrentUserService _currentUser;


    public GetClientByIdQueryHandler(IApplicationDbContext context, IMemoryCache cache, ICurrentUserService currentUser)
    {
        _context = context;
        _cache = cache;
        _currentUser = currentUser;
    }


    //public async Task<ClientsDTO?> Handle(
    //    GetClientByIdQuery request,
    //    CancellationToken cancellationToken)
    //{
    //    var cacheKey = $"client_{request.Id}";

    //    if (_cache.TryGetValue(cacheKey, out ClientsDTO? cachedClient))
    //    {
    //        return cachedClient;
    //    }

    //    var client = await _context.Clients
    //        .Where(c => c.Id == request.Id && c.IsActive)
    //        .Select(c => new ClientsDTO
    //        {
    //            Username = c.Username,
    //            Id = c.Id,
    //            Name = c.Name,
    //            Email = c.Email,
    //            NationalID = c.NationalID,
    //            PhoneNumber = c.PhoneNumber,
    //            AccountBalance = c.AccountBalance
    //        })
    //        .FirstOrDefaultAsync(cancellationToken);

    //    if (client == null)
    //    {
    //        return null;
    //    }

    //    _cache.Set(
    //   cacheKey,
    //   client,
    //   new MemoryCacheEntryOptions
    //   {
    //       SlidingExpiration = TimeSpan.FromMinutes(5)
    //   });

    //    return client;
    //}
    public async Task<ClientsDTO?> Handle(
    GetClientByIdQuery request,
    CancellationToken cancellationToken)
    {

        if (!_currentUser.IsAdmin &&
            request.Id != _currentUser.Id)
        {
            throw new ForbiddenException();
        }


        var cacheKey = $"client_{request.Id}";

        if (_cache.TryGetValue(cacheKey, out ClientsDTO? cachedClient))
        {
            return cachedClient;
        }


        var client = await _context.Clients
            .Where(c => c.Id == request.Id && c.IsActive)
            .Select(c => new ClientsDTO
            {
                Username = c.Username,
                Id = c.Id,
                Name = c.FirstName,
                Email = c.Email,
                NationalID = c.NationalId,
                PhoneNumber = c.PhoneNumber,
                AccountBalance = c.AccountBalance
            })
            .FirstOrDefaultAsync(cancellationToken);


        if (client == null)
        {
            return null;
        }


        _cache.Set(
            cacheKey,
            client,
            new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(5)
            });


        return client;
    }
}