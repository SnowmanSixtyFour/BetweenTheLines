// Keeps track of an integer number and a string.
// The int being for the speaker's name, and the string being their dialog.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetweenTheLines.Source.Objects.Level
{
    internal class DialogString
    {
        public int name {  get; set; }
        public int state { get; set; }
        public string text { get; set; }
        public int textColor { get; set; }
        public bool hideName { get; set; }

        public DialogString(int name, string text, int textColor = 0, int state = 0, bool hideName = false)
        {
            this.name = name;
            this.text = text;
            this.textColor = textColor;
            this.state = state;
            this.hideName = hideName;
        }
    }
}
