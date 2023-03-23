using System.Security.Cryptography.X509Certificates;
using ManipulandoArquivos1;
using static System.Net.Mime.MediaTypeNames;

internal class Program
{
    private static void Main(string[] args)
    {
        int op = 0, lines = 0;
        List<Livros> totalbooks = new List<Livros>();
        List<Livros> emprestados = new List<Livros>();
        List<Livros> iniciados = new List<Livros>();

        CheckFiles("totalbooks.txt");
        CheckFiles("emprestados.txt");
        CheckFiles("iniciados.txt");

        lines = QuantLines("totalbooks.txt");
        WriteTolist(totalbooks, "totalbooks.txt", lines / 4);
        lines = QuantLines("emprestados.txt");
        WriteTolist(emprestados, "emprestados.txt", lines / 4);
        lines = QuantLines("iniciados.txt");
        WriteTolist(iniciados, "iniciados.txt", lines / 4);

        do
        {
            Console.Clear();
            op = menu();
            switch (op)
            {
                case 1:
                    AddBook(totalbooks);
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
                    CopyBook(totalbooks, iniciados, livroinit);
                    RemoveBook(totalbooks, livroinit);
                    break;
                case 6:
                    Console.Clear();
                    PrintList(totalbooks);
                    Console.Write("\nQual livro emprestar: ");
                    var livro = Console.ReadLine();
                    CopyBook(totalbooks, emprestados, livro);
                    RemoveBook(totalbooks, livro);
                    break;
                case 7:
                    Console.Clear();
                    PrintList(emprestados);
                    Console.Write("\nQual livro foi devolvido: ");
                    var livrodev = Console.ReadLine();
                    CopyBook(emprestados, totalbooks, livrodev);
                    RemoveBook(emprestados, livrodev);
                    break;
                case 8:
                    Console.Clear();
                    PrintList(iniciados);
                    Console.Write("\nQual livro terminou: ");
                    var livroterm = Console.ReadLine();
                    CopyBook(iniciados, totalbooks, livroterm);
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

        WriteFinalFile(totalbooks, "totalbooks.txt");
        WriteFinalFile(emprestados, "emprestados.txt");
        WriteFinalFile(iniciados, "iniciados.txt");

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

        void AddBook(List<Livros> name)
        {
            Console.Clear();
            Console.WriteLine("Adicionar Livro-\n");
            Console.Write("Nome do livro: ");
            var namebook = Console.ReadLine();
            Console.Write("Autor: ");
            var autor = Console.ReadLine();
            Console.Write("Edição: ");
            var edicao = Console.ReadLine();
            Console.Write("ISBN: ");
            var isbn = Console.ReadLine();
            name.Add(new Livros(namebook.ToUpper(), autor.ToUpper(), edicao.ToUpper(),isbn.ToUpper()));
        }

        int menu()
        {
            int option;
            Console.WriteLine("Opções:\n1- Adicionar Livros\n2- Mostrar Emprestados\n3- Mostrar Prateleira\n" +
                "4- Mostrar iniciados\n5- Iniciar Leitura\n6- Emprestar Livro\n7- Devolvido\n8- Concluir leitura\n9- Sair");
            while(!int.TryParse(Console.ReadLine(),out option))
            {
                Console.Clear();
                Console.WriteLine("### DIGITE UMA DAS OPÇÕES ###\n\nOpções:\n1- Adicionar Livros\n2- Mostrar Emprestados\n3- Mostrar Prateleira\n" +
                "4- Mostrar iniciados\n5- Iniciar Leitura\n6- Emprestar Livro\n7- Devolvido\n8- Concluir leitura\n9- Sair");
            }
            return option;
        }

        void CopyBook(List<Livros> name, List<Livros> namecop, string namebook)
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

        string TotalBook(int quantbooks)
        {
            string nomes = "";
            string nome;
            for (int i = 1; i <= quantbooks; i++)
            {
                Console.Write($"{i}º Livro: ");
                nome = Console.ReadLine();
                nomes += nome + "\n";
            }
            return nomes;
        }

        void CheckFiles(string nomearquivo)
        {
            if (!File.Exists(nomearquivo))
            {
                StreamWriter wr = new(nomearquivo);
                wr.Close();
            }
        }

        void WriteFinalFile(List<Livros> name, string path)
        {
            string texto = "";
            for (int i = 0; i < name.Count; i++)
            {
                texto += name[i].NomeDoLivro + "\n";
                texto += name[i].Autor + "\n";
                texto += name[i].Edicao + "\n";
                texto += name[i].Isbn + "\n";
            }
            File.WriteAllText(path, texto);
        }

        void WriteTolist(List<Livros> name, string namefile, int lines)
        {
            StreamReader sr = new StreamReader(namefile);
            for (int i = 0; i < lines; i++)
            {
                name.Add(new Livros(sr.ReadLine(), sr.ReadLine(), sr.ReadLine(), sr.ReadLine()));
            }
            sr.Close();
        }

        int QuantLines(string file)
        {
            StreamReader sr = new StreamReader(file);
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