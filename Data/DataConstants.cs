namespace VanillaArtStore.Data
{
    public class DataConstants
    {
        public const int ProductNameMaxLenght = 200;
        public const int ProductNameMinLenght = 4;

        public const int ProductPriceMinQuantity = 1;
        public const int ProductPriceMaxQuantity = 10000;

        public const int ProductDescriptionMaxLenght = 5000;
        public const int ProductDescriptionMinLenght = 10;

        public const int ProductInSctockMaxQuantity = 100;
        public const int ProductInSctockMinQuantity = 1;

        public const int ReviewCommentMaxLenght = 10000;
        public const int ReviewCommentMinLenght = 10000;
        public const int ReviewRatingMin = 1;
        public const int ReviewRatingMxn = 5;

        public class User
        {
            public const int UserNameMaxLenght = 20;
            public const int UserAdressMaxLenght = 120;
            public const int UserPhoneMaxLenght = 20;
        }
    }
}
