using Application.DTOs;
using System;
using System.ComponentModel.DataAnnotations;
using Xunit;
using System.Collections.Generic;

namespace GeladeiraAPI.Tests
{
    public class UparGeladeiraDTOTests
    {
        [Fact]
        public void UparGeladeiraDTO_ValidDTO_ShouldPassValidation()
        {
            var dto = new UparGeladeiraDTO
            {
                Nome = "Geladeira B",
                Posicao = 2,
                Andar = 3,
                Container = 1
            };

            var validationResults = ValidateModel(dto);

            Assert.True(validationResults.Count == 0); 
        }

        [Fact]
        public void UparGeladeiraDTO_InvalidAndar_ShouldFailValidation()
        {
            var dto = new UparGeladeiraDTO
            {
                Nome = "Geladeira B",
                Posicao = 2,
                Andar = 5, 
                Container = 2
            };

            var validationResults = ValidateModel(dto);

            Assert.True(validationResults.Count > 0);
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("O andar deve ser um valor entre 1 e 4."));
        }

        [Fact]
        public void UparGeladeiraDTO_EmptyNome_ShouldFailValidation()
        {
            var dto = new UparGeladeiraDTO
            {
                Nome = "", 
                Posicao = 2,
                Andar = 2,
                Container = 3
            };

            var validationResults = ValidateModel(dto);

            Assert.True(validationResults.Count > 0);
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("The Nome field is required"));
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
