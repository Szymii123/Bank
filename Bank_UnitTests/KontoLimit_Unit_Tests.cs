using NUnit.Framework;
using System;
using Bank;
using NUnit.Framework.Legacy;

namespace BankTests
{
    [TestFixture]
    public class KontoLimitTests
    {
        [Test]
        public void KontoLimit_Constructor_ShouldInitializeCorrectly()
        {
            var konto = new KontoLimit("Testowy Klient", 100, 50);
            ClassicAssert.AreEqual("Testowy Klient", konto.Nazwa);
            ClassicAssert.AreEqual(150, konto.Bilans);
            ClassicAssert.IsFalse(konto.Zablokowane);
        }

        [Test]
        public void Wplata_ShouldIncreaseBalance()
        {
            var konto = new KontoLimit("Testowy Klient", 100, 50);
            konto.Wplata(50);
            ClassicAssert.AreEqual(200, konto.Bilans);
        }

        [Test]
        public void Wplata_NegativeAmount_ShouldThrowException()
        {
            var konto = new KontoLimit("Testowy Klient", 100, 50);
            ClassicAssert.Throws<ArgumentException>(() => konto.Wplata(-10));
        }

        [Test]
        public void Wyplata_ShouldDecreaseBalance()
        {
            var konto = new KontoLimit("Testowy Klient", 100, 50);
            konto.Wyplata(50);
            ClassicAssert.AreEqual(100, konto.Bilans);
        }

        [Test]
        public void Wyplata_WithinOverdraftLimit_ShouldAllowTransaction()
        {
            var konto = new KontoLimit("Testowy Klient", 100, 50);
            konto.Wyplata(140);
            ClassicAssert.IsTrue(konto.Zablokowane);
        }

        [Test]
        public void Wyplata_ExceedingOverdraftLimit_ShouldThrowException()
        {
            var konto = new KontoLimit("Testowy Klient", 100, 50);
            ClassicAssert.Throws<InvalidOperationException>(() => konto.Wyplata(160));
        }

        [Test]
        public void BlokujKonto_ShouldSetBlockedStatus()
        {
            var konto = new KontoLimit("Testowy Klient", 100, 50);
            konto.Wyplata(140);
            ClassicAssert.IsTrue(konto.Zablokowane);
        }

        [Test]
        public void OdblokujKonto_AfterDeposit_ShouldUnsetBlockedStatus()
        {
            var konto = new KontoLimit("Testowy Klient", 100, 50);
            konto.Wyplata(140);
            ClassicAssert.IsTrue(konto.Zablokowane);
            konto.Wplata(100);
            ClassicAssert.IsFalse(konto.Zablokowane);
        }

        [Test]
        public void Wplata_OnBlockedAccount_ShouldUnblockIfBalancePositive()
        {
            var konto = new KontoLimit("Testowy Klient", 100, 50);
            konto.Wyplata(140);
            ClassicAssert.IsTrue(konto.Zablokowane);
            konto.Wplata(50);
            ClassicAssert.IsFalse(konto.Zablokowane);
        }

        [Test]
        public void Wyplata_OnBlockedAccount_ShouldThrowException()
        {
            var konto = new KontoLimit("Testowy Klient", 100, 50);
            konto.Wyplata(140);
            ClassicAssert.Throws<InvalidOperationException>(() => konto.Wyplata(10));
        }
    }
}
