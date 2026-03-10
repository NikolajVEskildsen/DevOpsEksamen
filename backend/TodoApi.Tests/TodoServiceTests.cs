using TodoApi.Services;
using Xunit;

namespace TodoApi.Tests;

public class TodoServiceTests
{
    private TodoService CreateService() => new TodoService();

    [Fact]
    public void GetAll_ReturnsThreeStubItems()
    {
        var service = CreateService();
        var items = service.GetAll();
        Assert.Equal(5, items.Count);
    }

    [Fact]
    public void GetById_ExistingId_ReturnsCorrectItem()
    {
        var service = CreateService();
        var item = service.GetById(1);
        Assert.NotNull(item);
        Assert.Equal("Buy groceries", item.Title);
    }

    [Fact]
    public void GetById_NonExistingId_ReturnsNull()
    {
        var service = CreateService();
        var item = service.GetById(999);
        Assert.Null(item);
    }

    [Fact]
    public void Add_NewTitle_AddsItemToList()
    {
        var service = CreateService();
        var newItem = service.Add("Write tests");
        Assert.Equal("Write tests", newItem.Title);
        Assert.False(newItem.IsCompleted);
        Assert.Equal(6, service.GetAll().Count);
    }

    [Fact]
    public void ToggleComplete_ExistingItem_TogglesStatus()
    {
        var service = CreateService();
        var before = service.GetById(1)!.IsCompleted;
        service.ToggleComplete(1);
        var after = service.GetById(1)!.IsCompleted;
        Assert.NotEqual(before, after);
    }

    [Fact]
    public void ToggleComplete_NonExistingId_ReturnsFalse()
    {
        var service = CreateService();
        var result = service.ToggleComplete(999);
        Assert.False(result);
    }

    [Fact]
    public void Delete_ExistingItem_RemovesFromList()
    {
        var service = CreateService();
        service.Delete(1);
        Assert.Equal(4, service.GetAll().Count);
        Assert.Null(service.GetById(1));
    }

    [Fact]
    public void Delete_NonExistingId_ReturnsFalse()
    {
        var service = CreateService();
        var result = service.Delete(999);
        Assert.False(result);
    }
}
