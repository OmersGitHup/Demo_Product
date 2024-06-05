using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.FluentValidation
{
    public class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x=> x.Name).NotEmpty().WithMessage("You can't pass with non name !");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("The Name have to be min 3 character ! ");
            RuleFor(x => x.Stock).NotEmpty().WithMessage("Stock cant pass empty");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price cant pass empty");

        }
    }
}
