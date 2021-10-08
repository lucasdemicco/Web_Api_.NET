using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAuth.Models;

namespace WebApiAuth.Repositories
{
    public class IUsuario
    {
        public interface IUsuarioRepository
        {
            Usuario Read(string email, string senha);
            void Create(Usuario usuario);
        }

        public class UsuarioRepository : IUsuarioRepository
        {

            private readonly DataContext _context;

            public UsuarioRepository(DataContext context)
            {
                _context = context;
            }
            public void Create(Usuario _usuario)
            {
                _usuario.Id = Guid.NewGuid();
                _context.Usuarios.Add(_usuario);
                _context.SaveChanges();
            }

            public Usuario Read(string email, string senha)
            {
                return _context.Usuarios.SingleOrDefault(
                    usuario => usuario.Email == email && usuario.Senha == senha
                    );
            }
        }
    }
}
