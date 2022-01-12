namespace BlApi
{
    public class BlFactory
    {
        public static IBL GetBl() {
            return BL.BL.Instance;
        }
    }
}
