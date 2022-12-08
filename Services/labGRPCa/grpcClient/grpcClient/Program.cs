using Grpc.Net.Client;
using labGRPCaClient;

Action<ChangesReply> displayChangesReply = cr =>
{
    Console.WriteLine($@"
        ChangeId: {cr.ChangeId}
        CurrencyName: {cr.Currency.CurrencyName}
        Action: {cr.Currency.Action}
        Date: {cr.Currency.Date}
        Price: {cr.Currency.Price}");
};

Func<SingleCurrency> getCurrency = () =>
{
    Console.Write("currencyName: ");
    var currencyName = Console.ReadLine();
    Console.Write("action: ");
    var action = Console.ReadLine();
    Console.Write("date: ");
    var date = DateTime.Parse(Console.ReadLine());
    Console.Write("price: ");
    var price = decimal.Parse(Console.ReadLine());

    var currency = new SingleCurrency
    {
        CurrencyName = currencyName,
        Action = action,
        Date = date.ToString(),
        Price = (double)price
    };

    return currency;
};

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var connectionStr = config.GetConnectionString("serverConnection");

using var channel = GrpcChannel.ForAddress(connectionStr);

var client = new CryptoGrpcService.CryptoGrpcServiceClient(channel);

while (true)
{
    Console.WriteLine();
    Console.WriteLine(@"Функции gRPC сервиса:
        0: Exit
        1: GetAllChanges
        2: AddCurrencyRate
        3: GetCurrencyChangesStream
        4: AddCurrencyChangesStream");

    Console.Write("Выбор: ");
    var chois = int.Parse(Console.ReadLine());

    Console.WriteLine();

    switch (chois)
    {
        case 0:
            Environment.Exit(0);
            break;
        case 1:
            var changes = client.GetAllChanges(new AllCurrencyRequest());
            var meta = changes.Meta;
            Console.WriteLine($"total: {meta.Total} limit: {meta.Limit} offset: {meta.Offset}");

            foreach (var change in changes.Data)
                displayChangesReply(change);

            break;
        case 2:
            var currency = getCurrency();
            var result = client.AddCurrencyRate(currency);
            Console.WriteLine();
            Console.WriteLine(result.Content);
            break;
        case 3:
            Console.Write("currencyName: ");
            var name = Console.ReadLine();

            var serverData = client.GetCurrencyChangesStream(
                new ChangesRequest
                {
                    CurrencyName = name
                });
            var stream = serverData.ResponseStream;

            while (await stream.MoveNext(new CancellationToken()))
                displayChangesReply(stream.Current);

            break;
        case 4:
            Console.Write("Кол-во элементов: ");
            var elemsCount = int.Parse(Console.ReadLine());

            var addStream = client.AddCurrencyChangesStream();
            for (int i = 0; i < elemsCount; i++)
            {
                Console.WriteLine();
                var elem = getCurrency();
                await addStream.RequestStream.WriteAsync(elem);
            }
            await addStream.RequestStream.CompleteAsync();
            var response = await addStream.ResponseAsync;

            Console.WriteLine();
            Console.WriteLine(response.Content);
            
            break;

        default:
            break;
    }
}

