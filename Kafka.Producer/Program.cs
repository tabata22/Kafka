using Confluent.Kafka;
using Kafka.Common;
using System.Net;

var persons = Enumerable.Range(1, 10000).Select(x => new Person(x, x.ToString()));

var config = new ProducerConfig
{
    BootstrapServers = "localhost:29092",
    ClientId = Dns.GetHostName(),
};

using var producer = new ProducerBuilder<int, string>(config).Build();

//var topic = new TopicPartition("mytopic", new Partition(1));

foreach (var person in persons)
{
    var result = await producer.ProduceAsync("mytopic", new Message<int, string>()
    {
        Key = person.Key,
        Value = person.Value
    });

    Console.WriteLine($"key: {person.Key}, status: {result.Status}, partition: {result.Partition.Value}");
}
//var result = await producer.ProduceAsync(topic, new Message<int, string>()
//{
//    Key = 7,
//    Value = "test message from nick!!!!"
//});

//Console.WriteLine(result.Status);
Console.WriteLine("done!!!");
Console.ReadLine();