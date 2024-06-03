namespace Mac.MadeInCotia.Entities.Colaborador
{
    public class ColaboradorViewModel
    {
        public int IdColaborador { get; set; }
        public int IdTipoUsuario { get; set; } 
        public string Nome { get; set; } = null!;
        public string Cpf { get; set; } = null!;
        public string NmUsuario { get; set; } = null!;
        public string Senha { get; set; } = null!;
    }
}