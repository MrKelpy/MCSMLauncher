using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace MCSMLauncher.common
{
    /// <summary>
    /// This class implements methods used in order to check the network state.
    /// </summary>
    public class NetworkTester
    {

        /// <summary>
        /// Checks if the current machine has internet connectivity.
        /// </summary>
        /// <returns>True if it is, false if not, or an error occured.</returns>
        public static bool IsWifiConnected()
        {
            try
            {
                PingReply reply = new Ping().Send("google.com", 1000, new byte[32], new PingOptions());
                return reply != null && reply.Status == IPStatus.Success;
            }
            // There are many, many exceptions that can be thrown from a ping, so just catch them all.
            catch { return false; }
        }
    }
}