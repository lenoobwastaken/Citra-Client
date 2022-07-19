using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Microsoft.Win32;

namespace CitraClient.Authentication
{
	public class api
	{
		[DataContract]
		private class response_structure
		{
			[DataMember]
			public bool success { get; set; }

			[DataMember]
			public string sessionid { get; set; }

			[DataMember]
			public string contents { get; set; }

			[DataMember]
			public string response { get; set; }

			[DataMember]
			public string message { get; set; }

			[DataMember]
			public string download { get; set; }

			[DataMember(IsRequired = false, EmitDefaultValue = false)]
			public user_data_structure info { get; set; }

			[DataMember(IsRequired = false, EmitDefaultValue = false)]
			public app_data_structure appinfo { get; set; }

			[DataMember]
			public List<msg> messages { get; set; }
		}

		public class msg
		{
			public string message { get; set; }

			public string author { get; set; }

			public string timestamp { get; set; }
		}

		[DataContract]
		private class user_data_structure
		{
			[DataMember]
			public string username { get; set; }

			[DataMember]
			public string ip { get; set; }

			[DataMember]
			public string hwid { get; set; }

			[DataMember]
			public string createdate { get; set; }

			[DataMember]
			public string lastlogin { get; set; }

			[DataMember]
			public List<Data> subscriptions { get; set; }
		}

		[DataContract]
		private class app_data_structure
		{
			[DataMember]
			public string numUsers { get; set; }

			[DataMember]
			public string numOnlineUsers { get; set; }

			[DataMember]
			public string numKeys { get; set; }

			[DataMember]
			public string version { get; set; }

			[DataMember]
			public string customerPanelLink { get; set; }
		}

		public class app_data_class
		{
			public string numUsers { get; set; }

			public string numOnlineUsers { get; set; }

			public string numKeys { get; set; }

			public string version { get; set; }

			public string customerPanelLink { get; set; }
		}

		public class user_data_class
		{
			public string username { get; set; }

			public string ip { get; set; }

			public string hwid { get; set; }

			public string createdate { get; set; }

			public string lastlogin { get; set; }

			public List<Data> subscriptions { get; set; }
		}

		public class Data
		{
			public string subscription { get; set; }

			public string expiry { get; set; }

			public string timeleft { get; set; }
		}

		public class response_class
		{
			public bool success { get; set; }

			public string message { get; set; }
		}

		public string name;

		public string ownerid;

		public string secret;

		public string version;

		private string sessionid;

		private string enckey;

		private bool initzalized;

		public app_data_class app_data = new app_data_class();

		public user_data_class user_data = new user_data_class();

		public response_class response = new response_class();

		private json_wrapper response_decoder = new json_wrapper(new response_structure());

		public api(string name, string ownerid, string secret, string version)
		{
			if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(ownerid) || string.IsNullOrWhiteSpace(secret) || string.IsNullOrWhiteSpace(version))
			{
				error("Application not setup correctly. Please watch video link found in Main.cs");
				Environment.Exit(0);
			}
			this.name = name;
			this.ownerid = ownerid;
			this.secret = secret;
			this.version = version;
		}

		public void init()
		{
			enckey = encryption.sha256(encryption.iv_key());
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection post_data = new NameValueCollection
			{
				["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("init")),
				["ver"] = encryption.encrypt(version, secret, text),
				["hash"] = checksum(Process.GetCurrentProcess().MainModule.FileName),
				["enckey"] = encryption.encrypt(enckey, secret, text),
				["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(name)),
				["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(ownerid)),
				["init_iv"] = text
			};
			string text2 = req(post_data);
			if (text2 == "KeyAuth_Invalid")
			{
				error("Application not found");
				Environment.Exit(0);
			}
			text2 = encryption.decrypt(text2, secret, text);
			response_structure response_structure = response_decoder.string_to_generic<response_structure>(text2);
			load_response_struct(response_structure);
			if (response_structure.success)
			{
				load_app_data(response_structure.appinfo);
				sessionid = response_structure.sessionid;
				initzalized = true;
			}
			else if (response_structure.message == "invalidver")
			{
				Process.Start(response_structure.download);
				Environment.Exit(0);
			}
		}

		public static string GetHWID()
		{
			string text = "No HWID";
			try
			{
				text = (string)RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\IDConfigDB\\Hardware Profiles\\0001", writable: false).GetValue("HwProfileGUID");
				text = text.Replace("{", "");
				text = text.Replace("}", "");
				return text;
			}
			catch
			{
				return text;
			}
		}

		public void license(string key)
		{
			if (!initzalized)
			{
				error("Please initzalize first");
				Environment.Exit(0);
			}
			string hWID = GetHWID();
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection post_data = new NameValueCollection
			{
				["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("license")),
				["key"] = encryption.encrypt(key, enckey, text),
				["hwid"] = encryption.encrypt(hWID, enckey, text),
				["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(sessionid)),
				["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(name)),
				["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(ownerid)),
				["init_iv"] = text
			};
			string message = req(post_data);
			message = encryption.decrypt(message, enckey, text);
			response_structure response_structure = response_decoder.string_to_generic<response_structure>(message);
			load_response_struct(response_structure);
			if (response_structure.success)
			{
				load_user_data(response_structure.info);
			}
		}

		public static string checksum(string filename)
		{
			using MD5 mD = MD5.Create();
			using FileStream inputStream = File.OpenRead(filename);
			byte[] array = mD.ComputeHash(inputStream);
			return BitConverter.ToString(array).Replace("-", "").ToLowerInvariant();
		}

		public static void error(string message)
		{
			Console.WriteLine(message);
			Thread.Sleep(2000);
			Environment.Exit(0);
		}

		private static string req(NameValueCollection post_data)
		{
			try
			{
				using WebClient webClient = new WebClient();
				byte[] bytes = webClient.UploadValues("https://keyauth.win/api/1.0/", post_data);
				return Encoding.Default.GetString(bytes);
			}
			catch (WebException ex)
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)ex.Response;
				HttpStatusCode statusCode = httpWebResponse.StatusCode;
				HttpStatusCode httpStatusCode = statusCode;
				if (httpStatusCode == (HttpStatusCode)429)
				{
					error("You're connecting too fast to loader, slow down.");
					Environment.Exit(0);
					return "";
				}
				error("Connection failure. Please try again, or contact us for help.");
				Environment.Exit(0);
				return "";
			}
		}

		private void load_app_data(app_data_structure data)
		{
			app_data.numUsers = data.numUsers;
			app_data.numOnlineUsers = data.numOnlineUsers;
			app_data.numKeys = data.numKeys;
			app_data.version = data.version;
			app_data.customerPanelLink = data.customerPanelLink;
		}

		private void load_user_data(user_data_structure data)
		{
			user_data.username = data.username;
			user_data.ip = data.ip;
			user_data.hwid = data.hwid;
			user_data.createdate = data.createdate;
			user_data.lastlogin = data.lastlogin;
			user_data.subscriptions = data.subscriptions;
		}

		private void load_response_struct(response_structure data)
		{
			response.success = data.success;
			response.message = data.message;
		}
	}
}
