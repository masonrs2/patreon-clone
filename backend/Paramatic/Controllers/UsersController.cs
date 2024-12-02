// using Microsoft.AspNetCore.Mvc;
// using Paramatic.Models;
// using Paramatic.UnitOfWork;

// [ApiController]
// [Route("[controller]")]
// public class UsersController : ControllerBase
// {
//     private readonly IUnitOfWork _unitOfWork;

//     public UsersController(IUnitOfWork unitOfWork)
//     {
//         _unitOfWork = unitOfWork;
//     }

//     [HttpGet]
//     public IActionResult GetAllUsers()
//     {
//         var users = _unitOfWork.Repository<User>().GetAll();
//         return Ok(users);
//     }

//     [HttpPost]
//     public IActionResult CreateUser(User user)
//     {
//         if (!ModelState.IsValid)
//         {
//             return BadRequest(ModelState);
//         }

//         _unitOfWork.Repository<User>().Add(user);
//         _unitOfWork.Complete();
//         return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
//     }

//     [HttpGet("{id}")]
//     public IActionResult GetUser(int id)
//     {
//         var user = _unitOfWork.Repository<User>().GetById(id);
//         if (user == null)
//         {
//             return NotFound();
//         }
//         return Ok(user);
//     }
// }