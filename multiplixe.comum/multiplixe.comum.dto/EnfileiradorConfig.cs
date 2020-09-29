namespace multiplixe.comum.dto
{
    public class EnfileiradorConfig
    {
        public string HostName { get; private set; }
        public string Nome { get; private set; }
        public bool Durable { get; private set; }
        public bool Exclusive { get; private set; }
        public bool AutoDelete { get; private set; }
        public bool Persistent { get { return Durable; } }
        public bool AutoAck { get; private set; }
        public EnfileiradorConfig(string hostName, string nome, bool durable, bool exclusive, bool autoDelete, bool autoAck)
        {
            HostName = hostName;
            Nome = nome;
            Durable = durable;
            Exclusive = exclusive;
            AutoDelete = autoDelete;
            AutoAck = autoAck;
        }
    }
}
