using NetBox.Admin.Core.Jobs.Interfaces;

namespace NetBox.Admin.Core.Jobs.Services;

sealed class TokenNumberGenerator : ITokenNumberGenerator
{
    private readonly ITokenNumberGeneratorRepository _tokenNumberGeneratorRepository;

    public TokenNumberGenerator(ITokenNumberGeneratorRepository tokenNumberGeneratorRepository)
    {
        _tokenNumberGeneratorRepository = tokenNumberGeneratorRepository;
    }

    public async Task<TokenNumberMaster> GetNextTokenNumber()
    {
        var token = await _tokenNumberGeneratorRepository.GetLatestToken();

        if (token is null) return GenerateNewTokenForCurrentDay();

        token.GetNext();

        return token;

        TokenNumberMaster GenerateNewTokenForCurrentDay()
        {
            TokenNumberMaster newToken = new();
            _tokenNumberGeneratorRepository.Add(newToken);
            return newToken;
        }
    }
}
