using System.ComponentModel.DataAnnotations;
using AutoFixture;
using BudgetBuddy.Domain.Abstractions.Repository.Core;
using BudgetBuddy.Domain.Abstractions.Repository.Exceptions;
using BudgetBuddy.Infrastructure.Repository.Core;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;

namespace BudgetBuddy.Tests;

public class RepositoryTests
{
    private ApplicationDbContext _context = null!;
    private IRepository<TestEntity> _repository = null!;
    private readonly IFixture _fixture;

    public RepositoryTests()
    {
        _fixture = new Fixture();
    }

    #region AddAsync

    [Fact]
    public async Task AddAsync_ShouldThrowArgumentNullException_WhenEntityIsNull()
    {
        // Arrange
        _context = CreateInMemoryContext();
        _repository = new TestEntityRepository(_context);

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            // Act
            await _repository.AddAsync(null!);
        });
    }

    [Fact]
    public async Task AddAsync_ShouldAddEntity()
    {
        // Arrange
        _context = CreateInMemoryContext();
        _repository = new TestEntityRepository(_context);
        var entity = A.Fake<TestEntity>();

        // Act
        await _repository.AddAsync(entity);
        await _context.SaveChangesAsync();

        // Assert
        Assert.Equal(1, _context.TestEntities.Count());
        Assert.Equal(entity, _context.TestEntities.First());
    }

    [Fact]
    public async Task AddAsync_ShouldThrowDuplicatedKeyException_WhenEntityIsDuplicated()
    {
        // Arrange
        _context = CreateInMemoryContext();
        _repository = new TestEntityRepository(_context);
        var entity = A.Fake<TestEntity>();
        _context.TestEntities.Add(entity);
        await _context.SaveChangesAsync();

        // Assert
        await Assert.ThrowsAsync<DuplicatedEntityException>(async () =>
        {
            // Act
            await _repository.AddAsync(entity);
            await _context.SaveChangesAsync();
        });
    }

    #endregion

    #region Delete

    [Fact]
    public async Task Delete_ShouldThrowArgumentNullException_WhenIdentifiersIsNull()
    {
        // Arrange
        _context = CreateInMemoryContext();
        _repository = new TestEntityRepository(_context);

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            // Act
            await _repository.Delete(null!);
        });
    }

    [Fact]
    public async Task Delete_ShouldThrowsNotFoundException_WhenEntityIsNotFound()
    {
        // Arrange
        _context = CreateInMemoryContext();
        _repository = new TestEntityRepository(_context);
        var entity = _fixture.Build<TestEntity>()
        .With(e => e.Id, 1)
        .Create();
        _context.TestEntities.Add(entity);
        await _context.SaveChangesAsync();

        // Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            // Act
            await _repository.Delete(2);
            await _context.SaveChangesAsync();
        });
    }

    [Fact]
    public async Task Delete_ShouldDeleteEntity()
    {
        // Arrange
        _context = CreateInMemoryContext();
        _repository = new TestEntityRepository(_context);
        var entity = A.Fake<TestEntity>();
        _context.TestEntities.Add(entity);
        await _context.SaveChangesAsync();

        // Act
        await _repository.Delete(entity.Id);
        await _context.SaveChangesAsync();

        // Assert
        Assert.Empty(_context.TestEntities);
    }

    #endregion

    #region GetByIdentifiersAsync

    [Fact]
    public async Task GetByIdentifiersAsync_ShouldThrowArgumentNullException_WhenIdentifiersIsNull()
    {
        // Arrange
        _context = CreateInMemoryContext();
        _repository = new TestEntityRepository(_context);

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            // Act
            await _repository.GetByIdentifiersAsync(null!);
        });
    }

    [Fact]
    public async Task GetByIdentifiersAsync_ShouldThrowsNotFoundException_WhenEntityIsNotFound()
    {
        // Arrange
        _context = CreateInMemoryContext();
        _repository = new TestEntityRepository(_context);
        var entity = _fixture.Build<TestEntity>()
        .With(e => e.Id, 1)
        .Create();
        _context.TestEntities.Add(entity);
        await _context.SaveChangesAsync();

        // Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            // Act
            await _repository.GetByIdentifiersAsync(2);
        });
    }

    [Fact]
    public async Task GetByIdentifiersAsync_ShouldReturnEntity()
    {
        // Arrange
        _context = CreateInMemoryContext();
        _repository = new TestEntityRepository(_context);
        var entity = A.Fake<TestEntity>();
        _context.TestEntities.Add(entity);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetByIdentifiersAsync(entity.Id);

        // Assert
        Assert.Equal(entity, result);
    }

    #endregion

    #region Update

    [Fact]
    public Task Update_ShouldThrowArgumentNullException_WhenEntityIsNull()
    {
        // Arrange
        _context = CreateInMemoryContext();
        _repository = new TestEntityRepository(_context);

        // Assert
        return Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            // Act
            await _repository.Update(null!, 2);
        });
    }

    [Fact]
    public Task Update_ShouldThrowArgumentNullException_WhenIdentifiersIsNull()
    {
        // Arrange
        _context = CreateInMemoryContext();
        _repository = new TestEntityRepository(_context);
        var entity = A.Fake<TestEntity>();

        // Assert
        return Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            // Act
            await _repository.Update(entity, null!);
        });
    }

    [Fact]
    public async Task Update_ShouldThrowNotFoundException_WhenEntityIsNotFound()
    {
        // Arrange
        _context = CreateInMemoryContext();
        _repository = new TestEntityRepository(_context);
        var entity = _fixture.Build<TestEntity>()
        .With(e => e.Id, 1)
        .Create();
        _context.TestEntities.Add(entity);
        await _context.SaveChangesAsync();

        // Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        {
            // Act
            await _repository.Update(entity, 2);
        });
    }

    [Fact]
    public async Task Update_ShouldUpdateEntity()
    {
        // Arrange
        _context = CreateInMemoryContext();
        _repository = new TestEntityRepository(_context);
        var originalEntity = _fixture.Build<TestEntity>()
        .With(e => e.Id, 1)
        .With(e => e.Name, "Foo")
        .Create();
        _context.TestEntities.Add(originalEntity);
        await _context.SaveChangesAsync();

        // Act
        var newEntity = _fixture.Build<TestEntity>()
        .With(e => e.Id, 1)
        .With(e => e.Name, "Bar")
        .Create();
        await _repository.Update(newEntity, originalEntity.Id);
        await _context.SaveChangesAsync();

        // Assert
        Assert.Equal(1, _context.TestEntities.Count());
        Assert.Equal(newEntity, _context.TestEntities.First());
    }

    #endregion

    // Configurations
    public class ApplicationDbContext(DbContextOptions<RepositoryTests.ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<TestEntity> TestEntities { get; set; }
    }

    public static ApplicationDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()
            .Options;
        return new ApplicationDbContext(options);
    }

    // Test data
    public class TestEntity
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            var other = obj as TestEntity;

            return other!.Id == Id && other.Name == Name;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class TestEntityRepository(RepositoryTests.ApplicationDbContext context) : Repository<TestEntity>(context)
    {
    }
}
