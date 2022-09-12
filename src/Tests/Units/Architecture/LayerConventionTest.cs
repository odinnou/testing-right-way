using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using Service.Core.UseCases;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Tests.Units.Architecture
{
    public class LayerConventionTest
    {
        private static readonly ArchUnitNET.Domain.Architecture Architecture = new ArchLoader().LoadAssemblies(typeof(PandaRenamer).Assembly).Build();
        private readonly IObjectProvider<IType> _coreLayer = Types().That().ResideInNamespace("Service.Core.*", useRegularExpressions: true).As("Core layer");
        private readonly IObjectProvider<IType> _mordorLayer = Types().That().ResideInNamespace("Service.Mordor.*", useRegularExpressions: true).As("Mordor layer");

        [Fact]
        public void Types_that_resides_in_Core_layer_should_not_depend_on_any_types_that_reside_in_Mordor_layer()
        {
            // arrange act assert
            Types().That().Are(_coreLayer).Should().NotDependOnAny(_mordorLayer).Because("It's dangerous to venture beyond the mountains of Mordor!").Check(Architecture);
        }
    }
}
