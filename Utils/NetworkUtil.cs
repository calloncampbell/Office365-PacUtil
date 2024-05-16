using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Office365.PacUtil.Utils
{
    internal static class NetworkUtil
    {
        /// <summary>
        /// Calculates the IP v4 subnet
        /// Example
        ///   Input: 192.168.0.1/24
        ///   Result: IP: 192.168.0.1, Subnet: 255.255.255.0  
        /// </summary>
        /// <param name="ipWithMask">When a IP-Range is written as CIDR Notation: aaa.bbb.ccc.ddd/netmask</param>
        /// <returns></returns>
        public static Tuple<string, string> CalculateIpV4Subnet(string ipWithMask)
        {
            var result = ipWithMask.Split('/');
            if (result?.Length == 0)
            {
                // TOOD - thow error
                throw new Exception("error parsing IP");
            }

            IPAddress ip = IPAddress.Parse(result[0].ToString());
            int cidr = int.Parse(result[1].ToString());
            string subnet = string.Empty;

            switch (cidr)
            {
                case 32:
                    subnet = "255.255.255.255";
                    break;

                case 31:
                    subnet = "255.255.255.254";
                    break;

                case 30:
                    subnet = "255.255.255.252";
                    break;

                case 29:
                    subnet = "255.255.255.248";
                    break;

                case 28:
                    subnet = "255.255.255.240";
                    break;

                case 27:
                    subnet = "255.255.255.224";
                    break;

                case 26:
                    subnet = "255.255.255.192";
                    break;

                case 25:
                    subnet = "255.255.255.128";
                    break;

                case 24:
                    subnet = "255.255.255.0";
                    break;

                case 23:
                    subnet = "255.255.254.0";
                    break;

                case 22:
                    subnet = "255.255.252.0";
                    break;

                case 21:
                    subnet = "255.255.248.0";
                    break;

                case 20:
                    subnet = "255.255.240.0";
                    break;

                case 19:
                    subnet = "255.255.224.0";
                    break;

                case 18:
                    subnet = "255.255.192.0";
                    break;

                case 17:
                    subnet = "255.255.128.0";
                    break;

                case 16:
                    subnet = "255.255.0.0";
                    break;

                case 15:
                    subnet = "255.254.0.0";
                    break;

                case 14:
                    subnet = "255.252.0.0";
                    break;

                case 13:
                    subnet = "255.248.0.0";
                    break;

                case 12:
                    subnet = "255.240.0.0";
                    break;

                case 11:
                    subnet = "255.224.0.0";
                    break;

                case 10:
                    subnet = "255.192.0.0";
                    break;

                case 9:
                    subnet = "255.128.0.0";
                    break;

                case 8:
                    subnet = "255.0.0.0";
                    break;

                case 7:
                    subnet = "254.0.0.0";
                    break;

                case 6:
                    subnet = "252.0.0.0";
                    break;

                case 5:
                    subnet = "248.0.0.0";
                    break;

                case 4:
                    subnet = "240.0.0.0";
                    break;

                case 3:
                    subnet = "224.0.0.0";
                    break;

                case 2:
                    subnet = "192.0.0.0";
                    break;

                case 1:
                    subnet = "128.0.0.0";
                    break;

                case 0:
                    subnet = "0.0.0.0";
                    break;
            }

            return Tuple.Create(ip.ToString(), subnet.ToString());
        }

        /// <summary>
        /// Calculates the IP v4 range
        /// Example
        ///   Input: 192.168.0.1/25
        ///   Result: 192.168.0.1 to 192.168.0.126
        ///   
        /// Reference
        ///   https://stackoverflow.com/questions/1470792/how-to-calculate-the-ip-range-when-the-ip-address-and-the-netmask-is-given
        /// </summary>
        /// <param name="ipWithMask">When a IP-Range is written as CIDR Notation: aaa.bbb.ccc.ddd/netmask</param>
        /// <returns></returns>
        public static Tuple<string, string, string> CalculateIpV4Range(string ipWithMask)
        {
            // TODO - Fix IP and BITS
            var result = ipWithMask.Split('/');
            if (result?.Length == 0) 
            {
                throw new Exception("error parsing IP");
            }
            
            IPAddress ip = IPAddress.Parse(result[0].ToString());
            int bits = int.Parse(result[1].ToString());

            uint mask = ~(uint.MaxValue >> bits);

            // Convert the IP address to bytes.
            byte[] ipBytes = ip.GetAddressBytes();

            // BitConverter gives bytes in opposite order to GetAddressBytes().
            byte[] maskBytes = BitConverter.GetBytes(mask).Reverse().ToArray();

            byte[] startIPBytes = new byte[ipBytes.Length];
            byte[] endIPBytes = new byte[ipBytes.Length];

            // Calculate the bytes of the start and end IP addresses.
            for (int i = 0; i < ipBytes.Length; i++)
            {
                startIPBytes[i] = (byte)(ipBytes[i] & maskBytes[i]);
                endIPBytes[i] = (byte)(ipBytes[i] | ~maskBytes[i]);
            }

            // Convert the bytes to IP addresses.
            IPAddress startIP = new IPAddress(startIPBytes);
            IPAddress endIP = new IPAddress(endIPBytes);

            return Tuple.Create(ip.ToString(), startIP.ToString(), endIP.ToString());
        }
    }
}
