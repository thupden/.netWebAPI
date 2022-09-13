using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.ActionResults;
using my_books.Data.Models;
using my_books.Data.Services;
using my_books.Data.ViewModels;
using my_books.Exceptions;
using System;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        public PublishersService _publishersService;

        public PublishersController(PublishersService publishersService)
        {
            _publishersService = publishersService;
        }

        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers(string sortBy, string searchString, int pageNumber)
        {
           

            try
            {
                var _result = _publishersService.GetAllPublishers(sortBy,searchString,pageNumber);
                return Ok(_result);
            }
            catch (Exception)
            {

                return BadRequest("Sorry, we could not load the publishers");
            }
        }

        [HttpPost("add-publisher")]

        public IActionResult AddPublisher([FromBody]PublisherVM publisher)
        {
            try
            {
                var newPublisher = _publishersService.AddPublisher(publisher);
                return Created(nameof(AddPublisher), newPublisher);
            }
            catch(PublisherNameException ex)
            {
                return BadRequest($"{ex.Message},Publisher name:{ex.PublisherName}");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("get-publisher-by-id/{id}")]
        public CustomActionResult GetPublisherById(int id)
        {
            //throw new Exception("This is an exception that will be handled by middleware");
            var _response = _publishersService.GetPublisherById(id);
            if(_response!=null)
            {
                //return Ok(_response);
                var _responseObj = new CustomActionResultVM()
                {
                    Publisher = _response
                };
                return new CustomActionResult(_responseObj);
                //return _response;
            }
            else
            {
                var _responseObj = new CustomActionResultVM()
                {
                    Exception = new Exception("This is coming from publisher controller")
                };
                return new CustomActionResult(_responseObj);
                //eturn NotFound();
            }
        }
        [HttpGet("get-publisher-books-with-authors/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            var _response = _publishersService.GetPublisherData(id);
            return Ok(_response);
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            
            try
            {
                _publishersService.DeletePublisherById(id);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
