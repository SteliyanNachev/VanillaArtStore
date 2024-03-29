﻿namespace VanillaArtStore.Data
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
        
        public const int ProductImagesMaxCount = 10;
        public const int ProductImagesMinCount = 3;

        public const int ReviewCommentMaxLenght = 10000;
        public const int ReviewCommentMinLenght = 10;
        public const int ReviewRatingMin = 1;
        public const int ReviewRatingMxn = 5;

        public const int MessageUsernameMinLenght = 3;
        public const int MessageUsernameMaxLenght = 40;
        public const int MessageSubjectMaxLenght = 500;
        public const int MessageMaxLenght = 50000;

        public class User
        {
            public const int UserNameMaxLenght = 20;
            public const int UserAdressMaxLenght = 120;
            public const int UserPhoneMaxLenght = 20;
        }

        public class Address
        {
            public const int AddressLineMaxLenght = 500;
            public const int TownMaxLenght = 30;
            public const int ZipCodeMaxLenght = 30;
        }

        public class Country
        {
            public const int CountryNameMaxLenght = 30;
        }
    }
}
