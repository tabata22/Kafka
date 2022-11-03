using Confluent.Kafka;
using System.Text;
using System.Text.Json;

namespace Kafka.Common;

public class PersonSerializer : ISerializer<Person>
{
    public byte[] Serialize(Person data, SerializationContext context)
    {
        var json = JsonSerializer.Serialize(data);

        return Encoding.UTF8.GetBytes(json);
    }
}
