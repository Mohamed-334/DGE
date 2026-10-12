using DGE.Core.Features.Notifications.Dto;
using DGE.Core.Shared.Models;
using DGE.Service.Shared.PaginatedList;
using MediatR;

namespace DGE.Core.Features.Notifications.Queries.RequestModels
{
    public class GetNotificationsPaginatedListQueryRequestModel : IRequest<Response<PaginatedList<NotificationFullDataDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public GetNotificationsPaginatedListQueryRequestModel(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        public GetNotificationsPaginatedListQueryRequestModel() { }
    }
}
