
namespace Events.API.Controller
{
    public class LK_EventsStatusController : BaseController
    {
        private readonly IMediator _mediator;

        public LK_EventsStatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Post

        [HttpPost]
        public async Task<ResponseDTO> Post([FromForm] PosLLk_NewsStatusCommand PostPositionCommand)
        {
            return await _mediator.Send(PostPositionCommand);
        }
        #endregion

        #region Put

        [HttpPut]
        public async Task<ResponseDTO> Put([FromForm] PutLk_NewsStatusCommand PutPositionComand)
        {
            return await _mediator.Send(PutPositionComand);
        }
        #endregion

        #region Delete
        [HttpDelete("Delete/{Id}")]
        public async Task<ResponseDTO> Delete(int Id)
        {
            return await _mediator.Send(new DeleteLk_NewsStatusCommand { Id = Id });
        }
        #endregion

        #region Get
        [HttpGet]
        public async Task<ResponseDTO> Get()
        {
            return await _mediator.Send(new GetAllLK_EntityMemberQuery { });

        }
        #endregion
    }
}
