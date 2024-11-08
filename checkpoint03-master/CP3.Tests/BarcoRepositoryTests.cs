using CP3.Data.AppData;
using CP3.Data.Repositories;
using CP3.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CP3.Tests
{
    public class BarcoRepositoryTests
    {
        private readonly DbContextOptions<ApplicationContext> _options;
        private readonly ApplicationContext _context;
        private readonly BarcoRepository _barcoRepository;

        public BarcoRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationContext(_options);
            _barcoRepository = new BarcoRepository(_context);
        }

        [Fact]
        public void Adicionar_DeveAdicionarBarcoERetornarBarcoEntity()
        {
            // Arrange
            var barco = new BarcoEntity
            {
                Nome = "Barco Teste",
                Modelo = "Modelo X",
                Ano = 2020,
                Tamanho = 15.5
            };

            // Act
            var resultado = _barcoRepository.Adicionar(barco);

            // Assert
            var barcoNoDb = _context.Barco.FirstOrDefault(b => b.Id == resultado.Id);
            Assert.NotNull(barcoNoDb);
            Assert.Equal(barco.Nome, barcoNoDb.Nome);
            Assert.Equal(barco.Modelo, barcoNoDb.Modelo);
            Assert.Equal(barco.Ano, barcoNoDb.Ano);
            Assert.Equal(barco.Tamanho, barcoNoDb.Tamanho);
        }

        [Fact]
        public void Editar_DeveAtualizarBarcoERetornarBarcoEntity_QuandoBarcoExiste()
        {
            // Arrange
            var barco = new BarcoEntity
            {
                Nome = "Barco Teste",
                Modelo = "Modelo X",
                Ano = 2020,
                Tamanho = 15.5
            };
            _context.Barco.Add(barco);
            _context.SaveChanges();

            barco.Nome = "Barco Atualizado";
            barco.Modelo = "Modelo Y";
            barco.Ano = 2022;
            barco.Tamanho = 18.0;

            // Act
            var resultado = _barcoRepository.Editar(barco);

            // Assert
            var barcoNoDb = _context.Barco.FirstOrDefault(b => b.Id == barco.Id);
            Assert.NotNull(barcoNoDb);
            Assert.Equal("Barco Atualizado", barcoNoDb.Nome);
            Assert.Equal("Modelo Y", barcoNoDb.Modelo);
            Assert.Equal(2022, barcoNoDb.Ano);
            Assert.Equal(18.0, barcoNoDb.Tamanho);
        }

        [Fact]
        public void ObterPorId_DeveRetornarBarcoEntity_QuandoBarcoExiste()
        {
            // Arrange
            var barco = new BarcoEntity
            {
                Nome = "Barco Teste",
                Modelo = "Modelo X",
                Ano = 2020,
                Tamanho = 15.5
            };
            _context.Barco.Add(barco);
            _context.SaveChanges();

            // Act
            var resultado = _barcoRepository.ObterPorId(barco.Id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(barco.Id, resultado.Id);
            Assert.Equal(barco.Nome, resultado.Nome);
            Assert.Equal(barco.Modelo, resultado.Modelo);
            Assert.Equal(barco.Ano, resultado.Ano);
            Assert.Equal(barco.Tamanho, resultado.Tamanho);
        }

        [Fact]
        public void ObterTodos_DeveRetornarListaDeBarcos_QuandoExistiremBarcos()
        {
            // Arrange
            var barcos = new List<BarcoEntity>
            {
                new BarcoEntity { Nome = "Barco 1", Modelo = "Modelo A", Ano = 2021, Tamanho = 10.0 },
                new BarcoEntity { Nome = "Barco 2", Modelo = "Modelo B", Ano = 2022, Tamanho = 12.5 }
            };
            _context.Barco.AddRange(barcos);
            _context.SaveChanges();

            // Act
            var resultado = _barcoRepository.ObterTodos();

            // Assert
            Assert.Equal(barcos.Count, resultado.Count());
            Assert.Equal(barcos[0].Nome, resultado.First().Nome);
            Assert.Equal(barcos[1].Nome, resultado.Last().Nome);
        }

        [Fact]
        public void Remover_DeveRemoverBarcoERetornarBarcoEntity_QuandoBarcoExiste()
        {
            // Arrange
            var barco = new BarcoEntity
            {
                Nome = "Barco Teste",
                Modelo = "Modelo X",
                Ano = 2020,
                Tamanho = 15.5
            };
            _context.Barco.Add(barco);
            _context.SaveChanges();

            // Act
            var resultado = _barcoRepository.Remover(barco.Id);

            // Assert
            var barcoNoDb = _context.Barco.FirstOrDefault(b => b.Id == barco.Id);
            Assert.Null(barcoNoDb);
            Assert.Equal(barco, resultado);
        }
    }
}
