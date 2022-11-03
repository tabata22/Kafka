using Confluent.Kafka;

var cancellationTokenSource = new CancellationTokenSource();
var cancellationToken = cancellationTokenSource.Token;

var config = new ConsumerConfig
{
    BootstrapServers = "localhost:29092",
    GroupId = "group1",
    AutoOffsetReset = AutoOffsetReset.Earliest,
    IsolationLevel = IsolationLevel.ReadCommitted,
    PartitionAssignmentStrategy = PartitionAssignmentStrategy.CooperativeSticky
};

using var consumer = new ConsumerBuilder<int, string>(config).Build();

consumer.Subscribe("persons");

while (!cancellationToken.IsCancellationRequested)
{
    var result = consumer.Consume();

    Console.WriteLine("Consumer: 1, " + "Key: " + result.Message.Key + ", Message: " + result.Message.Value + ", Message date: " + result.Message.Timestamp.UtcDateTime + 
        ", Partition: " + result.Partition.Value);
}

consumer.Close();