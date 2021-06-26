using System;
using System.Collections.Generic;

namespace Bank
{
    class Program
    {

        static List<Conta> ListaConta = new List<Conta>();

        static void Main(string[] args)
        {

            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {

                switch (opcaoUsuario)
                {
                    case "1":
                        InserirConta();
                        break;
                    case "2":
                        ConsultarConta();
                        break;
                    case "3":
                        Sacar();
                        break;
                    case "4":
                        Depositar();
                        break;
                    case "5":
                        Tranferir();
                        break;
                    case "6":
                        Pix();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    case "A":
                        ListarContasAdmin();
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                opcaoUsuario = ObterOpcaoUsuario();
            }
        }

        private static void ListarContasAdmin()
        {
            if (ListaConta.Count > 0)
            {
                foreach (var item in ListaConta)
                {
                    Console.WriteLine();
                    Console.WriteLine(item.ToString());
                    Console.WriteLine();
                }
                return;
            }

            Console.WriteLine("Lista está vazia !!!!!");
        }

        private static void Pix()
        {
            Console.WriteLine("Pix entre conta e agencias");

            Console.WriteLine("Digite a chave pix de origem: ");
            string chavePix_o = Convert.ToString(Console.ReadLine());

            Console.WriteLine("Digite a chave pix de destino: ");
            string chavePix_d = Convert.ToString(Console.ReadLine());

            Console.WriteLine(".....");

            Console.WriteLine("Digite o valor do pix: ");
            double valorPix = double.Parse(Console.ReadLine());


            foreach (var origem in ListaConta)
            {
                // achar conta origem
                if (origem.chavePix == chavePix_o)
                {
                    foreach (var destino in ListaConta)
                    {
                        if (destino.chavePix == chavePix_d)
                        {
                            origem.Pix(valorPix, destino);
                            return;
                        }
                    }

                    return;
                }
            }
            Console.WriteLine("Erro na operação chaves pix não encontradas");
            Console.WriteLine(".....");

            Console.WriteLine("Cadastrar Chave Pix? [Y|N]: ");
            String resposta = Convert.ToString(Console.ReadLine()).ToUpper();

            if (resposta == "Y")
            {
                Console.WriteLine("Digite  a agência: ");
                int agencia = int.Parse(Console.ReadLine());
                Console.WriteLine("Digite o numero da conta: ");
                int nroconta = int.Parse(Console.ReadLine());

                // verificar se conta já não existe se existir apresentar informação + menu
                foreach (var item in ListaConta)
                {
                    if (item.nroAgencia == agencia && item.nroConta == nroconta)
                    {
                        Console.WriteLine("Digite a  Chave Pix: ");
                        String chavepix = Convert.ToString(Console.ReadLine());
                        item.PixCadastrar(chavepix);
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Conta não encontrada !!!! ");
                        Console.WriteLine("Inserir nova conta? [Y|N]: ");
                        String conta = Convert.ToString(Console.ReadLine()).ToUpper();
                        if (conta == "Y")
                        {
                            InserirConta();
                        }

                        return;

                    }

                }


            }

            return;
        }

        private static void Tranferir()
        {

            Console.WriteLine("Transferência entre conta e agencias");

            Console.WriteLine("Digite a agencia de origem: ");
            int agencia_O = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o numero da conta de origem: ");
            int nroconta_O = int.Parse(Console.ReadLine());

            Console.WriteLine(".....");

            Console.WriteLine("Digite a agencia de destino: ");
            int agencia_D = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o numero da conta de destino: ");
            int nroconta_D = int.Parse(Console.ReadLine());

            Console.WriteLine(".....");

            Console.WriteLine("Digite o valor da à transferir: ");
            double valorTransferencia = double.Parse(Console.ReadLine());


            foreach (var origem in ListaConta)
            {
                // achar conta origem
                if (origem.nroAgencia == agencia_O && origem.nroConta == nroconta_O)
                {
                    foreach (var destino in ListaConta)
                    {
                        if (destino.nroAgencia == agencia_D && destino.nroConta == nroconta_D)
                        {
                            origem.Transfirir(valorTransferencia, destino);
                            if (origem.nroAgencia != destino.nroAgencia)
                            {
                                origem.Funcaosobrecredito(-5);
                                Console.WriteLine();
                                Console.WriteLine("Debito no Credito por transferências entre agências diferêntes: R$ {0}", 15);
                                return;
                            }
                            Console.WriteLine("*** sem debito do credito ***");
                            return;
                        }
                    }

                    Console.WriteLine("Erro na operação conferir se contas estão corretas !!!!");
                    return;
                }
            }

            Console.WriteLine("Erro na operação conferir se contas estão corretas !!!!");
        }

        private static void Depositar()
        {
            Console.WriteLine("Depositar na conta");

            Console.WriteLine("Digite a agencia: ");
            int agencia = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o numero da conta: ");
            int nroconta = int.Parse(Console.ReadLine());

            foreach (var item in ListaConta)
            {
                if (item.nroAgencia == agencia && item.nroConta == nroconta)
                {
                    Console.WriteLine("Digite o valor para deposito: ");
                    double valordeposito = double.Parse(Console.ReadLine());

                    item.Depositar(valordeposito);
                    item.Funcaosobrecredito(150);
                    Console.WriteLine();
                    Console.WriteLine("Acréscimo no credito por deposito de: R$ {0}", 150);
                    Console.WriteLine();
                    return;
                }

            }

            Console.WriteLine("Conta não encontrada !!!!");

        }

        private static void Sacar()
        {
            Console.WriteLine("Saque na conta");

            Console.WriteLine("Digite a agencia: ");
            int agencia = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o numero da conta: ");
            int nroconta = int.Parse(Console.ReadLine());

            foreach (var item in ListaConta)
            {
                if (item.nroAgencia == agencia && item.nroConta == nroconta)
                {
                    Console.WriteLine("Digite o valor para saque: ");
                    double valorSaque = double.Parse(Console.ReadLine());

                    item.Sacar(valorSaque);
                    item.Funcaosobrecredito(-7);
                    Console.WriteLine();
                    Console.WriteLine("Debito no Credito por saque: R$ {0}", 10);
                    Console.WriteLine();
                    return;
                }

            }

            Console.WriteLine("Conta não encontrada !!!!");
        }

        private static void ConsultarConta()
        {
            Console.WriteLine("Consultar conta");

            Console.WriteLine("Digite a agencia: ");
            int agencia = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o numero da conta: ");
            int nroconta = int.Parse(Console.ReadLine());

            foreach (var item in ListaConta)
            {
                if (item.nroAgencia == agencia && item.nroConta == nroconta)
                {
                    Console.WriteLine();
                    Console.WriteLine(item.ToString());
                    Console.WriteLine();
                    return;
                }
            }

            Console.WriteLine("Conta não encontrada !!!!");

        }

        private static void InserirConta()
        {
            Console.WriteLine("Inserir nova conta");

            Console.WriteLine("Digite  a agencia: ");
            int agencia = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite o numero da conta: ");
            int nroconta = int.Parse(Console.ReadLine());

            // verificar se conta já não existe se existir apresentar informação + menu
            foreach (var item in ListaConta)
            {
                if (item.nroAgencia == agencia && item.nroConta == nroconta)
                {
                    Console.WriteLine("Nome Cliente: {0} ; Agencia: {1}; Nro Conta: {2} ", item.nome, item.nroAgencia, item.nroConta);
                    return;
                }

            }

            Console.WriteLine("Digite o Nome Completo: ");
            string nome = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Digite o tipo da conta entre pessoa fisia = 1 ou juridica = 2: ");
            int tipoConta = int.Parse(Console.ReadLine());


            Console.WriteLine("Cadastrar chavePix: ( *** EMAIL | CPF | TELEFONE) *** :");
            string chavePixCadastro = Convert.ToString(Console.ReadLine());

            var novaconta = new Conta((TipoConta)tipoConta, 500, nome, agencia, nroconta, chavePixCadastro);


            Console.WriteLine("Por favor efetue um deposito para iniicalizar a conta: ");
            double deposito = double.Parse(Console.ReadLine());

            novaconta.Depositar(deposito);
            novaconta.Funcaosobrecredito(150); // cada deposito


            ListaConta.Add(novaconta);

        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("****** YOUR BANK ******");
            Console.WriteLine("Informe a opção desejada:");
            Console.WriteLine("1 - Inserir Conta");
            Console.WriteLine("2 - Consultar Conta");
            Console.WriteLine("3 - Sacar");
            Console.WriteLine("4 - Depositar");
            Console.WriteLine("5 - Transferir");
            Console.WriteLine("6 - Pix");
            Console.WriteLine("C - Limpar Tela");
            Console.WriteLine("X - Sair");
            Console.WriteLine();

            Console.Write("Opção: ");

            string opcao = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcao;
        }

    }
}
