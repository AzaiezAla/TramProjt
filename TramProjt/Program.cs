//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TramProjt;

namespace projetProduction1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Appel à la méthode ConstruireTrame
            ProtocolX protocol = new ProtocolX();
            byte slaveAdress = 0x01;
            byte commend = 0x25;

            byte[] trame = protocol.ConstruireTrame(slaveAdress, commend);
            Console.WriteLine("Trame construite : " + BitConverter.ToString(trame));

            byte distination = 1;
            byte commende = 0x21;
            byte[] data1 = { 0x00 };
            byte[] trameWitheData = protocol.ConstruireTrameWithData(distination, commende, data1);
            Console.WriteLine("Trame withe data construite  : " + BitConverter.ToString(trameWitheData));

            byte nbrcommend = 0x86;
            byte[] myBytes = { 0x01, 0x00, 0x00, 0x00, 0x31,0x01 };
            byte[] trameWitheData2 = protocol.ConstruireTrameWithData(distination, nbrcommend, myBytes);
            Console.WriteLine("Trame withe data construite  : " + BitConverter.ToString(trameWitheData2));

        }
    }

}
