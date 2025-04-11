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
            // Param�tres de test
            byte slaveAdress = 0x01;
            byte commend = 0x25;

            // Appel de la m�thode ConstruireTrame
            byte[] result = protocol.ConstruireTrame(slaveAdress, commend);

            // V�rifications sur le tableau retourn�
            Assert.AreEqual(result[1] + 2, result.Length); // La taille du tableau retourn� doit �tre 6 (head, length, slaveAdress, commend, xorValue, sum)
            Assert.AreEqual(0x7E, result[0]); // La valeur de `head` doit �tre 0x7E
            Assert.AreEqual(0x04, result[1]); // La longueur totale est 4 (taille des �l�ments + sum et xorValue)
            Assert.AreEqual(0x01, result[2]); // La valeur de `slaveAdress`
            Assert.AreEqual(0x25, result[3]); // La valeur de `commend`
            Assert.AreEqual(0XDB, result[4]);
            Assert.AreEqual(0x01, result[5]);
        }

        [Test]
        public void TestConstruireTrameWithDatavide_creationTramDataOneByte_TramDataOneByte()
        {
            // Param�tres de test
            byte slaveAdress = 0x01;
            byte commend = 0x21;
            byte[] data = new byte[] {0x00 };

            // Appel de la m�thode ConstruireTrameWithData
            byte[] result = protocol.ConstruireTrameWithData(slaveAdress, commend, data);

            // V�rifications sur le tableau retourn�
            Assert.AreEqual(result[1]+2, result.Length); // La taille du tableau doit �tre 8 (head, length, slaveAdress, commend, data[], xorValue, sum)
            Assert.AreEqual(0x7E, result[0]); // La valeur de `head` doit �tre 0x7E
            Assert.AreEqual(0x05, result[1]); // La longueur totale est 7 (4 �l�ments + 3 �l�ments de data)
            Assert.AreEqual(slaveAdress, result[2]); // La valeur de `slaveAdress`
            Assert.AreEqual(commend, result[3]); // La valeur de `commend`
            Assert.AreEqual(0x00, result[4]);
            Assert.AreEqual(0xDF, result[5]);
            Assert.AreEqual(0x01, result[6]);

            // V�rification des donn�es
            //for (int i = 0; i < data.Length; i++)
            //{
            //    Assert.AreEqual(data[i], result[4 + i]); // Les donn�es doivent �tre copi�es � partir de l'indice 4
            //}

            //// V�rification du XOR et de la somme
            //byte xorExpected = (byte)(0xFF ^ slaveAdress ^ commend ^ data[0] ^ data[1] ^ data[2]);
            //byte sumExpected = (byte)(slaveAdress + commend + xorExpected + data[0] + data[1] + data[2]);
            //Assert.AreEqual(xorExpected, result[result.Length - 2]); // V�rification de xorValue
            //Assert.AreEqual(sumExpected, result[result.Length - 1]); // V�rification de sum
        }
    }
}
