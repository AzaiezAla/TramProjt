using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TramProjt
{
    public class ProtocolX
    {
        byte head = 0x7E;
        //byte length = 0x04;

        public byte[] ConstruireTrame(byte slaveAdress, byte commend)
        {

            //byte slaveAdress = slaveAdress;
            //byte commend = commend;
            //byte length = 0x7E;
            byte xorValue = (byte)(0xFF ^ slaveAdress ^ commend);

            //Console.WriteLine($"XOR: 0x{xorValue:X2}");

            byte sum = (byte)(slaveAdress + commend + xorValue);  // Addition des 3 valeurs

            byte length = (byte)(Marshal.SizeOf(slaveAdress) + Marshal.SizeOf(commend) + Marshal.SizeOf(xorValue)+ Marshal.SizeOf(sum));

            byte[] resultat = { this.head, length, slaveAdress, commend, xorValue, sum };



            return (resultat);
        }

        //public byte[] ConstruireTrameWitheDta(byte slaveAdress, byte commend , byte[] data)
        //{

        //    //byte slaveAdress = slaveAdress;
        //    //byte commend = commend;
        //    //byte length = 0x7E;

        //    byte xorValue = (byte)(0xFF ^ slaveAdress ^ commend ^ data);

        //    //Console.WriteLine($"XOR: 0x{xorValue:X2}");

        //    byte sum = (byte)(slaveAdress + commend + xorValue + data);  // Addition des 3 valeurs

        //    byte length = (byte)(Marshal.SizeOf(slaveAdress) + Marshal.SizeOf(commend)+ Marshal.SizeOf(data) + Marshal.SizeOf(xorValue) + Marshal.SizeOf(sum));

        //    byte[] resultat = { this.head, length, slaveAdress, commend, data ,xorValue, sum };



        //    return (resultat);
        //}

        public byte[] ConstruireTrameWithData(byte slaveAdress, byte commend, byte[] data)
        {
            // Calcul du XOR de tous les éléments
            byte xorValue = 0xFF;  // Initialisation avec 0xFF
            foreach (byte b in data)
            {
                xorValue ^= b;  // XOR sur chaque élément de data
            }
            xorValue = (byte)(slaveAdress ^ xorValue ^ commend); // XOR avec slaveAdress et commend

            // Calcul de la somme
            byte sum = (byte)(slaveAdress + commend + xorValue);
            foreach (byte b in data)
            {
                sum = (byte)(sum + b);  // Addition de chaque élément de data
            }

            // Calcul de la longueur totale de la trame (tous les éléments, y compris le tableau de données)
            byte length = (byte)(sizeof(byte) * 4 + data.Length); // 4 éléments (head, slaveAdress, commend, xorValue) + taille du tableau data

            // Création de la trame avec une taille suffisante pour contenir tous les éléments
            byte[] resultat = new byte[4 + data.Length + 2];  // 4 pour head, slaveAdress, commend, length + 2 pour xorValue et sum
            resultat[0] = this.head;
            resultat[1] = length;
            resultat[2] = slaveAdress;
            resultat[3] = commend;

            // Copier les données à partir de l'indice 4
            Array.Copy(data, 0, resultat, 4, data.Length);

            // Mettre xorValue à la fin avant la somme
            resultat[resultat.Length - 2] = xorValue;

            // Mettre sum à la fin
            resultat[resultat.Length - 1] = sum;

            return resultat;
        }


    }
}