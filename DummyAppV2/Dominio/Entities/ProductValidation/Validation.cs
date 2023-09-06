using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entities.ProductValidation
{
    public class ProductValidation : AbstractValidator<Product>
    {
    
        public ProductValidation()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Title).NotEmpty().WithMessage("O titulo não pode ser vazio");   
        }
    }
}
