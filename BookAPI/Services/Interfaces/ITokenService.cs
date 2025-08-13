using BookAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace BookAPI.Services.Interfaces;

public interface ITokenService
{
    (string Token, SecurityTokenDescriptor Descriptor) GenerateToken(User user);
}