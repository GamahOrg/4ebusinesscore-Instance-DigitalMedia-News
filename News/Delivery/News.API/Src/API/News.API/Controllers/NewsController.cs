using News.Application.Features.NewsFeatures.Query.GetMainHomeNews;

namespace News.API.Controller
{
  //  [AllowAnonymous]
    [RequestSizeLimit(1048576000)] // 100 MB
    [RequestFormLimits(MultipartBodyLengthLimit = 1048576000)] // 100 MB
    public class NewsController : BaseController
    {
        private readonly IMediator _mediator;
        public NewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<ResponseDTO> GetAllNews()
        {
            return _mediator.Send(new GetAllNewsQuery());
        }

        [HttpGet("GetAllNewsPaginatedFiltered")]
        public Task<ResponseDTO> GetAllNewsPaginatedFiltered(string? title, DateTime? startDate, int pageNumber, int pageSize)
        {
            return _mediator.Send(new GetAllNewsPaginatedQuery
            {
                StartDate = startDate,
                Title = title,
                PageNumber = pageNumber,
                PageSize = pageSize,
            });
        }

        [HttpGet("{newsId}")]
        public Task<ResponseDTO> GetNewsById(Guid newsId)
        {
            return _mediator.Send(new GetNewsByIdQuery { NewsId = newsId });
        }
        [HttpGet("GetAllNewsByAuthorId")]
        public Task<ResponseDTO> GetAllNewsByAuthorId(Guid authorId, int pageNumber, int pageSize)
        {
            return _mediator.Send(new GetAllNewsByAuthorIdQuery
            {
                AuthorId = authorId,
                PageNumber = pageNumber,
                PageSize = pageSize,
            });
        }
        [HttpPost]
        public Task<ResponseDTO> PostNews([FromForm] PostNewsDto newsDto)
        {
            return _mediator.Send(new PostNewsCommand { NewsDto = newsDto });
        }

        [HttpPut("{newsId}")]
        public Task<ResponseDTO> PutNews([FromForm] PutNewsDto newsDto, Guid newsId)
        {
            return _mediator.Send(new PutNewsCommand { NewsId = newsId, NewsDto = newsDto });
        }

        [HttpPut("PutPublishNews/{newsId}")]
        public Task<ResponseDTO> PutPublishNews(Guid newsId)
        {
            return _mediator.Send(new PutPublishNewsCommand { NewsId = newsId });
        }

        //[HttpPut("ChangeNewsState/{newsId}")]
        //      public Task<ResponseDTO> ChangeNewsState([FromRoute] Guid newsId)
        //      {
        //          return _mediator.Send(new ChangeNewsStateCommand { NewsId = newsId });
        //      }

        [HttpDelete("{newsId}")]
        public Task<ResponseDTO> DeleteNews(Guid newsId)
        {
            return _mediator.Send(new DeleteNewsCommand { NewsId = newsId });
        }

        [HttpGet]
        [Route("GetAllLatestNewsPaginated")]
        public async Task<ResponseDTO> GetAllLatestNewsPaginated(int pageNumber, int pageSize) => await _mediator.Send(new GetAllLeatestPaginatedQuery(pageNumber, pageSize));

        [HttpGet]
        [Route("GetAllLeatestNews")]
        public async Task<ResponseDTO> GetAllLeatestNews() => await _mediator.Send(new GetAllLeatestQuery());

        [HttpGet]
        [Route("GetAllImportantNews")]
        public async Task<ResponseDTO> GetAllImportantNews() => await _mediator.Send(new GetImportantNewsQuery());

        [HttpGet]
        [Route("GetAllMainNews")]
        public async Task<ResponseDTO> GetAllMainNews() => await _mediator.Send(new GetMainNewsQuery());
        [HttpGet]
        [Route("GetAllMainHomeNews")]
        public async Task<ResponseDTO> GetAllMainHomeNews(int pageNumber, int pageSize) => await _mediator.Send(new GetMainHomeNewsQuery()
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        });

        [HttpGet]
        [Route("GetAllSimilarNewsPaginated/{newsId}")]
        public async Task<ResponseDTO> GetAllSimilarNewsPaginated(Guid newsId, int pageNumber, int pageSize) => await _mediator.Send(new GetSimilarNewsQuery(newsId, pageNumber, pageSize));

        [HttpGet, Route("GetNewsDetails/{id}")]
        public async Task<ResponseDTO> GetNewsDetails(Guid id) => await _mediator.Send(new GetNewsDetailsQuery(id));

        [HttpPost, Route("PostNewsRating")]
        public async Task<ResponseDTO> PostNewsRating([FromBody] PostNewsRatingCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
