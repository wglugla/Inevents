using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inevent.Models
{
    /// <summary>
    ///  Klasa przechowująca informacje o obecnym użytkwoniku
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// ID użytkownika
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Token logowania przypisany dla bieżącej sesji
        /// </summary>
        public string token { get; set; } 
    }
}
