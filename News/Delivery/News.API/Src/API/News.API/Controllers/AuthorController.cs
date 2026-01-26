using News.Application.Features.Author.Command.ChangeState;

namespace News.API.Controller
{
    public class AuthorController :BaseController
	{
        private readonly IMediator _mediator;
        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("PostAuthor")]
        public async Task<ResponseDTO> PostAuthor(PostAuthorCommand command) => await _mediator.Send(command);

        [HttpGet]
        [Route("GetAllAuthorsPaginated")]
        public async Task<ResponseDTO> GetAllAuthorsPaginated(int pageNumber, int pageSize, string? searchTerm)
        {
            var query = new GetAllAuthorsPaginatedQuery(pageNumber, pageSize, searchTerm);
            return await _mediator.Send(query);
        }

        [HttpGet]
        [Route("GetAllAuthors")]
        public async Task<ResponseDTO> GetAllAuthors()
        {
            var query = new GetAllAuthorsQuery();
            return await _mediator.Send(query);
        }

        [HttpGet, Route("GetAuthorById/{id}")]
        public async Task<ResponseDTO> GetAuthorById(Guid id) => await _mediator.Send(new GetAuthorByIdQuery(id));

        [HttpPut]
        [Route("PutAuthor/{id}")]
        public async Task<ResponseDTO> PutAuthor([FromRoute] Guid id, PutAuthorCommand command)
        {
            command.AuthorId = id;
            return await _mediator.Send(command);
        }

        [HttpPut]
        [Route("ChangeAuthorState/{id}")]
        public async Task<ResponseDTO> ChangeAuthorState([FromRoute] Guid id)
        {
            return await _mediator.Send(new ChangeAuthorStateCommand { AuthorId = id} );
        }

        [HttpDelete]
        [Route("DeleteAuthor/{id}")]
        public async Task<ResponseDTO> DeleteAuthor(Guid id) => await _mediator.Send(new DeleteAuthorCommand(id));
    }
}
