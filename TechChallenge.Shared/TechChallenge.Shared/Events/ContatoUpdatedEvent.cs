namespace TechChallenge.Shared.Events
{
    public class ContatoUpdatedEvent
    {
        public int CTT_ID { get; set; }
        public string CTT_NOME { get; set; } = string.Empty;
        public string CTT_EMAIL { get; set; } = string.Empty;
        public string CTT_NUMERO { get; set; } = string.Empty;
        public int CTT_DDD { get; set; }
    }
}
