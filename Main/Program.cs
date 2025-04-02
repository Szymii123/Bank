using System;
using Bank;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Symulacja działania konta ===\n");

        // Tworzenie zwykłego konta
        Konto konto = new Konto("Jan Kowalski", 200);
        Console.WriteLine($"Utworzono Konto: {konto.Nazwa}, Bilans: {konto.Bilans} PLN\n");

        konto.Wplata(100);
        Console.WriteLine($"Po wpłacie 100 PLN, bilans: {konto.Bilans} PLN");

        konto.Wyplata(50);
        Console.WriteLine($"Po wypłacie 50 PLN, bilans: {konto.Bilans} PLN\n");

        // Tworzenie konta z debetem
        KontoPlus kontoPlus = new KontoPlus("Anna Nowak", 100, 50);
        Console.WriteLine($"Utworzono KontoPlus: {kontoPlus.Nazwa}, Bilans: {kontoPlus.Bilans} PLN (w tym limit debetowy)\n");

        kontoPlus.Wyplata(120);
        Console.WriteLine($"Po wypłacie 120 PLN, bilans: {kontoPlus.Bilans} PLN");

        try
        {
            kontoPlus.Wyplata(50);
        }
        catch (Exception ex)
        {
           Console.WriteLine($"Błąd: {ex.Message}");
        }

        kontoPlus.Wplata(100);
        Console.WriteLine($"Po wpłacie 100 PLN, bilans: {kontoPlus.Bilans} PLN, Zablokowane: {kontoPlus.Zablokowane}\n");

        // Tworzenie konta z delegacją
        KontoLimit kontoLimit = new KontoLimit("Piotr Zieliński", 150, 75);
        Console.WriteLine($"Utworzono KontoLimit: {kontoLimit.Nazwa}, Bilans: {kontoLimit.Bilans} PLN\n");

        kontoLimit.Wyplata(200);
        Console.WriteLine($"Po wypłacie 200 PLN, bilans: {kontoLimit.Bilans} PLN, Zablokowane: {kontoLimit.Zablokowane}");

        kontoLimit.Wplata(75);
        Console.WriteLine($"Po wpłacie 75 PLN, bilans: {kontoLimit.Bilans} PLN, Zablokowane: {kontoLimit.Zablokowane}\n");

        Console.WriteLine("=== Symulacja zakończona ===");
    }
}
