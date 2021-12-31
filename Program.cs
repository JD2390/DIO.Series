using System;

namespace DIO.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            Tipo a = OpcaoTipo();

            string opcaoUsuario = ObterOpcaoUsuario(a);

			while (opcaoUsuario.ToUpper() != "X")
			{
                //fazendo esse if porque o programa simplesmente não voltava para a tela de escolha
                if (opcaoUsuario!="1" && opcaoUsuario!="2" && opcaoUsuario!="3" && opcaoUsuario!="4" && opcaoUsuario!="5" && opcaoUsuario!="C" && opcaoUsuario!="X")
                {
                    Console.Clear();
                    Console.WriteLine("Opção errada, digite novamente.");
                }else{
                    switch (opcaoUsuario)
                    {
                        case "1":
                            ListarSeries(a);
                            break;
                        case "2":
                            InserirSerie(a);
                            break;
                        case "3":
                            AtualizarSerie(a);
                            break;
                        case "4":
                            ExcluirSerie(a);
                            break;
                        case "5":
                            VisualizarSerie(a);
                            break;
                        case "C":
                            Console.Clear();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                opcaoUsuario = ObterOpcaoUsuario(a);
			}

			Console.WriteLine("Obrigado por utilizar nossos serviços.");
			Console.ReadLine();
        }

        private static void ExcluirSerie(Tipo tipo)
		{
            Console.WriteLine($"Excluir {tipo}");
            var lista = repositorio.Lista();
			if (lista.Count == 0)
			{
				Console.WriteLine("Nenhum Título cadastrado.");
				return;
			}
			Console.Write($"Digite o id da(o) {tipo}: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			repositorio.Exclui(indiceSerie);
		}

        private static void VisualizarSerie(Tipo tipo)
		{
            Console.WriteLine($"Tipo de mídia a ser visualizada - {tipo}");
            var lista = repositorio.Lista();
			if (lista.Count == 0)
			{
				Console.WriteLine("Nenhum Título cadastrado.");
				return;
			}            
			Console.Write($"Digite o id: ");
			int indiceSerie = int.Parse(Console.ReadLine());
            var serie = repositorio.RetornaPorId(indiceSerie);
            Console.WriteLine(serie);            
			
		}

        private static void AtualizarSerie(Tipo tipo)
		{
            Console.WriteLine($"Tipo de mídia a ser atualizada - {tipo}");
            var lista = repositorio.Lista();
			if (lista.Count == 0)
			{
				Console.WriteLine("Nenhum Título cadastrado.");
				return;
			}            
			Console.Write("Digite o Id: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			Console.Write("Digite o novo gênero entre as opções acima: ");
			int entradaGenero = int.Parse(Console.ReadLine());

			Console.Write($"Digite o novo Título: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o novo Ano de Lançamento: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write($"Digite a nova Descrição: ");
			string entradaDescricao = Console.ReadLine();

			Serie atualizaSerie = new Serie(id: indiceSerie,
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			repositorio.Atualiza(indiceSerie, atualizaSerie);
		}
        private static void ListarSeries(Tipo tipo)
		{
			Console.WriteLine($"Listar {tipo}s");

			var lista = repositorio.Lista();

			if (lista.Count == 0)
			{
				Console.WriteLine("Nenhum Título cadastrado.");
				return;
			}

			foreach (var serie in lista)
			{
                var excluido = serie.retornaExcluido();
                
				Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Excluído*" : ""));
			}
		}

        private static void InserirSerie(Tipo tipo)
		{
			Console.WriteLine($"Inserir {tipo}");

			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			Console.Write("Digite o gênero entre as opções acima: ");
			int entradaGenero = int.Parse(Console.ReadLine());

			Console.Write($"Digite o Título da(o) {tipo}: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de Lançamento: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write($"Digite a Descrição da(o) {tipo}: ");
			string entradaDescricao = Console.ReadLine();

			Serie novaSerie = new Serie(id: repositorio.ProximoId(),
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			repositorio.Insere(novaSerie);
		}
        
        //escolha para o tipo de Menu que irá aparecer no terminal
        private static Tipo OpcaoTipo(){
            string escolha=null;
            while (escolha!="1" && escolha!="2")
            {
                Console.WriteLine("Qual o tipo de repositorio deseja escolher?");
                Console.WriteLine("1- Séries");
			    Console.WriteLine("2- Filmes");
                escolha = Console.ReadLine();
                if (escolha!="1" && escolha!="2")
                {
                    Console.Clear();
                    Console.WriteLine("Opção errada, digite novamente.");
                }
            }
            
            Tipo tipo;
            if (escolha=="1")
            {
                tipo=Tipo.Série;
            }else{
                tipo=Tipo.Filme;
            }
            return tipo;
        }
        private static string ObterOpcaoUsuario(Tipo tipo)
		{
			Console.WriteLine();
			Console.WriteLine($"DIO {tipo}s a seu dispor!!!");
			Console.WriteLine("Informe a opção desejada:");

			Console.WriteLine($"1- Listar {tipo}s");
			Console.WriteLine($"2- Inserir {tipo}");
			Console.WriteLine($"3- Atualizar {tipo}");
			Console.WriteLine($"4- Excluir {tipo}");
			Console.WriteLine($"5- Visualizar {tipo}");
			Console.WriteLine("C- Limpar Tela");
			Console.WriteLine("X- Sair");
			Console.WriteLine();

			string opcaoUsuario = Console.ReadLine().ToUpper();         

			Console.WriteLine();
			return opcaoUsuario;
		}
    }
}
