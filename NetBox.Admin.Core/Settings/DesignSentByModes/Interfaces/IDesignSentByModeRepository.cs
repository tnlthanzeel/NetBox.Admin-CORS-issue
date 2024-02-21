namespace NetBox.Admin.Core.Settings.DesignSentByModes.Interfaces;

public interface IDesignSentByModeRepository : IBaseRepository
{
    DesignSentByMode Add(DesignSentByMode entity);

    Task<DesignSentByMode?> GetBySpec(ISpecification<DesignSentByMode> specification);

    Task<TResult?> GetProjectedEntityBySpec<TResult>(ISpecification<DesignSentByMode, TResult> specification, CancellationToken token);

    Task<(IReadOnlyList<TResult> list, int totalRecords)> GetProjectedListBySpec<TResult>(Paginator paginator,
                                                                                          ISpecification<DesignSentByMode, TResult> specification,
                                                                                          CancellationToken token);
    Task<IReadOnlyList<TResult>> GetProjectedListBySpec<TResult>(ISpecification<DesignSentByMode, TResult> specification, CancellationToken token);
}
