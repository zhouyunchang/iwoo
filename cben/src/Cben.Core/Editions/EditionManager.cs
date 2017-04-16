using Cben.Application.Editions;
using Cben.Application.Features;
using Cben.Domain.Repositories;

namespace Cben.Core.Editions
{
    public class EditionManager : CbenEditionManager
    {
        public const string DefaultEditionName = "Standard";

        public EditionManager(
            IRepository<Edition> editionRepository, 
            ICbenZeroFeatureValueStore featureValueStore)
            : base(
                editionRepository,
                featureValueStore
            )
        {
        }
    }
}
