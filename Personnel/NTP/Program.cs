using System;
using System.Net;
using System.Net.Sockets;
namespace NTP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string ntpServer = "0.ch.pool.ntp.org";

            byte[] ntpData = new byte[48];
            ntpData[0] = 0x1B; //LI = 0 (no warning), VN = 3 (IPv4 only), Mode = 3 (Client Mode)

            IPEndPoint ntpEndpoint = new IPEndPoint(Dns.GetHostAddresses(ntpServer)[0], 123);

            UdpClient ntpClient = new UdpClient();
            ntpClient.Connect(ntpEndpoint);

            ntpClient.Send(ntpData, ntpData.Length);

            ntpData = ntpClient.Receive(ref ntpEndpoint);

            DateTime ntpTime = ToDateTime(ntpData);

            //Console.WriteLine("Heure actuelle : " + ntpTime.ToString());

            //B1
            Console.WriteLine("- " + ntpTime.ToLongDateString());
            Console.WriteLine("- " + ntpTime.ToString("dd/MM/yyyy HH:mm:ss"));
            Console.WriteLine("- " + ntpTime.ToShortDateString());

            //C1
            //Console.WriteLine((DateTime.Now - ntpTime).TotalSeconds);
            TimeSpan timeDiff = DateTime.Now - ntpTime;
            Console.WriteLine("Différence de temps : " + timeDiff.TotalSeconds + " secondes");
            //C2
            //Console.WriteLine(ntpTime + (DateTime.Now - ntpTime));
            DateTime localTime = ntpTime.Add(timeDiff);
            Console.WriteLine("Heure locale : " + localTime.ToString());
            Console.WriteLine();
            //C3
            //Console.WriteLine(DateTime.Now - (DateTime.Now - ntpTime));
            DateTime utcTime = localTime.ToUniversalTime();
            Console.WriteLine("Heure UTC : " + utcTime.ToString());
            //C4
            //Console.WriteLine(ntpTime + (DateTime.Now - ntpTime));
            localTime = utcTime.ToLocalTime();
            Console.WriteLine("Heure locale (à partir de l'heure UTC) : " + localTime.ToString());
            Console.WriteLine();

            //C5
            DateTime gmtTime = localTime.ToUniversalTime().AddHours(-1); // Suisse en GMT+1
            Console.WriteLine("Heure GMT : " + gmtTime.ToString());

            //C6
            localTime = gmtTime.ToUniversalTime().AddHours(1); // Suisse en GMT+1
            Console.WriteLine("Heure local : " + localTime.ToString());

            ntpClient.Close();
        }
        static DateTime ToDateTime(byte[] ntpData)
        {
            ulong intPart = (ulong)ntpData[40] << 24 | (ulong)ntpData[41] << 16 | (ulong)ntpData[42] << 8 | (ulong)ntpData[43];
            ulong fractPart = (ulong)ntpData[44] << 24 | (ulong)ntpData[45] << 16 | (ulong)ntpData[46] << 8 | (ulong)ntpData[47];

            var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
            var networkDateTime = (new DateTime(1900, 1, 1)).AddMilliseconds((long)milliseconds);
            return networkDateTime;
        }
    }
}
