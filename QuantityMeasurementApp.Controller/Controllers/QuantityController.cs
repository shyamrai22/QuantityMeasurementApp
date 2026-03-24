using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementApp.Model.DTO;
using QuantityMeasurementApp.Service;

namespace QuantityMeasurementApp.Controller.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class QuantityController : ControllerBase
  {
    private readonly IQuantityMeasurementService service;

    public QuantityController(IQuantityMeasurementService service)
    {
      this.service = service;
    }

    // ===================== COMPARE =====================

    [HttpPost("compare")]
    public IActionResult Compare([FromBody] CompareRequest request)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var result = service.Compare(request.First, request.Second);
      return Ok(result);
    }

    // ===================== CONVERT =====================

    [HttpPost("convert")]
    public IActionResult Convert([FromBody] ConvertRequest request)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var result = service.Convert(request.Source, request.TargetUnit);
      return Ok(result);
    }

    // ===================== ADD =====================

    [HttpPost("add")]
    public IActionResult Add([FromBody] CompareRequest request)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var result = service.Add(request.First, request.Second);
      return Ok(result);
    }

    // ===================== SUBTRACT =====================

    [HttpPost("subtract")]
    public IActionResult Subtract([FromBody] CompareRequest request)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var result = service.Subtract(request.First, request.Second);
      return Ok(result);
    }

    // ===================== DIVIDE =====================

    [HttpPost("divide")]
    public IActionResult Divide([FromBody] CompareRequest request)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var result = service.Divide(request.First, request.Second);
      return Ok(result);
    }

    // ===================== GET RECORDS =====================

    [HttpGet("records")]
    public IActionResult GetAll()
    {
      var data = service.GetAll();
      return Ok(data);
    }
  }
}