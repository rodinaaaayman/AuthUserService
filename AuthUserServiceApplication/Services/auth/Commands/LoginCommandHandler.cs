using AuthUserServiceApplication.DTOs.Auth;
using AuthUserServiceApplication.Interfaces;
using AuthUserServiceDomain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthUserServiceApplication.Services.auth.Commands
{
    public class LoginCommandHandler
        : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IJwtService _jwt;

        public LoginCommandHandler(
            IApplicationDbContext context,
            IJwtService jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        public async Task<LoginResponse> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x =>
                    x.Email == request.Email, cancellationToken);

            if (user == null)
                throw new Exception("Invalid credentials.");

            if (user.Password != request.Password)
                throw new Exception("Invalid credentials.");

            var accessToken = _jwt.GenerateAccessToken(user);
            var refreshToken = _jwt.GenerateRefreshToken();

            // Check if a refresh token row already exists for this user
            var existingToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(x => x.Id == user.Id, cancellationToken);

            if (existingToken != null)
            {
                existingToken.RefreshToken = refreshToken;
                existingToken.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            }
            else
            {
                _context.RefreshTokens.Add(new RefreshTokens
                {
                    Id = user.Id,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7)
                });
            }

            await _context.SaveChangesAsync(cancellationToken);

            return new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}