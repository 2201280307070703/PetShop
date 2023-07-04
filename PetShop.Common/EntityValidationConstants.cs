namespace PetShop.Common
{
    public static class EntityValidationConstants
    {
        public static class Product
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;

            public const int DescriptionMinLength =50;
            public const int DescriptionMaxLength = 500;

            public const int ImageUrlMaxLength = 2048;

            public const string PriceMinValue = "0";
            public const string PriceMaxValue = "1000";

            public const string QuantityMinValue = "0";
            public const string QuantityMaxValue = "10000";
        }

        public static class  Category
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 30;
        }

        public static class AnimalType
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }

        public static class AgeType
        {
            public const int AgeTypeMinLength = 2;
            public const int AgeTypeMaxLength = 50;
        }
    }
}   
