using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inevent.Models
{
    /// <summary>
    /// Klasa pełniąca rolę modelu użytkownika
    /// </summary>
    public class User
    {
        /// <summary>
        /// ID użytkownika
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nick użytkownika
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Imię użytkownika
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Nazwisko użytkownika
        /// </summary>
        public string Surname { get; set; }
    }
}
