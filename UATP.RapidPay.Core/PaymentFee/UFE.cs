namespace UATP.RapidPay.Core.PaymentFee
{
    public class UFE
    {
        #region Singleton
        private UFE() { }

        private static readonly UFE _instance = new ();
        public static UFE Instance => _instance;

        #endregion

        #region Random generator
        private const double MAX_VALUE = 2;
        private readonly Random random = new(); // since UFE implementation is a singleton, it is no need to handle Random as a static instance
        #endregion

        private static object _lock = new();

        public double ActualFee { get; private set; }

        public void CalculateNewFee()
        {
            lock (_lock)
            {
                double newFee = random.NextDouble() * MAX_VALUE;
                ActualFee = ActualFee == 0 ?
                    newFee : 
                    ActualFee * newFee;
            }
        }

    }
}
