using Domain;
using System;
using System.ComponentModel.DataAnnotations;
using Xunit;
using System.Collections.Generic;

namespace GeladeiraAPI.Tests
{
    public class ItemModelTests
    {
        [Fact]
        public void ItemModel_ValidModel_ShouldPassValidation()
        {
            var item = new ItemModel
            {
                Nome = "Item 1",
                Posicao = 3,
                Andar = 2,
                Container = 1,
                Id = 1 
            };

            var validationResults = ValidateModel(item);

            Assert.True(validationResults.Count == 0); 
        }

        [Fact]
        public void ItemModel_InvalidPosicao_ShouldFailValidation()
        {
            var item = new ItemModel
            {
                Nome = "Item 1",
                Posicao = 6, 
                Andar = 2,
                Container = 1,
                Id = 1
            };

            var validationResults = ValidateModel(item);

            Assert.True(validationResults.Count > 0);
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("A posição deve ser um valor entre 1 e 5."));
        }

        [Fact]
        public void ItemModel_EmptyNome_ShouldFailValidation()
        {
            var item = new ItemModel
            {
                Nome = "", 
                Posicao = 3,
                Andar = 2,
                Container = 1,
                Id = 1
            };

            var validationResults = ValidateModel(item);

            Assert.True(validationResults.Count > 0);
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("The Nome field is required"));
        }

        [Fact]
        public void ItemModel_InvalidContainer_ShouldFailValidation()
        {
            var item = new ItemModel
            {
                Nome = "Item 1",
                Posicao = 3,
                Andar = 2,
                Container = 5, 
                Id = 1
            };

            var validationResults = ValidateModel(item);

            Assert.True(validationResults.Count > 0);
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("O container deve ser um valor entre 1 e 4."));
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
