using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inevent.Models
{
    /// <summary>
    /// Klasa pełniąca funkcję modelu wydarzenia + dodatkowe informacje obliczane podczas działania na obiektach klasy
    /// </summary>
    public class Event : INotifyPropertyChanged
    {
        /// <summary>
        ///  ID wydarzenia
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ID właściciela
        /// </summary>
        public int OwnerId { get; set; }
        /// <summary>
        /// Tytuł wydarzenia
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Data wydarzenia
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Miejsce wydarzenia
        /// </summary>
        public string Place { get; set; }
        /// <summary>
        ///  Opis wydarzenia
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Tablica zawierająca tagi przypisane do wydarzenia
        /// </summary>
        public string[] Tags { get; set; }
        /// <summary>
        /// Data zapisana jako string w formacie YYYY/MM/DD
        /// </summary>
        public string FormatedDate { get; set; }
        /// <summary>
        ///  Numer dnia, w którym odbędzie sie wydarzenie
        /// </summary>
        public string FormatedDay { get; set; }
        /// <summary>
        /// Dzień tygodnia, w którym odbędzie się wydarzenie
        /// </summary>
        public string FormatedDayName { get; set; }
        /// <summary>
        /// Procent trafności w przypadku dopasowywania wydarzenia do użytkownika
        /// </summary>
        public double MatchedValue { get; set; }
        /// <summary>
        ///  Czas w formacie DD, HH, MM, SS odliczający czas do rozpoczęcia wydarzenia
        /// </summary>
        private string _countdown;
        public string Countdown {
            get { return _countdown; }
            set
            {
                if (_countdown != value)
                {
                    _countdown = value;
                    OnPropertyChanged("Countdown");
                }
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;



        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
