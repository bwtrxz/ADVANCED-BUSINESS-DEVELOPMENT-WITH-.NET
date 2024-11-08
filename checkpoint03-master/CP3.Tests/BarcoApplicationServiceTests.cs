using CP3.Application.Services;
using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using CP3.Domain.Interfaces.Dtos;
using Moq;

namespace CP3.Tests
{
    public class BarcoApplicationServiceTests
    {
        private readonly Mock<IBarcoRepository> _repositoryMock;
        private readonly BarcoApplicationService _barcoService;

        public BarcoApplicationServiceTests()
        {
            _repositoryMock = new Mock<IBarcoRepository>();
            _barcoService = new BarcoApplicationService(_repositoryMock.Object);
        }

        [Fact]
        public void AdicionarBarco_DeveRetornarBarcoEntity_QuandoAdicionarComSucesso()
        {
            // Arrange
            var barcoDtoMock = new Mock<IBarcoDto>();
            barcoDtoMock.Setup(b => b.Nome).Returns("Barco A");
            barcoDtoMock.Setup(b => b.Modelo).Returns("Modelo X");
            barcoDtoMock.Setup(b => b.Ano).Returns(2020);
            barcoDtoMock.Setup(b => b.Tamanho).Returns(30.5);

            var barcoEsperado = new BarcoEntity
            {
                Nome = "Barco A",
                Modelo = "Modelo X",
                Ano = 2020,
                Tamanho = 30.5
            };

            _repositoryMock.Setup(r => r.Adicionar(It.IsAny<BarcoEntity>())).Returns(barcoEsperado);

            // Act
            var resultado = _barcoService.AdicionarBarco(barcoDtoMock.Object);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(barcoEsperado.Nome, resultado.Nome);
            Assert.Equal(barcoEsperado.Modelo, resultado.Modelo);
            Assert.Equal(barcoEsperado.Ano, resultado.Ano);
            Assert.Equal(barcoEsperado.Tamanho, resultado.Tamanho);
        }

        [Fact]
        public void EditarBarco_DeveRetornarBarcoEntity_QuandoEditarComSucesso()
        {
            // Arrange
            var barcoDtoMock = new Mock<IBarcoDto>();
            barcoDtoMock.Setup(b => b.Nome).Returns("Barco B");
            barcoDtoMock.Setup(b => b.Modelo).Returns("Modelo Y");
            barcoDtoMock.Setup(b => b.Ano).Returns(2021);
            barcoDtoMock.Setup(b => b.Tamanho).Returns(35.0);

            var barcoEsperado = new BarcoEntity
            {
                Id = 1,
                Nome = "Barco B",
                Modelo = "Modelo Y",
                Ano = 2021,
                Tamanho = 35.0
            };
            _repositoryMock.Setup(r => r.Editar(It.IsAny<BarcoEntity>())).Returns(barcoEsperado);

            // Act
            var resultado = _barcoService.EditarBarco(1, barcoDtoMock.Object);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(barcoEsperado.Id, resultado.Id);
            Assert.Equal(barcoEsperado.Nome, resultado.Nome);
            Assert.Equal(barcoEsperado.Modelo, resultado.Modelo);
            Assert.Equal(barcoEsperado.Ano, resultado.Ano);
            Assert.Equal(barcoEsperado.Tamanho, resultado.Tamanho);
        }

        [Fact]
        public void ObterBarcoPorId_DeveRetornarBarcoEntity_QuandoBarcoExiste()
        {
            // Arrange
            var barcoEsperado = new BarcoEntity
            {
                Id = 1,
                Nome = "Barco C",
                Modelo = "Modelo Z",
                Ano = 2019,
                Tamanho = 28.0
            };
            _repositoryMock.Setup(r => r.ObterPorId(1)).Returns(barcoEsperado);

            // Act
            var resultado = _barcoService.ObterBarcoPorId(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(barcoEsperado.Id, resultado.Id);
            Assert.Equal(barcoEsperado.Nome, resultado.Nome);
            Assert.Equal(barcoEsperado.Modelo, resultado.Modelo);
            Assert.Equal(barcoEsperado.Ano, resultado.Ano);
            Assert.Equal(barcoEsperado.Tamanho, resultado.Tamanho);
        }

        [Fact]
        public void ObterTodosBarcos_DeveRetornarListaDeBarcos_QuandoExistiremBarcos()
        {
            // Arrange
            var barcosEsperados = new List<BarcoEntity>
            {
                new BarcoEntity { Id = 1, Nome = "Barco D", Modelo = "Modelo A", Ano = 2022, Tamanho = 40.0 },
                new BarcoEntity { Id = 2, Nome = "Barco E", Modelo = "Modelo B", Ano = 2023, Tamanho = 45.0 }
            };
            _repositoryMock.Setup(r => r.ObterTodos()).Returns(barcosEsperados);

            // Act
            var resultado = _barcoService.ObterTodosBarcos();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            Assert.Equal(barcosEsperados.First().Nome, resultado.First().Nome);
        }

        [Fact]
        public void RemoverBarco_DeveRetornarBarcoEntity_QuandoRemoverComSucesso()
        {
            // Arrange
            var barcoEsperado = new BarcoEntity
            {
                Id = 1,
                Nome = "Barco F",
                Modelo = "Modelo C",
                Ano = 2020,
                Tamanho = 50.0
            };
            _repositoryMock.Setup(r => r.Remover(1)).Returns(barcoEsperado);

            // Act
            var resultado = _barcoService.RemoverBarco(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(barcoEsperado.Id, resultado.Id);
            Assert.Equal(barcoEsperado.Nome, resultado.Nome);
            Assert.Equal(barcoEsperado.Modelo, resultado.Modelo);
            Assert.Equal(barcoEsperado.Ano, resultado.Ano);
            Assert.Equal(barcoEsperado.Tamanho, resultado.Tamanho);
        }
    }
}
