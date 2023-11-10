using System;
using System.ComponentModel.DataAnnotations;

namespace Customer.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "ImageSelfy is required.")]
        public string ImageSelfy { get; set; } = string.Empty;

        [Required(ErrorMessage = "PhoneNo is required.")]
        [RegularExpression(@"^(77|73|71|78|70)\d{7}$", ErrorMessage = "PhoneNo must be a Yemeni number.")]
        public string PhoneNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "FullName is required.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "BirthDay is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [CurrentDate(ErrorMessage = "BirthDay cannot be greater than the current date.")]
        public DateTime BirthDay { get; set; } = DateTime.Now.Date;

        [Required(ErrorMessage = "IDCard is required.")]
        public string IDCard { get; set; } = string.Empty;

    
        public int Age
        {
            get { return CalculateAge(BirthDay); }
        }

        private int CalculateAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }
    }

    public class CurrentDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            DateTime date;
            if (DateTime.TryParse(value!.ToString(), out date))
            {
                return date <= DateTime.Now;
            }
            return false;
        }
    }
}