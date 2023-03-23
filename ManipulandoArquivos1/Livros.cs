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
        public string Edicao { get; set; }
        public string Isbn { get; set; }

        public Livros(string nome, string autor, string edicao, string isbn) 
        {
            NomeDoLivro = nome;
            Autor = autor;
            Edicao = edicao;
            Isbn = isbn;

        }

        public override string ToString()
        {
            return $"Livro: {NomeDoLivro} | Autor: {Autor} | Edição: {Edicao} | Isbn: {Isbn}";
        }
    }
}
