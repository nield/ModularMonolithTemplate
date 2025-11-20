using ModularMonolithTemplate.Api.Modules.Reminder.Endpoints.V1.Todos;
using ModularMonolithTemplate.Api.Modules.Reminder.Entities;

namespace ModularMonolithTemplate.Api.Tests.Modules.Reminder.Endpoints.V1.ToDos.GetToDo;

public class EndpointTests : BaseReminderTestFixture
{
    [Fact]
    public async Task Given_InvalidId_Should_ThrowException()
    {
        var id = 1L;

        _reminderDbContextMock.TodoItems.FindAsync(id, Arg.Any<CancellationToken>())
            .ReturnsNull();

        await Assert.ThrowsAsync<NotFoundException>(() =>
            GetById.Handler(id, _toDoRepositoryMock, CancellationToken.None));
    }

    [Fact]
    public async Task Given_ValidId_Should_ReturnData()
    {
        var id = 1L;

        var item = Builder<ToDoItem>.CreateNew().Build();

        _toDoRepositoryMock.GetByIdAsync(id, Arg.Any<CancellationToken>())
            .Returns(item);

        var sut = await GetById.Handler(id, _toDoRepositoryMock, CancellationToken.None);

        sut.Should().NotBeNull();
    }
}
