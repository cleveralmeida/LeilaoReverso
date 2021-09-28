using System;
using System.Collections.Generic;

namespace LR.Models
{
    public class TextValue
    {
        string text;
        int value;
        public TextValue(string text, int value)
        {
            this.Text = text;
            this.Value = value;
        }
        public string Text { get; set; }
        public int Value { get; set; }
    }
}
