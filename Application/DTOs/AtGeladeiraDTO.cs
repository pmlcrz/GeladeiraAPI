using Application.DTOs;
using System;
using System.ComponentModel.DataAnnotations;
using Xunit;
using System.Collections.Generic;

namespace GeladeiraAPI.Tests
{
    public class AtGeladeiraDTOTests
    {
        [Fact]
        public void AtGeladeiraDTO_ValidDTO_ShouldPassValidation()
        {
            var dto = new AtGeladeiraDTO
            {
                Id = 1,
                Nome = "Geladeira A",
                Posicao = 3,
                Andar = 2,
                Container = 1
            };

            var validationResults = ValidateModel(dto);

            Assert.True(validationResults.Count == 0); 
        }

        [Fact]
        public void AtGeladeiraDTO_InvalidPosicao_ShouldFailValidation()
        {
            var dto = new AtGeladeiraDTO
            {
                Id = 1,
                Nome = "Geladeira A",
                Posicao = 6, 
                Andar = 2,
                Container = 1
            };

            var validationResults = ValidateModel(dto);

            Assert.True(validationResults.Count > 0);
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("A posição deve ser um valor entre 1 e 5."));
        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }
    }
}
