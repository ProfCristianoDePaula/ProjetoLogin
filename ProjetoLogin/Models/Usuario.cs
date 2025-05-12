using Microsoft.AspNetCore.Identity;

namespace ProjetoLogin.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string NomeCompleto { get; set; }
        public DateTime? DataCadastro { get; set; }
        public bool? Ativo { get; set; }
        public Guid? AppUserId { get; set; } // Nullable Guid to allow for null values
        public IdentityUser? IdentityUser { get; set; } // Navigation property to IdentityUser
        public string TipoUsuario { get; set; } // Assuming TipoUsuario is a string, adjust as necessary    
        public Usuario()
        {
            DataCadastro = DateTime.Now;
            Ativo = true;
        }


    }
}
