using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Recive
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
                channel.ExchangeDeclare("contact", ExchangeType.Direct, false, false, null);
               
                channel.QueueDeclare(queue: "a",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                channel.QueueBind("a", "contact", "blue", null);
             
                 var consumer = new EventingBasicConsumer(channel);
                consumer.Received += Consumer_Received;
 
                channel.BasicConsume(queue: "a",
                                     autoAck: false,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }

     

        private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body.ToArray();
              var message = Encoding.UTF8.GetString(body);
               Console.WriteLine(" [x] Received {0}", message);
        }
    }
}
