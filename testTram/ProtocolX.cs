using NUnit.Framework;
using System;

namespace TramProjt.Tests
{
    public class ProtocolXTests
    {
        private ProtocolX protocol;

        [SetUp]
        public void Setup()
        {
            protocol = new ProtocolX();
        }

        [Test]
        public void TestConstruireTrame_creationtram_Tramesansdata()
        {
            // Paramètres de test
            byte slaveAdress = 0x01;
            byte commend = 0x25;

            // Appel de la méthode ConstruireTrame
            byte[] result = protocol.ConstruireTrame(slaveAdress, commend);

            // Vérifications sur le tableau retourné
            Assert.AreEqual(result[1] + 2, result.Length); // La taille du tableau retourné doit être 6 (head, length, slaveAdress, commend, xorValue, sum)
            Assert.AreEqual(0x7E, result[0]); // La valeur de `head` doit être 0x7E
            Assert.AreEqual(0x04, result[1]); // La longueur totale est 4 (taille des éléments + sum et xorValue)
            Assert.AreEqual(0x01, result[2]); // La valeur de `slaveAdress`
            Assert.AreEqual(0x25, result[3]); // La valeur de `commend`
            Assert.AreEqual(0XDB, result[4]);
            Assert.AreEqual(0x01, result[5]);
        }

        [Test]
        public void TestConstruireTrameWithDatavide_creationTramDataOneByte_TramDataOneByte()
        {
            // Paramètres de test
            byte slaveAdress = 0x01;
            byte commend = 0x21;
            byte[] data = new byte[] {0x00 };

            // Appel de la méthode ConstruireTrameWithData
            byte[] result = protocol.ConstruireTrameWithData(slaveAdress, commend, data);

            // Vérifications sur le tableau retourné
            Assert.AreEqual(result[1]+2, result.Length); // La taille du tableau doit être 8 (head, length, slaveAdress, commend, data[], xorValue, sum)
            Assert.AreEqual(0x7E, result[0]); // La valeur de `head` doit être 0x7E
            Assert.AreEqual(0x05, result[1]); // La longueur totale est 7 (4 éléments + 3 éléments de data)
            Assert.AreEqual(slaveAdress, result[2]); // La valeur de `slaveAdress`
            Assert.AreEqual(commend, result[3]); // La valeur de `commend`
            Assert.AreEqual(0x00, result[4]);
            Assert.AreEqual(0xDF, result[5]);
            Assert.AreEqual(0x01, result[6]);

            // Vérification des données
            //for (int i = 0; i < data.Length; i++)
            //{
            //    Assert.AreEqual(data[i], result[4 + i]); // Les données doivent être copiées à partir de l'indice 4
            //}

            //// Vérification du XOR et de la somme
            //byte xorExpected = (byte)(0xFF ^ slaveAdress ^ commend ^ data[0] ^ data[1] ^ data[2]);
            //byte sumExpected = (byte)(slaveAdress + commend + xorExpected + data[0] + data[1] + data[2]);
            //Assert.AreEqual(xorExpected, result[result.Length - 2]); // Vérification de xorValue
            //Assert.AreEqual(sumExpected, result[result.Length - 1]); // Vérification de sum
        }
    }
}
