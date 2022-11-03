using Confluent.Kafka;
using System.Net;
using System.Text.Json;

var config = new ProducerConfig
{
    BootstrapServers = "localhost:29092",
    ClientId = Dns.GetHostName(),
};

var student = new Student(1, "Nika Tabatadze", 28);

using var producer = new ProducerBuilder<int, string>(config).Build();

var result = await producer.ProduceAsync("students", new Message<int, string>()
{
    Key = student.Id,
    Value = JsonSerializer.Serialize(student)
});

Console.WriteLine($"key: {student.Id}, status: {result.Status}, partition: {result.Partition.Value}");

internal record Student(int Id, string Name, int Age);