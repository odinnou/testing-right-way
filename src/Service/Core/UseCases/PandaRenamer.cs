using Service.Core.Models;

namespace Service.Core.UseCases
{
    public class PandaRenamer
    {
        private readonly IPandaFetcher _pandaFetcher;

        public PandaRenamer(IPandaFetcher pandaFetcher)
        {
            _pandaFetcher = pandaFetcher;
        }

        public Panda Execute(Guid pandaId, string newName)
        {
            Panda panda = _pandaFetcher.Execute(pandaId);

            panda.Name = newName;

            return panda;
        }
    }
}
