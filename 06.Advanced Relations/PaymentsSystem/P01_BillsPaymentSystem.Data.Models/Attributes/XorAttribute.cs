namespace P01_BillsPaymentSystem.Data.Models.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property)]
    public class XorAttribute : ValidationAttribute
    {
        private readonly string targetAttribute;

        public XorAttribute(string targetAttribute)
        {
            this.targetAttribute = targetAttribute;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType
                                            .GetProperty(targetAttribute)
                                            .GetValue(validationContext.ObjectInstance);

            if ((property == null && value != null) || (property != null && value == null))
            {
                return ValidationResult.Success; 
            }

            return new ValidationResult("One of the props must be null!"); 
        }
    }
}