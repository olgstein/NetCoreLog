namespace NetCoreLog
{
    [System.Serializable]
    public class NetCoreLogException : System.Exception
    {
        public NetCoreLogException() { }

        public NetCoreLogException(string message) : base(message) { }

        public NetCoreLogException(string message, System.Exception inner) : base(message, inner) { }

        protected NetCoreLogException(
            System.Runtime.Serialization.SerializationInfo info, 
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

}