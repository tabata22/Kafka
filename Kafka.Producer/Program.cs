using Confluent.Kafka;
using Kafka.Common;
using System.Net;
using System.Text.Json;

var persons = Enumerable.Range(1, 10000).Select(x => new Person(x, "Nika", "Tabatadze", 23, DateTime.Now));

var config = new ProducerConfig
{
    BootstrapServers = "localhost:29092",
    ClientId = Dns.GetHostName(),
};

using var producer = new ProducerBuilder<int, Person>(config).Build();

foreach (var person in persons)
{
    var result = await producer.ProduceAsync("persons", new Message<int, Person>()
    {
        Key = person.Id,
        Value = person
    });

    Console.WriteLine($"key: {person.Id}, status: {result.Status}, partition: {result.Partition.Value}");
}

Console.WriteLine("done!!!");
Console.ReadLine();