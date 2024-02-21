
namespace NetBox.Admin.Core.Jobs.Interfaces;

internal interface ITokenNumberGenerator
{
    Task<TokenNumberMaster> GetNextTokenNumber();
}
