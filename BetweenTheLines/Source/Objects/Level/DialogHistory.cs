// Stores the last 5 dialog lines read in the game.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetweenTheLines.Source.Objects.Level
{
    internal class DialogHistory
    {
        // Public Variables
        public readonly int MaxSize = 5;

        // Private Variables
        private List<DialogString> history;

        // Length of History List
        public int Length
        {
            get { return history.Count; }
        }

        public DialogHistory()
        {
            history = new List<DialogString>();
        }

        // Getters and Setters

        public void Add(DialogString dialog)
        {
            history.Add(dialog);
        }

        public void Remove(DialogString dialog)
        {
            history.Remove(dialog);
        }

        public void RemoveFirst()
        {
            if (history.Count > 0)
            {
                history.RemoveAt(0);
            }
        }
    }
}
