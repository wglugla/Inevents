using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inevent.Models
{
    /// <summary>
    /// Klasa reprezentująca obiekt pojedynczego tagu
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// ID tagu
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Wartość tagu
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Informacja wymagana przy checkboxach, określająca czy dany tag został wybrany (checked/unchecked)
        /// </summary>
        public bool IsChecked { get; set; }
    }
}
