using Inevent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Inevent.Elements
{
    /// <summary>
    /// Klasa przechowująca informacje o pojedynczym evencie, które są potrzebne do wyświetlenia pojedynczego kafelka
    /// </summary>
    public class EventTile
    {
        /// <summary>
        /// ID eventu
        /// </summary>
        public int EventId { get; private set; }
        /// <summary>
        /// ID twórcy
        /// </summary>
        public int OwnerId { get; private set; }
        /// <summary>
        /// Tytuł
        /// </summary>
        public string Title { get; private set; }
        /// <summary>
        /// Miejsce
        /// </summary>
        public string Place { get; private set; }
        /// <summary>
        /// Opis wydarzenia
        /// </summary>
        public string Description { get; private set; }
        /// <summary>
        /// Data wydarzenia
        /// </summary>
        public DateTime EventDate { get; private set; }
        /// <summary>
        /// Tablica tagów przypisanych do eventu
        /// </summary>
        public string[] Tags { get; private set; }

        /// <summary>
        /// Konstruktor klasy EventTile
        /// </summary>
        /// <param name="ev"> Obiekt klasy Event zawierający pobrane informacje o wydarzeniu </param>
        public EventTile(Event ev)
        {
            EventId = ev.Id;
            OwnerId = ev.OwnerId;
            Title = ev.Title;
            Place = ev.Place;
            Description = ev.Description;
            EventDate = ev.Date;
        }

        /// <summary>
        /// Metoda tworząca nowy element XML - StackPanel
        /// </summary>
        /// <returns> Zwraca element XML - StackPanel wypełniony elementami label z informacjami  </returns>
        public StackPanel CreateElement()
        {
            StackPanel tile = new StackPanel();
            Label title = new Label()
            {
                Content = Title
            };
            Label place = new Label()
            {
                Content = Place
            };
            Label description = new Label()
            {
                Content = Description
            };
            tile.Children.Add(title);
            tile.Children.Add(place);
            tile.Children.Add(description);
            return tile;
        }
    }
}
