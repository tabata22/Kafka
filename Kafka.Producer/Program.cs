using Confluent.Kafka;
using Kafka.Common;
using System.Net;
using System.Text.Json;

var persons = Enumerable.Range(1, 5).Select(x => new Person(x, "Nika", "Tabatadze", 23, DateTime.Now));

var config = new ProducerConfig
{
    BootstrapServers = "localhost:29092",
    ClientId = Dns.GetHostName(),
    TransactionalId = "newId"
};

using var producer = new ProducerBuilder<int, string>(config).Build();

foreach (var person in persons)
{
    var result = await producer.ProduceAsync("persons", new Message<int, string>()
    {
        Key = person.Id,
        Value = JsonSerializer.Serialize(person)
    });

    Console.WriteLine($"key: {person.Id}, status: {result.Status}, partition: {result.Partition.Value}");
}

Console.WriteLine("done!!!");
Console.ReadLine();