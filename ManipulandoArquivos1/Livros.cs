using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManipulandoArquivos1
{
    internal class Livros
    {
        public string NomeDoLivro { get; set; }
        public string Autor { get; set; }
        public int PáginaParou { get; set; }

        public Livros(string nome, string autor) 
        {
            NomeDoLivro = nome;
            Autor = autor;
        }

        public override string ToString()
        {
            return $"Livro: {NomeDoLivro} | Autor: {Autor}";
        }
    }
}
