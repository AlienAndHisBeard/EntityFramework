namespace EntityFramework
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Repository repository = new(new ApplicationDbContext());
            ShopController shop = new(repository);
            if (!await shop.SetUp()) return;

            await shop.RunShop();
        }
    }
}