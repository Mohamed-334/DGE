using AutoMapper;
using DGE.Core.Features.Roles.Dto;
using DGE.Core.Features.Roles.Queries.Handler;
using DGE.Core.Features.Roles.Queries.RequestModels;
using DGE.Domain.Entities;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.PaginatedList;
using DGE.Tests.TestDoubles;
using Moq;
using System.Net;

namespace DGE.Tests.Features.Roles
{
    public class RoleHandlerQueryTests
    {
        private readonly Mock<IRoleService> _roleService = new();
        private readonly Mock<IMapper> _mapper = new();
        private readonly RoleHandlerQuery _handler;

        public RoleHandlerQueryTests()
        {
            _mapper.Setup(m => m.Map<RoleFullDataDto>(It.IsAny<Role>()))
                .Returns((Role role) => new RoleFullDataDto { Id = role.Id, Name = role.Name, NameLocalization = role.NameLocalization });
            _mapper.Setup(m => m.Map<List<RoleFullDataDto>>(It.IsAny<List<Role>>()))
                .Returns((List<Role> roles) => roles.Select(role => new RoleFullDataDto { Id = role.Id, Name = role.Name }).ToList());
            _handler = new RoleHandlerQuery(_roleService.Object, new KeyEchoLocalizer(), _mapper.Object);
        }

        [Fact]
        public async Task GetById_ExistingRole_ReturnsMappedDto()
        {
            using var cts = new CancellationTokenSource();
            _roleService.Setup(s => s.GetByIdNoTrackingAsync(7, cts.Token))
                .ReturnsAsync(new Role { Id = 7, Name = "Admin", NameLocalization = "مدير" });

            var response = await _handler.Handle(new GetRoleByIdQueryRequestModel { RoleId = 7 }, cts.Token);

            Assert.True(response.Succeeded);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Admin", response.Data!.Name);
            _roleService.Verify(s => s.GetByIdNoTrackingAsync(7, cts.Token), Times.Once);
        }

        [Fact]
        public async Task GetById_MissingRole_ReturnsNotFound()
        {
            _roleService.Setup(s => s.GetByIdNoTrackingAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync((Role?)null);

            var response = await _handler.Handle(new GetRoleByIdQueryRequestModel { RoleId = 99 }, CancellationToken.None);

            Assert.False(response.Succeeded);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Null(response.Data);
        }

        [Fact]
        public async Task GetList_ReturnsAllRolesWithTotalCountMeta()
        {
            _roleService.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync([new Role { Id = 1, Name = "Admin" }, new Role { Id = 2, Name = "User" }]);

            var response = await _handler.Handle(new GetRolesListQueryRequestModel(), CancellationToken.None);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(2, response.Data!.Count);
            Assert.Equal(2, response.Meta!.GetType().GetProperty("TotalCount")!.GetValue(response.Meta));
        }

        [Fact]
        public async Task GetPaginatedList_EmptyPage_ReturnsNotFound()
        {
            _roleService.Setup(s => s.GetPaginatedListAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(PaginatedList<Role>.Success([], 0, 1, 10));

            var response = await _handler.Handle(new GetRolesPaginatedListQueryRequestModel { PageNumber = 1, PageSize = 10 }, CancellationToken.None);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal(AppLocalizationKeys.NotFound, response.Message);
        }

        [Fact]
        public async Task GetPaginatedList_KeepsPagingMetadata()
        {
            _roleService.Setup(s => s.GetPaginatedListAsync(2, 1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(PaginatedList<Role>.Success([new Role { Id = 2, Name = "User" }], 3, 2, 1));

            var response = await _handler.Handle(new GetRolesPaginatedListQueryRequestModel { PageNumber = 2, PageSize = 1 }, CancellationToken.None);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(3, response.Data!.TotalCount);
            Assert.Equal(2, response.Data.CurrentPage);
            Assert.Equal(3, response.Data.TotalPages);
            Assert.Equal(2, Assert.Single(response.Data.Data).Id);
        }
    }
}
