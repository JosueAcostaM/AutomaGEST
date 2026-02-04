using API_Consumer;
using Modelos_AutomaG;
using Servicios_AutomaG.Interfaces;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Servicios_AutomaG
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor; 
        public AuthService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Login(string email, string password)
        {

            var usuarios = CRUD<Usuarios>.GetAll();
            foreach (var usuario in usuarios)
            {
                if (usuario.emailusu == email)
                {

                    Console.WriteLine($"Comparando contraseña ingresado {password} con contraseña guardada {usuario.passwordhash} ");
                    if (BCrypt.Net.BCrypt.Verify(password, usuario.passwordhash))
                    {
                        var datosUsuario = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, usuario.nombreusu),
                            new Claim(ClaimTypes.Email, usuario.emailusu),
                        };
                        var credencialDigital = new ClaimsIdentity(datosUsuario, "Cookies");
                        var usuarioAutenticado = new ClaimsPrincipal(credencialDigital);

                        await _httpContextAccessor.HttpContext.SignInAsync("Cookies", usuarioAutenticado);
                        return true;
                    }
                }
            }
            return false;


        }

        public async Task<bool> Register(string email, string nombre, string apellido, string password)
        {
            var usuarioExistente = CRUD<Usuarios>.GetAll()
                .FirstOrDefault(u => u.emailusu == email);

            if (usuarioExistente != null)
                return false;

            try
            {
                CRUD<Usuarios>.Create(new Usuarios
                {
                    
                    nombreusu = nombre,
                    passwordhash = password,
                    emailusu = email,
                    activousu = true,
                    fechacreacion = DateTime.UtcNow
                });

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar usuario: {ex.Message}");
                return false;
            }
        }

    }
}
