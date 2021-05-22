using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using FakeItEasy;
using FidelizeDotz.Services.Api.CrossCutting.Constants;
using FidelizeDotz.Services.Api.CrossCutting.Infra;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.Dotz;
using FidelizeDotz.Services.Api.Domain.Application.Services;
using FidelizeDotz.Services.Api.Domain.Entities;
using FidelizeDotz.Services.Api.Domain.Infra;
using FidelizeDotz.Services.Api.Domain.Infra.Interfaces;
using MetLife.Sinistro.Api.CrossCutting.Extensions;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static FidelizeDotz.Services.Api.Domain.Enums.EReturnMessageType;

namespace Tests.Application.Services
{
    [TestClass]
    public class DotzServiceTest : ServiceBaseTest
    {
        private readonly DotzService _dotzService;
        private readonly IRepository<Dot> _dotRepository;
        private IList<Dot> _dots;

        public DotzServiceTest()
        {
            _dotRepository = A.Fake<IRepository<Dot>>();

            _dotzService = new DotzService(_unitOfWork, _adapter, _userLogged);
            A.CallTo(() => _unitOfWork.GetRepository<Dot>(false))
                .Returns(_dotRepository);

        }

        [TestMethod]
        public async Task GetBalanceDotsAsync_ExistinBalance_ReturnsOk()
        {
            //Arrage
            GenerateDots();

            //Act
            var result = await _dotzService.GetBalanceDotsAsync();

            //Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(_dots.Sum(_ => _.Quantity), result.Data.Balance);
        }

        [TestMethod]
        public async Task GetBalanceDotsAsync_Exception_ReturnsException()
        {
            // Arrange
            A.CallTo(() => _dotRepository.GetAll())
                .Throws(new Exception());

            //Act
            var result = await _dotzService.GetBalanceDotsAsync();

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(ClientErrorBadRequest, result.ReturnMessageType);
            Assert.IsTrue(result.ErrorMessages.Select(_ => _.Message).Contains(ErrorsConstants.UnexpectedError));
        }

        [TestMethod]
        public async Task RescuedDot_ExistinBalance_ReturnsOk()
        {
            // Arrange
            var request = GenerateRescuedDotRequest();
            GenerateDots();
            A.CallTo(() => _dotRepository.InsertAsync(A<Dot>.Ignored, A<CancellationToken>.Ignored))
                .Returns(Task.CompletedTask);

            //Act
            var result = await _dotzService.RescuedDot(request);

            //Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(request.Quantity, result.Data.Quantity * -1);
        }

        [TestMethod]
        public async Task RescuedDot_Exception_ReturnsException()
        {
            // Arrange          
            var request = GenerateRescuedDotRequest();
            GenerateDots();
            A.CallTo(() => _dotRepository.InsertAsync(A<Dot>.Ignored, A<CancellationToken>.Ignored))
                .Throws(new Exception());

            //Act
            var result = await _dotzService.RescuedDot(request);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(ClientErrorBadRequest, result.ReturnMessageType);
            Assert.IsTrue(result.ErrorMessages.Select(_ => _.Message).Contains(ErrorsConstants.UnexpectedError));
        }

        [TestMethod]
        public async Task RescuedDot_RequestNull_ReturnsError()
        {
            //Act
            var result = await _dotzService.RescuedDot(null);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(ClientErrorBadRequest, result.ReturnMessageType);
            Assert.IsTrue(result.ErrorMessages.Select(_ => _.Message).Contains(ErrorsConstants.RequestIsNull));
        }

        [TestMethod]
        public async Task GetAllDotsAsync_ExistinBalance_ReturnsOk()
        {
            // Arrange
            var request = GenerateGetAllDotRequest();
            GenerateDots();
            A.CallTo(() => _dotRepository.GetPagedListAsync(
                    A<Expression<Func<Dot, bool>>>.Ignored,
                    A<Func<IQueryable<Dot>, IOrderedQueryable<Dot>>>.Ignored,
                    A<Func<IQueryable<Dot>, IIncludableQueryable<Dot, object>>>.Ignored,
                    A<int>.Ignored,
                    A<int>.Ignored,
                    A<bool>.Ignored,
                    A<CancellationToken>.Ignored,
                    A<bool>.Ignored))
                .Returns(_dots.ToPagedList(0, 10));

            //Act
            var result = await _dotzService.GetAllDotsAsync(request);

            //Assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.Items.Count > 0);
        }

