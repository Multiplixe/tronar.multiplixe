namespace multiplixe.twitch.ping.dtos
{
    public class PingConfig
    {
        public int FrequenciaMinutos { get; set; }
        public int ToleranciaSegundos { get; set; }
        public string ChavePingKey { get; set; }
        public string Chamada { get; set; }
    }
}
