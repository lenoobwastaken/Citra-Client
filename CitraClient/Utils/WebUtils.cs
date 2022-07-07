using System.Net;

namespace CitraClient.Utils
{
	public static class WebUtils
	{
		public static byte[] ImageToByteArray(string url)
		{
			using WebClient webClient = new WebClient();
			return webClient.DownloadData(url);
		}
	}
}
