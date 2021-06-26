using System;
namespace Bank
{
    public class Conta
    {

        private int Agencia { get; set; }
        private int NroConta { get; set; }
        private string Nome { get; set; }
        private double Saldo { get; set; }
        private double Credito { get; set; }
        private TipoConta TipoConta { get; set; }
        private string ChavePix { get; set; }

        public int nroAgencia
        {
            get => Agencia;
        }

        public int nroConta
        {
            get => NroConta;
        }

        public string nome
        {
            get => Nome;
        }

        public string chavePix
        {
            get => ChavePix;
        }

        public Conta(TipoConta tipoConta, double credito, string nome, int agencia, int nroconta, string chavePix)
        {

            this.TipoConta = tipoConta;
            this.ChavePix = chavePix;
            this.Credito = credito;
            this.Nome = nome;
            this.Agencia = agencia;
            this.NroConta = nroconta;
        }

        public bool Sacar(double valorSaque)
        {
            if (this.Saldo - valorSaque < (this.Credito * -1))
            {
                Console.WriteLine("Saldo Insuficiente!");
                return false;
            }

            this.Saldo -= valorSaque;

            Console.WriteLine("Saldo autal da conta de {0} é {1}", this.Nome, this.Saldo);
            return true;
        }

        public void Depositar(double valorDeposito)
        {
            this.Saldo += valorDeposito;

            Console.WriteLine("Saldo atual da conta de {0} é {1}", this.Nome, this.Saldo);
        }

        public void Transfirir(double valorTransferencia, Conta contaDestino)
        {
            if (this.Sacar(valorTransferencia))
            {
                contaDestino.Depositar(valorTransferencia);
                Console.WriteLine();
                Console.WriteLine("Destinatário:");
                Console.WriteLine("Nome: {0} ; Agencia: {1} ; Conta: {2}", contaDestino.Nome, contaDestino.Agencia, contaDestino.NroConta);
                Console.WriteLine();
                Console.WriteLine("Remetente:");
                Console.WriteLine("Nome: {0} ; Agencia: {1} ; Conta: {2}", this.Nome, this.Agencia, this.NroConta);

                Console.WriteLine("*****************************\n");
                Console.WriteLine("Valor Transferência: R$ {0}", valorTransferencia);

            }
        }

        public void PixCadastrar(string chavepix)
        {
            this.ChavePix = chavepix;
        }

        public void Pix(double valorPix, Conta contaDestino)
        {
            if (this.Sacar(valorPix))
            {
                contaDestino.Depositar(valorPix);

                Console.WriteLine();
                Console.WriteLine("Destinatário:");
                Console.WriteLine("Nome: {0}", contaDestino.Nome);
                Console.WriteLine();
                Console.WriteLine("Remetente:");
                Console.WriteLine("Nome: {0}", this.Nome);
                Console.WriteLine("*****************************\n");
                Console.WriteLine("Valor Pix: R$ {0}", valorPix);
                Console.WriteLine("**valor de pix gratis sem debito do credito***");
            }
        }

        public void Funcaosobrecredito(double valor)
        {
            this.Credito += valor;
        }

        public override string ToString()
        {

            string retorno = "";
            retorno += "Nome: " + this.Nome + Environment.NewLine +
           "Agencia: " + this.Agencia + Environment.NewLine +
           "Nro. Conta: " + this.NroConta + Environment.NewLine +
           "Saldo: " + this.Saldo + Environment.NewLine +
           "Credito: " + this.Credito + Environment.NewLine +
           "ChavePix: " + this.ChavePix + Environment.NewLine +
           "Tipo Conta: " + this.TipoConta + Environment.NewLine;

            return retorno;


        }



    }
}