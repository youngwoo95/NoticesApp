using Microsoft.AspNetCore.Mvc;
using NoticeApp.Models;

namespace NoticeApp.Apis.Controllers
{
    [Produces("application/json")]
    [Route("api/Notices")]
    public class NoticesController : ControllerBase
    {
        private readonly INoticeRepositoryAsync _repository;
        private readonly ILogger _logger;

        public NoticesController(INoticeRepositoryAsync repository, ILoggerFactory loggerFactory)
        {
            this._repository = repository;
            this._logger = loggerFactory.CreateLogger(nameof(NoticesController));
        }

        // 입력
        // POST api/Notices
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody]Notice model)
        {
            var tempModel = new Notice();
            tempModel.Name = model.Name;
            tempModel.Title = model.Title;
            tempModel.Content = model.Content;
            tempModel.ParentId = model.ParentId;
            tempModel.Created = DateTime.Now;


            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var newModel = await _repository.AddAsync(tempModel);
                if(newModel == null)
                {
                    return BadRequest();
                }
                
                //return Ok(newModel);

                var uri = Url.Link("GetNoticeById", new { id = newModel.Id });
                return Created(uri, newModel); // 201 Created
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }

        }

        // 출력
        // GET api/Notices
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var notices = await _repository.GetAllAsync();
                return Ok(notices); // 200 OK 와 함께 값을 반환해준다.
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(); // 400 BadRequest
            }
        }

        // 상세
        // GET api/Notice/1
        [HttpGet("{id}", Name ="GetNoticeById")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            try
            {
                var model = await _repository.GetByIdAsync(id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // 수정
        // PUT api/Notices/1
        [HttpPut("{id}")]
        public async Task<IActionResult> EditAsync(int id, [FromBody]Notice model)
        {
            model.Id = id;

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var status = await _repository.EditAsync(model);
                if (!status)
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // 삭제
        // DELETE api/Notices/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var status = await _repository.DeleteAsync(id);
                if (!status)
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // 페이징
        // GET api/Notices/Page/0/10
        [HttpGet("Page/{pageIndex}/{pageSize}")]
        public async Task<IActionResult> GetAll(int pageIndex, int pageSize)
        {
            try
            {
                var resultsSet = await _repository.GetAllAsync(pageIndex, pageSize);

                // 응답 헤더에 총 레코드 수를 담아서 출력
                Response.Headers.Add("X-TotalRecordCount", resultsSet.TotalRecords.ToString());
                Response.Headers.Add("Access-Control-Expose-Headers", "X-TotalRecordCount");

                return Ok(resultsSet.Records);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }


    }

}
