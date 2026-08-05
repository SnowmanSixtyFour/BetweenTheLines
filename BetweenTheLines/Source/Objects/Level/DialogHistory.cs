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
        private List<DialogString> history;

        public DialogHistory()
        {
            history = new List<DialogString>();
        }

        public void Add(DialogString dialog)
        {
            history.Add(dialog);
        }

        public void Remove(DialogString dialog)
        {
            history.Remove(dialog);
        }
    }
}
