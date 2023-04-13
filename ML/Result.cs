namespace ML
{
    public class Result
    {
        public bool Correct { get; set; }
        public object Object { get; set; }
        public List<object> Objects { get; set; }
        public string Message { get; set; }
        public Exception ex { get; set; }
    }
}