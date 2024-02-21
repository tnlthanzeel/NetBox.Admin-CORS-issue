
namespace NetBox.Admin.Core.Jobs.Interfaces;

public interface ITokenNumberGeneratorRepository : IBaseRepository
{
    TokenNumberMaster Add(TokenNumberMaster entity);
    Task<TokenNumberMaster?> GetLatestToken();
}
