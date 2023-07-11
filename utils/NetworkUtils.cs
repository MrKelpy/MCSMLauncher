using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCSMLauncher.common;
using Open.Nat;

// ReSharper disable InconsistentNaming

namespace MCSMLauncher.utils
{
    /// <summary>
    /// This class contains a bunch of useful methods for interacting with the network
    /// </summary>
    public static class NetworkUtils
    {
        
        /// <summary>
        /// Returns the External IPv4 Address.
        /// </summary>
        /// <returns>A string containing the ip addr</returns>
        public static string GetExternalIPAddress() => new WebClient().DownloadString("https://checkip.amazonaws.com/");

        /// <summary>
        /// Returns the Local IPv4 Address.
        /// </summary>
        /// <returns>A string containing the ip addr</returns>
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            return host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)?.ToString();
        }

        /// <summary>
        /// Determines the next available port based on a given starting port, up
        /// until port 65534. This is done by creating a TCP socket and trying to bind
        /// the port to the local IP Address.
        /// </summary>
        /// <param name="startingPort">The port to start the binding checks on</param>
        /// <returns>Either the starting port or the next available port after it</returns>
        public static int GetNextAvailablePort(int startingPort)
        {
            // Iterates through the ports until it finds one that's available, starting from the given port.
            for (int currentPort = startingPort; currentPort < IPEndPoint.MaxPort; currentPort++)
                if (!PortInUse(currentPort)) return currentPort;

            return -1;  // There are no more ports open.
        }
        
        /// <summary>
        /// Checks if a port is open or not, by accessing the list of active TCP listeners
        /// and checking if any endpoint has the same port as the one we're checking.
        /// </summary>
        /// <param name="port">The port to check</param>
        /// <returns>Whether the port is open or not</returns>
        private static bool PortInUse(int port)
        {
            IPEndPoint[] ipEndpoints = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners();
            return ipEndpoints.Any(endPoint => endPoint.Port == port);
        }
        
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

        /// <summary>
        /// Checks if the current wifi network supports UPnP, and if so, creates a port mapping for the
        /// specified ports.
        /// If the port mapping already exists, the current one will be ignored.
        /// </summary>
        /// <param name="internalPort">The internal port to redirect incoming traffic to</param>
        /// <param name="externalPort">The external port to use to redirect the traffic</param>
        /// <returns>Either true or false, depending on whether the port mapping was successful or not</returns>
        public static async Task<bool> TryCreatePortMapping(int internalPort, int externalPort)
        {
            try
            {
                // Discover the router, on a 10 second timeout.
                NatDiscoverer discoverer = new NatDiscoverer();
                var device = await discoverer.DiscoverDeviceAsync(PortMapper.Upnp, new CancellationTokenSource(10000));

                // Create a new port mapping in the router identified by the external port.
                // TODO: UNCOMMENT THESE LINES
                // await device.CreatePortMapAsync(new Mapping(Protocol.Tcp, internalPort, externalPort,
                //    $"TCP-MCSMLauncher@{internalPort}"));
                
                // await device.CreatePortMapAsync(new Mapping(Protocol.Udp, internalPort, externalPort,
                //    $"UDP-MCSMLauncher@{internalPort}"));

                return true;
            }
            // If the port mapping already exists, ignore it.
            catch (MappingException)
            {
                Logging.LOGGER.Warn(@"The port mapping already exists. Ignoring...");
                return true;
            }
            
            // If the network does not support UPnP, ignore it.
            catch (NatDeviceNotFoundException)
            {
                Logging.LOGGER.Warn(@"The current network does not support UPnP. Ignoring...");
            }
            
            // If any other exception occurs, log it and return false.
            catch (Exception e)
            {
                Logging.LOGGER.Error(@$"An error occured while trying to create the port mapping.\n{e.StackTrace}");
            }

            return false;
        }
    }
}