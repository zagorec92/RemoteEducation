using System.Collections.Generic;

namespace RemoteEducationApplication.Helpers
{
    public static class WebBrowserHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static Dictionary<int, string> GetParsedUrlParameters(string content)
        {
            Dictionary<int, string> urlParams = new Dictionary<int, string>();

            string[] splittedParams = content.Split('&');

            for (int i = 0; i < splittedParams.Length; i++)
            {
                string[] data = splittedParams[i].Split('=');
                string answer = data[1].Replace("+", " ");

                urlParams.Add(i, answer);
            }

            return urlParams;
        }
    }
}
