using System;
using System.Collections.Generic;

namespace LR.Models
{
    public class TextValue4
    {
        string text;
        string value;
        public TextValue4(string text, string value)
        {
            this.Text = text;
            this.Value = value;
        }
        public string Text { get; set; }
        public string Value { get; set; }
    }
 
}
