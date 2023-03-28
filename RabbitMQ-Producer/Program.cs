using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Runtime.CompilerServices;
using System.Text;

IConnection conn = GetRabbitConnection();
while (true)
{
    var message = Console.ReadLine();
    if (message == "quit")
    {
        break;
    }
    SendMessage(message);
}



IModel GetRabbitChannel(string exchanheName = "test", string queueName = "test", string routingKey = "test")
{
    IModel model = conn.CreateModel();
    //model.ExchangeDeclare(exchanheName, ExchangeType.Direct);
    model.QueueDeclare("test", false, false, false, null);
    //model.QueueBind(queueName, exchanheName, routingKey, null);
    return model;
}

IConnection GetRabbitConnection()
{
    ConnectionFactory factory = new ConnectionFactory
    {
        HostName = "localhost"
    };
    return factory.CreateConnection();
}

void SendMessage(string message)
{
    IModel model = GetRabbitChannel();
    byte[] bodyBytes = Encoding.UTF8.GetBytes(message);
    model.BasicPublish(string.Empty,"test",null, bodyBytes);
}
