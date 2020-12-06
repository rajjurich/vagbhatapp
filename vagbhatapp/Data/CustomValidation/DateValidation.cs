using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vagbhatapp.Data.CustomValidation
{
    public class DateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime cdt = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            DateTime dt = Convert.ToDateTime(value);
            if (dt < cdt)
            {
                return new ValidationResult("Select Current or Future date",
            new[] { validationContext.MemberName });
            };
            return null;
        }
    }
}
