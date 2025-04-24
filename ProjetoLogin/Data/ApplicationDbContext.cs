using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetoLogin.Models;
using System.Reflection.Emit;


namespace ProjetoLogin.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            Guid AdminGuid = Guid.NewGuid();

            // Cadastrando as Roles padrão do Sistema 
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = AdminGuid.ToString(),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Professor",
                    NormalizedName = "PROFESSOR"
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Coordenador",
                    NormalizedName = "COORDENADOR"
                },
                 new IdentityRole
                 {
                     Id = Guid.NewGuid().ToString(),
                     Name = "Psicólogo",
                     NormalizedName = "PSICÓLOGO"
                 },
                 new IdentityRole
                 {
                     Id = Guid.NewGuid().ToString(),
                     Name = "Operador",
                     NormalizedName = "OPERADOR"
                 }
            );

            // Cadastrando o usuário padrão do sistema
            Guid UserGuid = Guid.NewGuid();
            var hasher = new PasswordHasher<IdentityUser>();

            builder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = UserGuid.ToString(),
                    UserName = "admin@admin.com",
                    NormalizedUserName = "ADMIN@ADMIN.COM",
                    Email = "admin@admin.com",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "@Abcd1234"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            );

            // Cadastrando o usuário padrão do sistema como Admin
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = UserGuid.ToString(),
                    RoleId = AdminGuid.ToString()
                }
            );
            builder.Entity<Aluno>().ToTable("Alunos");
            builder.Entity<Usuario>().ToTable("Usuarios");
        }
    }
}
