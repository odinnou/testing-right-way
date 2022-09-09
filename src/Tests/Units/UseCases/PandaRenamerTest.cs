using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Service.Core.Models;
using Service.Core.UseCases;

namespace Tests.Units.UseCases
{
    public class PandaRenamerTest
    {
        private readonly Mock<IPandaFetcher> _pandaFetcher;
        private readonly PandaRenamer _pandaRenamer;

        // Setup our test runner
        public PandaRenamerTest()
        {
            _pandaFetcher = new Mock<IPandaFetcher>();

            // Simulate dependency injection (Moq).
            _pandaRenamer = new PandaRenamer(_pandaFetcher.Object);
        }

        // Theory (xUnit) : enable parameterised tests.
        // AutoData (AutoFixure) : enable parameters auto generation.
        [Theory, AutoData]
        public void Should_throw_ArgumentNullException_when_provided_name_is_empty_and_never_called_PandaFetcher(Guid pandaId)
        {
            // act
            Action result = () => { _pandaRenamer.Execute(pandaId, string.Empty); };

            // assert (FluentAssertions)
            result.Should().Throw<ArgumentNullException>().And.Message.Should().Contain("newName");
            _pandaFetcher.Verify(mock => mock.Execute(It.IsAny<Guid>()), Times.Never);
        }

        [Theory, AutoData]
        public void Should_returns_an_updated_panda_with_a_new_name(Guid pandaId, string newName, Panda panda)
        {
            // arrange (Moq)
            _pandaFetcher.Setup(mock => mock.Execute(pandaId)).Returns(panda);

            // act
            Panda renamedPanda = _pandaRenamer.Execute(pandaId, newName);

            // assert
            renamedPanda.Should().Be(panda);
            renamedPanda.Name.Should().Be(newName);
            // We don't need to use .Verify here because the mock step is sufficient to assert that.
        }
    }
}
