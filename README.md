# RabbitMQ.NET sends a single message, and receives messages
we'll write two programs in C#; a producer that sends a single message, and a consumer that receives messages and prints them out. 
We'll gloss over some of the detail in the .NET client API, concentrating on this very simple thing just to get started. 
It's a "Hello World" of messaging.
We'll call our message publisher (sender) Send.cs and our message consumer (receiver) Receive.cs. 
The publisher will connect to RabbitMQ, send a single message, then exit.
