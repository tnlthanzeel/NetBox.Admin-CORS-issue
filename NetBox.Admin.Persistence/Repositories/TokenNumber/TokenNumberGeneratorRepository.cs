using NetBox.Admin.Core.Jobs;
using NetBox.Admin.Core.Jobs.Interfaces;
using NetBox.Admin.SharedKernal.Extensions;

namespace NetBox.Admin.Persistence.Repositories.TokenNumber;

sealed class TokenNumberGeneratorRepository : BaseRepository, ITokenNumberGeneratorRepository
{
    private readonly DbSet<TokenNumberMaster> _tokenTable;

    public TokenNumberGeneratorRepository(AppDbContext dbContext) : base(dbContext)
    {
        _tokenTable = _dbContext.Set<TokenNumberMaster>();
    }

    public TokenNumberMaster Add(TokenNumberMaster entity)
    {
        _tokenTable.Add(entity);
        return entity;
    }

    public async Task<TokenNumberMaster?> GetLatestToken()
    {
        var sriLankaDateTime = DateTimeOffset.UtcNow.GetLocalTime(AppConstants.SriLankaTimeZone).Date;

        DateOnly date = DateOnly.FromDateTime(sriLankaDateTime);

        var latestToken = await _tokenTable.AsTracking().Where(w => w.Date == date).FirstOrDefaultAsync();

        return latestToken;
    }
}
