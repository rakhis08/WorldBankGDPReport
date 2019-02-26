using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using WorldBankGDPReport.CommonException;

namespace WorldBankGDPReport
{
    public class LogWriter
    {
        
        public static void LogWrite(string logMessage)
        {
            string m_exePath = string.Empty;
            string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "Log.txt";
            try
            {
                using (StreamWriter w = File.AppendText(filePath))
                {
                    Log(logMessage, w);
                }
            }
            catch (WorldBankAPIException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"), logMessage);                
            }
            catch (WorldBankAPIException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}