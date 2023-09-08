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
        // https://stackoverflow.com/questions/1470792/how-to-calculate-the-ip-range-when-the-ip-address-and-the-netmask-is-given

        /// <summary>
        /// Calculates the IP v4 range
        /// Example
        ///   Input: 192.168.0.1/25
        ///   Result: 192.168.0.1 - 192.168.0.126
        /// </summary>
        /// <param name="ipRangeWithMask">When a IP-Range is written as CIDR Notation: aaa.bbb.ccc.ddd/netmask</param>
        /// <returns></returns>
        public static Tuple<string, string, string> CalculateIpV4Range(string ipRangeWithMask)
        {
            // TODO - Fix IP and BITS
            var result = ipRangeWithMask.Split('/');
            if (result?.Length == 0) 
            {
                // TOOD - thow error
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
