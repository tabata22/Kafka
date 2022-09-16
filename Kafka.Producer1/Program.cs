using Confluent.Kafka;
using System.Net;

var config = new ProducerConfig
{
    BootstrapServers = "localhost:29092",
    ClientId = Dns.GetHostName(),
    Acks = Acks.All,
    //TransactionalId = "tran1",
    EnableIdempotence = true,
    MessageSendMaxRetries = int.MaxValue,
    MaxInFlight = 5,
    BatchNumMessages = 100
};

using var producer = new ProducerBuilder<int, string>(config).Build();

//producer.InitTransactions(TimeSpan.FromSeconds(3));

//producer.BeginTransaction();

var result = await producer.ProduceAsync("test", new Message<int, string> { Key = 8, Value = "message 1" }).ConfigureAwait(false);
//await producer.ProduceAsync("transactions", new Message<int, string> { Key = 14, Value = "test message from Nick" }).ConfigureAwait(false);
//await producer.ProduceAsync("transactions", new Message<int, string> { Key = 15, Value = "test message from Nick" }).ConfigureAwait(false);

//producer.AbortTransaction();
//producer.CommitTransaction();

Console.WriteLine(result.Status);
Console.WriteLine("done!!!");