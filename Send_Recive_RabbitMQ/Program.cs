using RabbitMQ.Client;
using System;
using System.Text;

namespace Send
{
    class Program
    {
        static void Main(string[] args)
        {
            //setup
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //RabbitMQ provides four types of exchanges: Direct, Fanout, Topic, and Headers
                channel.ExchangeDeclare("contact", "direct", false, false, null);
                channel.QueueDeclare(queue: "a",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
                channel.QueueDeclare(queue: "b",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
                channel.QueueDeclare(queue: "c",
                               durable: false,
                               exclusive: false,
                               autoDelete: false,
                               arguments: null);
                channel.QueueBind("a", "contact", "blue", null);
                channel.QueueBind("b", "contact", "red", null);
                channel.QueueBind("c", "contact", "blue", null);

                channel.QueueBind("a", "contact", "black", null);
                channel.QueueBind("b", "contact", "black", null);
                channel.QueueBind("c", "contact", "black", null);
                //--- Chat
                string message = " ";
                string Key = " ";
                while (message != string.Empty)
                {
                    Console.Write("Message : ");
                    message = Console.ReadLine();
                    Console.Write("RoutingKey : ");
                    Key = Console.ReadLine();
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "contact",
                        
                                         routingKey: Key,
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [producer ] Sent {0}", message);
                }
               
            }
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

        }
    }
}
