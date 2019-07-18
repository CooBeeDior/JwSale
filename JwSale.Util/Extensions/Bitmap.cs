using System.IO;

namespace JwSale.Util.Extensions
{
    internal class Bitmap
    {
        private Stream sm;

        public Bitmap(Stream sm)
        {
            this.sm = sm;
        }
    }
}