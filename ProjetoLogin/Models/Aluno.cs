namespace ProjetoLogin.Models
{
    public class Aluno
    {
        public Guid AlunoId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string CPF { get; set; }
        public DateOnly DataNasc { get; set; }
        public string RM { get; set; }
        public DateTime? DataCadastro { get; set; }
        public bool? CadastroAtivo { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public string Numero { get; set; }
        public string UrlFoto { get; set; }
    }
}
