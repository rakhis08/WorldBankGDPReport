using System;

namespace WorldBankGDPReport.CommonException
{
    public class WorldBankAPIException : Exception
    {
        public WorldBankAPIException(string message) : base(message)
        {

        }
    }
}