        [TestMethod]
        public async Task GetAllDotsAsync_Exception_ReturnsException()
        {
            // Arrange          
            var request = GenerateGetAllDotRequest();
            A.CallTo(() => _dotRepository.InsertAsync(A<Dot>.Ignored, A<CancellationToken>.Ignored))
                .Throws(new Exception());

            //Act
            var result = await _dotzService.GetAllDotsAsync(request);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(ClientErrorBadRequest, result.ReturnMessageType);
            Assert.IsTrue(result.ErrorMessages.Select(_ => _.Message).Contains(ErrorsConstants.UnexpectedError));
        }

        [TestMethod]
        public async Task GetAllDotsAsync_RequestNull_ReturnsError()
        {
            //Act
            var result = await _dotzService.GetAllDotsAsync(null);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(ClientErrorBadRequest, result.ReturnMessageType);
            Assert.IsTrue(result.ErrorMessages.Select(_ => _.Message).Contains(ErrorsConstants.RequestIsNull));
        }

        [TestMethod]
        public async Task InsertDotAsync_ExistinBalance_ReturnsOk()
        {
            // Arrange
            var request = GenerateInsertDotRequest();
            GenerateDots();
            A.CallTo(() => _dotRepository.GetPagedListAsync(
                    A<Expression<Func<Dot, bool>>>.Ignored,
                    A<Func<IQueryable<Dot>, IOrderedQueryable<Dot>>>.Ignored,
                    A<Func<IQueryable<Dot>, IIncludableQueryable<Dot, object>>>.Ignored,
                    A<int>.Ignored,
                    A<int>.Ignored,
                    A<bool>.Ignored,
                    A<CancellationToken>.Ignored,
                    A<bool>.Ignored))
                .Returns(_dots.ToPagedList(0, 10));

            //Act
            var result = await _dotzService.InsertDotAsync(request);

            //Assert
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public async Task InsertDotAsync_Exception_ReturnsException()
        {
            // Arrange          
            var request = GenerateInsertDotRequest();
            A.CallTo(() => _dotRepository.InsertAsync(A<Dot>.Ignored, A<CancellationToken>.Ignored))
                .Throws(new Exception());

            //Act
            var result = await _dotzService.InsertDotAsync(request);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(ClientErrorBadRequest, result.ReturnMessageType);
            Assert.IsTrue(result.ErrorMessages.Select(_ => _.Message).Contains(ErrorsConstants.UnexpectedError));
        }

        [TestMethod]
        public async Task InsertDotAsync_RequestNull_ReturnsError()
        {
            //Act
            var result = await _dotzService.GetAllDotsAsync(null);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(ClientErrorBadRequest, result.ReturnMessageType);
            Assert.IsTrue(result.ErrorMessages.Select(_ => _.Message).Contains(ErrorsConstants.RequestIsNull));
        }

        #region [ Private ]

        private void GenerateDots(int minQuantity = 0, int maxQuantity = 20000)
        {
            _dots = new List<Dot> { GenerateDot(minQuantity, maxQuantity), GenerateDot(minQuantity, maxQuantity), GenerateDot(minQuantity, maxQuantity) };
            A.CallTo(() => _dotRepository.GetAll())
                .Returns(_dots.AsQueryable());
        }

        private Dot GenerateDot(int minQuantity = 0, int maxQuantity = 20000)
        {
            return new Faker<Dot>()
                .RuleFor(_ => _.Id, Guid.NewGuid)
                .RuleFor(_ => _.Quantity, _ => _.Random.Int(minQuantity, maxQuantity))
                .RuleFor(_ => _.UserId, _userLogged.Id);
        }

        private RescuedDotRequest GenerateRescuedDotRequest()
        {
            return new Faker<RescuedDotRequest>()
                .RuleFor(_ => _.Quantity, _ => _.Random.Int(0, 200));
        }

        private GetAllDotRequest GenerateGetAllDotRequest()
        {
            return new Faker<GetAllDotRequest>()
                .RuleFor(_ => _.PageIndex, 0)
                .RuleFor(_ => _.PageSize, 10);
        }

        private InsertDotRequest GenerateInsertDotRequest()
        {
            return new Faker<InsertDotRequest>()
                .RuleFor(_ => _.Quantity, _ => _.Random.Int(0, 200));
        }

        #endregion
    }
}