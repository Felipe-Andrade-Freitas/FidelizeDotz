using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using FakeItEasy;
using FidelizeDotz.Services.Api.CrossCutting.Constants;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.Category;
using FidelizeDotz.Services.Api.Domain.Application.Services;
using FidelizeDotz.Services.Api.Domain.Entities;
using FidelizeDotz.Services.Api.Domain.Infra.Interfaces;
using MetLife.Sinistro.Api.CrossCutting.Extensions;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static FidelizeDotz.Services.Api.Domain.Enums.EReturnMessageType;

namespace Tests.Application.Services
{
    [TestClass]
    public class CategoryServiceTest : ServiceBaseTest
    {
        private readonly CategoryService _categoryService;
        private readonly IRepository<Category> _categoryRepository;
        private IList<Category> _categories;

        public CategoryServiceTest()
        {
            _categoryRepository = A.Fake<IRepository<Category>>();

            _categoryService = new CategoryService(_unitOfWork, _adapter);
            A.CallTo(() => _unitOfWork.GetRepository<Category>(false))
                .Returns(_categoryRepository);
        }

        [TestMethod]
        public async Task ListCategoriesAsync_ExistinBalance_ReturnsOk()
        {
            // Arrange
            GenerateCategories();
            A.CallTo(() => _categoryRepository.GetPagedListAsync(
                    A<Expression<Func<Category, bool>>>.Ignored,
                    A<Func<IQueryable<Category>, IOrderedQueryable<Category>>>.Ignored,
                    A<Func<IQueryable<Category>, IIncludableQueryable<Category, object>>>.Ignored,
                    A<int>.Ignored,
                    A<int>.Ignored,
                    A<bool>.Ignored,
                    A<CancellationToken>.Ignored,
                    A<bool>.Ignored))
                .Returns(_categories.ToPagedList(0, 10));

            //Act
            var result = await _categoryService.ListCategoriesAsync();

            //Assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.Items.Count > 0);
        }

        [TestMethod]
        public async Task ListCategoriesAsync_Exception_ReturnsException()
        {
            // Arrange     
            A.CallTo(() => _categoryRepository.GetPagedListAsync(
                    A<Expression<Func<Category, bool>>>.Ignored,
                    A<Func<IQueryable<Category>, IOrderedQueryable<Category>>>.Ignored,
                    A<Func<IQueryable<Category>, IIncludableQueryable<Category, object>>>.Ignored,
                    A<int>.Ignored,
                    A<int>.Ignored,
                    A<bool>.Ignored,
                    A<CancellationToken>.Ignored,
                    A<bool>.Ignored))
                .Throws(new Exception());

            //Act
            var result = await _categoryService.ListCategoriesAsync();

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(ClientErrorBadRequest, result.ReturnMessageType);
            Assert.IsTrue(result.ErrorMessages.Select(_ => _.Message).Contains(ErrorsConstants.UnexpectedError));
        }
        
        [TestMethod]
        public async Task InsertDotAsync_ExistinBalance_ReturnsOk()
        {
            // Arrange
            var request = GenerateInsertCategoryRequest();
            A.CallTo(() => _categoryRepository.InsertAsync(A<Category>.Ignored, A<CancellationToken>.Ignored))
                .Returns(Task.CompletedTask);

            //Act
            var result = await _categoryService.InsertCategoryAsyc(request);

            //Assert
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public async Task InsertDotAsync_Exception_ReturnsException()
        {
            // Arrange          
            var request = GenerateInsertCategoryRequest();
            A.CallTo(() => _categoryRepository.InsertAsync(A<Category>.Ignored, A<CancellationToken>.Ignored))
                .Throws(new Exception());

            //Act
            var result = await _categoryService.InsertCategoryAsyc(request);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(ClientErrorBadRequest, result.ReturnMessageType);
            Assert.IsTrue(result.ErrorMessages.Select(_ => _.Message).Contains(ErrorsConstants.UnexpectedError));
        }

        [TestMethod]
        public async Task InsertDotAsync_RequestNull_ReturnsError()
        {
            //Act
            var result = await _categoryService.InsertCategoryAsyc(null);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(ClientErrorBadRequest, result.ReturnMessageType);
            Assert.IsTrue(result.ErrorMessages.Select(_ => _.Message).Contains(ErrorsConstants.RequestIsNull));
        }


        #region [ Private ]

        private void GenerateCategories()
        {
            _categories = new List<Category> { GenerateCategory(), GenerateCategory() };
            A.CallTo(() => _categoryRepository.GetAll())
                .Returns(_categories.AsQueryable());
        }

        private Category GenerateCategory()
        {
            return new Faker<Category>()
                .RuleFor(_ => _.Id, Guid.NewGuid)
                .RuleFor(_ => _.Name, _ => _.Random.String(100))
                .RuleFor(_ => _.Code, _ => _.Random.String());
        }

        private InsertCategoryRequest GenerateInsertCategoryRequest()
        {
            return new Faker<InsertCategoryRequest>()
                .RuleFor(_ => _.Name, _ => _.Random.String(100))
                .RuleFor(_ => _.Code, _ => _.Random.String(10));
        }

        #endregion
    }
}