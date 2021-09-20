using System.Threading.Tasks;
using MarketingBox.AuthApi.Domain.Models.Errors;

namespace MarketingBox.AuthApi.Domain.Tokens
{
    public interface ITokensService
    {
        Task<(TokenInfo Token, LoginError Error)> LoginAsync(string login, string tenantId, string password);
    }
}