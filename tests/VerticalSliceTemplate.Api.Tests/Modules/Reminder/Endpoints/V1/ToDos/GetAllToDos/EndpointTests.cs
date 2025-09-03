using VerticalSliceTemplate.Api.Modules.Reminder.Endpoints.V1.Todos;
using VerticalSliceTemplate.Api.Modules.Reminder.Entities;

namespace VerticalSliceTemplate.Api.Tests.Modules.Reminder.Endpoints.V1.ToDos.GetAllToDos;

public class EndpointTests : BaseTestFixture
{
    [Fact]
    public async Task Given_Data_Exists_Should_ReturnData()
    {
        var items = Builder<ToDoItem>.CreateListOfSize(1)
            .Build().AsQueryable().BuildMockDbSet();

        _reminderDbContextMock.TodoItems
            .Returns(items);

        var sut = await GetAll.Handler(_reminderDbContextMock, CancellationToken.None);

        sut.Should().NotBeNullOrEmpty();
    }
}
