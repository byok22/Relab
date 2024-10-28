using Domain.Repositories;
using Moq;
using Xunit;

namespace CustomersService.Tests
{
    public class SpecificEntity
    {
        public int Id { get; set; }
    }

    public class GenericRepositoryTests<TEntity> where TEntity : class, new()
    {
        private readonly Mock<IGenericRepository<TEntity>> _mockRepo;

        public GenericRepositoryTests()
        {
            _mockRepo = new Mock<IGenericRepository<TEntity>>();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEntity()
        {
            // Arrange
            var entity = new TEntity();
            entity.GetType().GetProperty("Id").SetValue(entity, 1);
            _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(entity);

            // Act
            var result = await _mockRepo.Object.GetByIdAsync(1);

            // Assert
            Assert.Equal(entity, result);
        }

        // Agrega más pruebas para otros métodos...
    }

    public class SpecificEntityRepositoryTests : GenericRepositoryTests<SpecificEntity>
    {
        // Aquí puedes agregar pruebas específicas para SpecificEntity si es necesario
    }
}