using EntityFrameworkCore.Testing.NSubstitute.Helpers;
using Microsoft.EntityFrameworkCore;

namespace TestUtilities.MockedObjects;

public class MockedDbContext
{
    private static string RandomDbName => Guid.NewGuid().ToString();

    public static DbContextOptions<T> GetOptions<T>() where T : DbContext
    {
        return new DbContextOptionsBuilder<T>()
            .UseInMemoryDatabase(RandomDbName)
            .Options;
    }

    public static T CreateFromOptions<T>(DbContextOptions<T> options) where T : DbContext
    {
        var dbContextToMock = (T)Activator.CreateInstance(typeof(T), options)!;

        var dbContext = new MockedDbContextBuilder<T>()
            .UseDbContext(dbContextToMock)
            .UseConstructorWithParameters(options)
            .MockedDbContext;

        PrepareData(dbContext);
        return dbContext;
    }

    public static void PrepareData<T>(T dbContext) where T : DbContext
    {
        // TODO

        dbContext.SaveChanges();
    }

    public static async Task PrepareDataAsync<T>(T dbContext) where T : DbContext
    {
        // TODO

        await dbContext.SaveChangesAsync();
    }
}
