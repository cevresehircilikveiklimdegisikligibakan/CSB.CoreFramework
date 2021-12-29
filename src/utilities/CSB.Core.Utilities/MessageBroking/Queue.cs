using System.Collections.Generic;

namespace CSB.Core.Utilities.MessageBroking
{
    public record Queue
    {
        public string Name { get; private set; }
        public bool Durable { get; private set; }
        public bool Exclusive { get; private set; }
        public bool AutoDelete { get; private set; }
        public string Exchange { get; private set; } = "";
        public IDictionary<string, object> Arguments { get; private set; }

        private Queue() { }

        public static Queue Create(string name)
        {
            return new Queue() { Name = name };
        }
        public Queue SetDurable(bool durable)
        {
            this.Durable = durable;
            return this;
        }
        public Queue SetExclusive(bool exclusive)
        {
            this.Exclusive = exclusive;
            return this;
        }
        public Queue SetAutoDelete(bool autoDelete)
        {
            this.AutoDelete = autoDelete;
            return this;
        }
        public Queue SetExchange(string exchange)
        {
            this.Exchange = exchange;
            return this;
        }
        public Queue SetArguments(IDictionary<string, object> arguments)
        {
            this.Arguments = arguments;
            return this;
        }
    }
}