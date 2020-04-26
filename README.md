# RabbitMQ-.NET-Send-Receive
We'll call our message publisher (sender) Send.cs and our message consumer (receiver) Receive.cs. The publisher will connect to RabbitMQ, send a single message, then exit.

in this example, we took a look at the four exchange types provided by RabbitMQ: Direct, Fanout, Topic, and Headers.  
In this installment we’ll walk through an example which uses a direct exchange type directly and we’ll take a look at the push API.

In the Hello World example from the second installment of the series, we used a direct exchange type implicitly by taking advantage of the automatic  binding of queues to the default exchange using the queue name as the routing key.  
The example we’ll work through this time will be similar, but we’ll declare and bind to the exchange explicitly.

This time our example will be a distributed logging application.  
We’ll create a Producer console application which publishes a logging message for some noteworthy action and a Consumer console application which displays the message to the console.

Beginning with our Producer app, we’ll start by establishing a connection using the default settings, create the connection, and create a channel:
