namespace Mac.MadeInCotia.Entities.Emails
{
    public class EmailsViewModel
    {
        public int IdEmail { get; set; }
        public string DsEmail { get; set; } = null!;
        public bool FlPrincipal { get; set; }
        public int IdColaborador { get; set; }
        public DateTime? DtCriacao { get; set; }
    }
}
