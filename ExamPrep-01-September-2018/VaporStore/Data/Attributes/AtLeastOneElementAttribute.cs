namespace VaporStore.Data.Attributes
{
    using System.Collections;
    using System.ComponentModel.DataAnnotations;

    public class AtLeastOneElementAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            IList collection = value as IList;
            if (collection != null)
            {
                return collection.Count > 0; 
            }

            return false;
        }
    }
}