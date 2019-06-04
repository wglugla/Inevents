using Inevent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Inevent.Elements
{
    public class EventTile
    {
        private int EventId { get; set; }
        private int OwnerId { get; set; }
        private string Title { get; set; }
        private string Place { get; set; }
        private string Description { get; set; }
        private DateTime EventDate { get; set; }
        private string[] Tags { get; set; }

        public EventTile(Event ev)
        {
            EventId = ev.Id;
            OwnerId = ev.OwnerId;
            Title = ev.Title;
            Place = ev.Place;
            Description = ev.Description;
            EventDate = ev.EventDate;
        }

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
