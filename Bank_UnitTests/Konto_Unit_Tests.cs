using NUnit.Framework;
using System;
using Bank;
using NUnit.Framework.Legacy;

namespace BankTests
{
    [TestFixture]
    public class KontoTests
    {
        [Test]
        public void Konto_Constructor_ShouldInitializeCorrectly()
        {
            var konto = new Konto("Testowy Klient", 100);
            ClassicAssert.AreEqual("Testowy Klient", konto.Nazwa);
            ClassicAssert.AreEqual(100, konto.Bilans);
            ClassicAssert.IsFalse(konto.Zablokowane);
        }

        [Test]
        public void Wplata_ShouldIncreaseBalance()
        {
            var konto = new Konto("Testowy Klient", 100);
            konto.Wplata(50);
            ClassicAssert.AreEqual(150, konto.Bilans);
        }

        [Test]
        public void Wplata_NegativeAmount_ShouldThrowException()
        {
            var konto = new Konto("Testowy Klient", 100);
            Assert.Throws<ArgumentException>(() => konto.Wplata(-10));
        }

        [Test]
        public void Wyplata_ShouldDecreaseBalance()
        {
            var konto = new Konto("Testowy Klient", 100);
            konto.Wyplata(50);
            ClassicAssert.AreEqual(50, konto.Bilans);
        }

        [Test]
        public void Wyplata_InsufficientFunds_ShouldThrowException()
        {
            var konto = new Konto("Testowy Klient", 100);
            Assert.Throws<InvalidOperationException>(() => konto.Wyplata(200));
        }

        [Test]
        public void BlokujKonto_ShouldSetBlockedStatus()
        {
            var konto = new Konto("Testowy Klient", 100);
            konto.BlokujKonto();
            ClassicAssert.IsTrue(konto.Zablokowane);
        }

        [Test]
        public void OdblokujKonto_ShouldUnsetBlockedStatus()
        {
            var konto = new Konto("Testowy Klient", 100);
            konto.BlokujKonto();
            konto.OdblokujKonto();
            ClassicAssert.IsFalse(konto.Zablokowane);
        }

        [Test]
        public void Wplata_OnBlockedAccount_ShouldThrowException()
        {
            var konto = new Konto("Testowy Klient", 100);
            konto.BlokujKonto();
            Assert.Throws<InvalidOperationException>(() => konto.Wplata(50));
        }

        [Test]
        public void Wyplata_OnBlockedAccount_ShouldThrowException()
        {
            var konto = new Konto("Testowy Klient", 100);
            konto.BlokujKonto();
            Assert.Throws<InvalidOperationException>(() => konto.Wyplata(50));
        }
    }
}
