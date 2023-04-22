namespace PRAS.Exceptions
{
    public class CredentialsException : Exception
    {
        public CredentialsException(string message = "Email or password is wrong") : base(message)
        {
        }
    }
}
