# RabbitMQ.NET sends a single message, and receives messages
we'll write two programs in C#; a producer that sends a single message, and a consumer that receives messages and prints them out. 
We'll gloss over some of the detail in the .NET client API, concentrating on this very simple thing just to get started. 
It's a "Hello World" of messaging.
We'll call our message publisher (sender) Send.cs and our message consumer (receiver) Receive.cs. 
The publisher will connect to RabbitMQ, send a single message, then exit.

Exchange Types

Exchanges control the routing of messages to queues. 
Each exchange type defines a specific routing algorithm which the server uses to determine which bound queues a published message should be routed to.

RabbitMQ provides four types of exchanges: Direct, Fanout, Topic, and Headers.

Direct Exchanges

The Direct exchange type routes messages with a routing key equal to the routing key declared by the binding queue.

Fanout Exchanges

The Fanout exchange type routes messages to all bound queues indiscriminately.  If a routing key is provided, it will simply be ignored. 

Topic Exchanges

The Topic exchange type routes messages to queues whose routing key matches all, or a portion of a routing key. 
With topic exchanges, messages are published with routing keys containing a series of words separated by a dot (e.g. “word1.word2.word3”).  Queues binding to a topic exchange supply a matching pattern for the server to use when routing the message.  Patterns may contain an asterisk (“*”) to match a word in a specific position of the routing key, or a hash (“#”) to match zero or more words.  
For example, a message published with a routing key of “honda.civic.navy” would match queues bound with “honda.civic.navy”, “*.civic.*”, “honda.#”, or “#”, but would not match “honda.accord.navy”, “honda.accord.silver”, “*.accord.*”, or “ford.#”. 

Headers Exchanges
The Headers exchange type routes messages based upon a matching of message headers to the expected headers specified by the binding queue.  The headers exchange type is similar to the topic exchange type in that more than one criteria can be specified as a filter, but the headers exchange differs in that its criteria is expressed in the message headers as opposed to the routing key, may occur in any order, and may be specified as matching any or all of the specified headers.



