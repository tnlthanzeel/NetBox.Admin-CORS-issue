using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetBox.Admin.SharedKernal.Extensions;
using NetBox.Admin.SharedKernal.Interfaces;
using System;

namespace NetBox.Admin.Core.Jobs;
public sealed class TokenNumberMaster : EntityBase, ICreatedAudit
{
    private const string Prefix = "N";

    public DateTimeOffset CreatedOn { get; set; }
    public string? CreatedBy { get; set; }

    public DateOnly Date { get; private set; }

    public string TokenNumber { get; private set; } = null!;


    public TokenNumberMaster()
    {
        var sriLankaDateTime = DateTimeOffset.UtcNow.GetLocalTime(AppConstants.SriLankaTimeZone).Date;
        Date = DateOnly.FromDateTime(sriLankaDateTime);
        SetFormatedTokenNumber(1);
    }

    internal void GetNext()
    {
        var numberOnly = int.Parse(TokenNumber.Split("-")[1]);

        ++numberOnly;

        SetFormatedTokenNumber(numberOnly);
    }

    private void SetFormatedTokenNumber(int numberOnly)
    {
        TokenNumber = $"{Prefix}-{numberOnly}";
    }
}
