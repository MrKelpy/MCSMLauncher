using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Label = System.Windows.Forms.Label;

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
            catch  { return false; }
        }

        /// <summary>
        /// Recurrently tests the wifi connection every two seconds until it is established.
        /// </summary>
        /// <param name="label">The label to write status updated into</param>
        public static async Task RecurrentTestAsync(Label label)
        {
            while (true)
            {
                Logging.LOGGER.Info(@"Checking for an internet connection...");
                if (IsWifiConnected()) break;
                
                label.Text = @"Could not connect to the internet. Retrying...";
                await Task.Delay(2 * 1000);
            }
        }
    }
}