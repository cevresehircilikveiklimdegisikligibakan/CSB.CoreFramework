namespace CSB.Core.Entities
{
    public record KeyValue
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public KeyValue() { }
        public KeyValue(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}