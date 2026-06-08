using System.Drawing;

namespace BookStore.Helpers
{
    public static class AppConstants
    {
        public const int RoleGuest = 1;
        public const int RoleClient = 2;
        public const int RoleManager = 3;
        public const int RoleAdmin = 4;

        public static readonly Color DiscountHighlight = ColorTranslator.FromHtml("#23E1EF");
        public static readonly Color OutOfStockColor = ColorTranslator.FromHtml("#D3D3D3");

        public const int ImageWidth = 300;
        public const int ImageHeight = 200;
    }
}
