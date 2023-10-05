using Bogus;
using ObjectModels.Models;

namespace SampleService01.DataGeneration;

public static class DataGenerator
{
    private static string[] items = new string[5] {
            "apple",
            "banana",
            "orange",
            "strawberry",
            "kiwi"
        };

    public static IEnumerable<Buyer> GenerateBuyersForever()
    {
        Randomizer.Seed = new Random(3897234);

        int orderIds = 0;
        Faker<Order> orders
            = new Faker<Order>()
               .StrictMode(true)
               .RuleFor(order => order.OrderId, fake => orderIds++)
               .RuleFor(order => order.Item, fake => fake.PickRandom(items))
               .RuleFor(order => order.Quantity, fake => fake.Random.Number(1, 10));

        int userIds = 0;
        Faker<Buyer> buyers =
            new Faker<Buyer>()
               .StrictMode(true)
               .RuleFor(buyer => buyer.Id, fake => userIds++)
               .RuleFor(buyer => buyer.FirstName, fake => fake.Name.FirstName())
               .RuleFor(buyer => buyer.LastName, fake => fake.Name.LastName())
               .RuleFor(buyer => buyer.Email, (fake, buyer) => fake.Internet.Email(buyer.FirstName, buyer.LastName))
               .RuleFor(buyer => buyer.Gender, fake => fake.PickRandom<Gender>())
               .RuleFor(buyer => buyer.CartId, fake => Guid.NewGuid())
               .RuleFor(buyer => buyer.Orders, fake => orders.Generate(fake.Random.Number(1, 7)))
               .FinishWith((fake, buyer) =>
               {
                   //Console.WriteLine($"Buyer Created! buyerIds: '{buyer.Id:000000000000}'.");
               });

        return buyers.GenerateForever();
    }
}