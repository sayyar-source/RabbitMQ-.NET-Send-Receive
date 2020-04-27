using RabbitMQ.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
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
                channel.ExchangeDeclare("header", ExchangeType.Headers, false, false, null);
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
                var headerBind1 = new Dictionary<string,object>();
                headerBind1.Add("x-match", "any");
                headerBind1.Add("key1", "bmp");
                headerBind1.Add("key2", "mp4");
                channel.QueueBind("a", "header","",headerBind1);

                var headerBind2 = new Dictionary<string, object>();
                headerBind2.Add("x-match", "any");
                headerBind2.Add("key1", "jpg");
                headerBind2.Add("key2", "mp3");
                headerBind2.Add("key3", "mpeg");
                channel.QueueBind("b", "header", "",headerBind2);

                var headerBind3 = new Dictionary<string, object>();
                headerBind3.Add("x-match", "all");
                headerBind3.Add("key1", "mkv");
                headerBind3.Add("key2", "bpm");
                channel.QueueBind("c", "header", "", headerBind3);

               
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
                    var properties = channel.CreateBasicProperties();
                    var headers = new Dictionary<string, object>()
                    {
                        {"key1",Key},
                        {"key2",Key },
                        {"key3",Key },
                        {"time",DateTime.Now.ToShortTimeString() }
                    };
                    properties.Headers = headers;
                    channel.BasicPublish(exchange: "header",                   
                                         routingKey: "",
                                         basicProperties: properties,
                                         body: body);
                    Console.WriteLine(" [producer ] Sent {0}", message);
                }
               
            }
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

        }
    }
}
