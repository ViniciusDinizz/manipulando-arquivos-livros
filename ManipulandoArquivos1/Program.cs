using System.Security.Cryptography.X509Certificates;
using ManipulandoArquivos1;
using static System.Net.Mime.MediaTypeNames;

internal class Program
{
    private static void Main(string[] args)
    {
        int op = 0, linhas = 0;
        List<Livros> totalbooks = new List<Livros>();
        List<Livros> emprestados = new List<Livros>();
        List<Livros> iniciados = new List<Livros>();

        VerificarArquivos("totalbooks.txt");
        VerificarArquivos("emprestados.txt");
        VerificarArquivos("iniciados.txt");

        linhas = QuantlLinhas("totalbooks.txt");
        GravarListaInicio(totalbooks, "totalbooks.txt", linhas / 2);
        linhas = QuantlLinhas("emprestados.txt");
        GravarListaInicio(emprestados, "emprestados.txt", linhas / 2);
        linhas = QuantlLinhas("iniciados.txt");
        GravarListaInicio(iniciados, "iniciados.txt", linhas / 2);

        do
        {
            Console.Clear();
            op = menu();
            switch (op)
            {
                case 1:
                    AdicionarLivro(totalbooks);
                    Console.Clear();
                    break;
                case 2:
                    PrintList(emprestados);
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 3:
                    PrintList(totalbooks);
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 4:
                    PrintList(iniciados);
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 5:
                    Console.Clear();
                    PrintList(totalbooks);
                    Console.Write("\nQual livro vai iniciar: ");
                    var livroinit = Console.ReadLine();
                    CopiarLivro(totalbooks, iniciados, livroinit);
                    RemoveBook(totalbooks, livroinit);
                    break;
                case 6:
                    Console.Clear();
                    PrintList(totalbooks);
                    Console.Write("\nQual livro emprestar: ");
                    var livro = Console.ReadLine();
                    CopiarLivro(totalbooks, emprestados, livro);
                    RemoveBook(totalbooks, livro);
                    break;
                case 7:
                    Console.Clear();
                    PrintList(emprestados);
                    Console.Write("\nQual livro foi devolvido: ");
                    var livrodev = Console.ReadLine();
                    CopiarLivro(emprestados, totalbooks, livrodev);
                    RemoveBook(emprestados, livrodev);
                    break;
                case 8:
                    Console.Clear();
                    PrintList(iniciados);
                    Console.Write("\nQual livro terminou: ");
                    var livroterm = Console.ReadLine();
                    CopiarLivro(iniciados, totalbooks, livroterm);
                    RemoveBook(iniciados, livroterm);
                    Console.WriteLine();
                    break;
                case 9:
                    Console.WriteLine("Até logo");
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

        } while (op != 9);

        EscreverArquivoFinal(totalbooks, "totalbooks.txt");
        EscreverArquivoFinal(emprestados, "emprestados.txt");
        EscreverArquivoFinal(iniciados, "iniciados.txt");

        void PrintList(List<Livros> name)
        {
            if (name.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("Sem livros.");
            }
            else
            {
                Console.Clear();
                for (int i = 0; i < name.Count; i++)
                {
                    Console.WriteLine(name[i].ToString());
                }
            }
        }

        void RemoveBook(List<Livros> name, string namebook)
        {
            int cont = 0;
            for (int i = 0; i < name.Count; i++)
            {
                if (name[i].NomeDoLivro == namebook.ToUpper())
                {
                    name.Remove(name[i]);
                }
                else
                {
                    cont++;
                }
            }
            if (cont++ == name.Count)
            {
                Console.WriteLine("Livro não encontrado.");
            }
        }

        void AdicionarLivro(List<Livros> name)
        {
            Console.Clear();
            Console.WriteLine("Adionar Livro-\n");
            Console.Write("Nome do livro: ");
            var namebook = Console.ReadLine();
            Console.Write("Autor: ");
            var autor = Console.ReadLine();
            name.Add(new Livros(namebook.ToUpper(), autor.ToUpper()));
        }

        int menu()
        {
            int option;
            Console.WriteLine("Opções:\n1- Adicionar Livros\n2- Mostrar Emprestados\n3- Mostrar Prateleira\n" +
                "4- Mostrar iniciados\n5- Iniciar Leitura\n6- Emprestar Livro\n7- Devolvido\n8- Concluir leitura\n9- Sair");
            option = int.Parse(Console.ReadLine());
            return option;
        }

        void CopiarLivro(List<Livros> name, List<Livros> namecop, string namebook)
        {
            int cont = 0;
            for (int i = 0; i < name.Count; i++)
            {
                if (name[i].NomeDoLivro == namebook.ToUpper())
                {
                    namecop.Add(name[i]);
                }
                else
                {
                    cont++;
                }
            }
            if (cont == name.Count)
            {
                Console.Write("Livro não encontrado...");
                Console.ReadKey();
            }
        }

        string LivrosTotais(int quantlivros)
        {
            string nomes = "";
            string nome;
            for (int i = 1; i <= quantlivros; i++)
            {
                Console.Write($"{i}º Livro: ");
                nome = Console.ReadLine();
                nomes += nome + "\n";
            }
            return nomes;
        }

        void VerificarArquivos(string nomearquivo)
        {
            if (!File.Exists(nomearquivo))
            {
                StreamWriter wr = new(nomearquivo);
                wr.Close();
            }
        }

        void EscreverArquivoFinal(List<Livros> name, string path)
        {
            string texto = "";
            for (int i = 0; i < name.Count; i++)
            {
                texto += name[i].NomeDoLivro + "\n";
                texto += name[i].Autor + "\n";
            }
            File.WriteAllText(path, texto);
        }

        void GravarListaInicio(List<Livros> nome, string nomearqu, int linhas)
        {
            StreamReader sr = new StreamReader(nomearqu);
            for (int i = 0; i < linhas; i++)
            {
                nome.Add(new Livros(sr.ReadLine(), sr.ReadLine()));
            }
            sr.Close();
        }

        int QuantlLinhas(string arquivo)
        {
            StreamReader sr = new StreamReader(arquivo);
            int cont = 0;
            while (sr.ReadLine() != null)
            {
                cont++;
            }
            sr.Close();
            return cont;
        }

    }
}