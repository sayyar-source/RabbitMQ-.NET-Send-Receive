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
                channel.ExchangeDeclare("subject", ExchangeType.Topic, false, false, null);
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
                channel.QueueBind("a", "subject","*.manager.*", null);
                channel.QueueBind("b", "subject", "director.*", null);
                channel.QueueBind("c", "subject", "master.#", null);

               
                //--- Chat
                string message = " ";
                string Key = " ";
                while (message != string.Empty)
                {
                    Console.Write("Message : ");
                    message = Console.ReadLine();
                    Console.Write("routingKey : ");
                    Key = Console.ReadLine();
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "subject",                   
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
