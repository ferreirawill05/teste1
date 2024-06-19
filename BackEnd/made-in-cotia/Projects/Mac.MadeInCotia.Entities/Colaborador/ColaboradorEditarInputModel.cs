using Mac.MadeInCotia.Entities.Emails;
using Mac.MadeInCotia.Entities.Telefones;

namespace Mac.MadeInCotia.Entities.Colaborador
{
    public class ColaboradorEditarInputModel
    {
        public string Nome { get; set; }
        public int IdTipoUsuario { get; set; }

        public List<EmailsViewModel>? emails { get; set; }
        public List<TelefonesViewModel>? telefones { get; set; }
    }
}
