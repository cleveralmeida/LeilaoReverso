using System;
using System.Collections.Generic;

namespace LR.Models
{
    public class QuestionavioVM
    {
        int _numero;
        string _questao;
        public QuestionavioVM(int numero, string questao)
        {
            this._numero = numero;
            this._questao = questao;
        }
        public int Numero { get; set; }
        public string Questao { get; set; }

    }
}
