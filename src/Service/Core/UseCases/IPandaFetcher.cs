using Service.Core.Models;

namespace Service.Core.UseCases
{
    public interface IPandaFetcher
    {
        Panda Execute(Guid pandaId);
    }
}
