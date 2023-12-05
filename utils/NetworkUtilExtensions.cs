using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LaminariaCore_General.utils;
using MCSMLauncher.common;
using Open.Nat;

// ReSharper disable InconsistentNaming

namespace MCSMLauncher.utils
{
    /// <summary>
    /// This class contains a bunch of useful methods for interacting with the network
    /// </summary>
    public static class NetworkUtilExtensions
    {
        /// <summary>
        /// Recurrently tests the wifi connection every two seconds until it is established.
        /// </summary>
        /// <param name="label">The label to write status updated into</param>
        public static async Task RecurrentTestAsync(Label label = default)
        {
            while (true)
            {
                Logging.Logger.Info(@"Checking for an internet connection...");
                if (NetworkUtils.IsWifiConnected()) break;
                
                if (label != null) label.Text = @"Could not connect to the internet. Retrying...";
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
                NatDiscoverer discoverer = new ();
                var device = await discoverer.DiscoverDeviceAsync(PortMapper.Upnp, new CancellationTokenSource(10000));

                // Create a new TCP port mapping in the router identified by the external port.
                try
                {
                    Logging.Logger.Info(@$"Creating a new TCP port mapping for I{internalPort}@E{externalPort}...");
                    await device.CreatePortMapAsync(new Mapping(Protocol.Tcp, internalPort, externalPort,
                        $"TCP-MCSMLauncher@{internalPort}"));
                }
                // If the port mapping already exists, ignore it.
                catch (MappingException) { Logging.Logger.Warn(@$"The I{internalPort}@E{externalPort} TCP port mapping already exists. Ignoring..."); }
                
                return true;
            }

            // If the network does not support UPnP, ignore it.
            catch (NatDeviceNotFoundException)
            {
                Logging.Logger.Warn(@"The current network does not support UPnP. Ignoring...");
            }
            
            // If any other exception occurs, log it and return false.
            catch (Exception e)
            {
                Logging.Logger.Error(@$"An error occured while trying to create the port mapping.\n{e.StackTrace}");
            }

            return false;
        }
    }
}