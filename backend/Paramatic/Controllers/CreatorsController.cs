using Microsoft.AspNetCore.Mvc;
using Paramatic.Models;
using Paramatic.UnitOfWork;

namespace Paramatic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreatorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatorsController(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAllCreators()
        {
            var creators = _unitOfWork.Repository<Creator>().GetAll();
            return Ok(creators);
        }

        [HttpGet("{id}")]
        public IActionResult GetCreator(int id)
        {
            var creator = _unitOfWork.Repository<Creator>().GetById(id);
            if (creator == null) 
                return NotFound();

            return Ok(creator);
        }

        [HttpPost]
        public IActionResult CreateCreator(Creator creator)
        {
            _unitOfWork.Repository<Creator>().Add(creator);
            _unitOfWork.Complete();
            return CreatedAtAction(nameof(GetCreator), new { id = creator.Id }, creator);
        }
    }
}