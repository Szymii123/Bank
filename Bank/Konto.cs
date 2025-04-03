namespace Bank
{
    public class Konto
    {
        private string klient;  //nazwa klienta
        private decimal bilans;  //aktualny stan środków na koncie
        private bool zablokowane = false; //stan konta

        public Konto(string klient, decimal bilansNaStart = 0)
        {
            this.klient = klient;
            this.bilans = bilansNaStart;
        }

        public string Nazwa
        {
            get { return klient; }
        }

        public decimal Bilans
        {
            get { return bilans; }
        }

        public bool Zablokowane
        {
            get { return zablokowane; }
        }

        public void Wplata(decimal kwota)
        {
			//if (zablokowane) throw new InvalidOperationException("Konto jest zablokowane.");
			if (kwota <= 0) throw new ArgumentException("Kwota musi być większa niż 0.");
            bilans += kwota;
        }

        public void Wyplata(decimal kwota)
        {
            if (zablokowane) throw new InvalidOperationException("Konto jest zablokowane.");
            if (kwota <= 0) throw new ArgumentException("Kwota musi być większa niż 0.");
            if (bilans < kwota) throw new InvalidOperationException("Brak wystarczających środków.");
            bilans -= kwota;
        }

        public void BlokujKonto()
        {
            zablokowane = true;
        }
        public void OdblokujKonto()
        {
            zablokowane = false;
        }
    }
}
