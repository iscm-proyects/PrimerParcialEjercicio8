using Microsoft.AspNetCore.Mvc;
using PrimerParcialEjercicio8.Models;

namespace PrimerParcialEjercicio8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly ExpressionEvaluator _evaluator;

        public CalculatorController()
        {
            _evaluator = new ExpressionEvaluator();
        }

        // Endpoint para evaluar una expresión infija
        [HttpGet("infix")]
        public IActionResult EvaluateInfix([FromQuery] string expression)
        {
            try
            {
                double result = _evaluator.EvaluateInfix(expression);
                return Ok(new { Result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        // Endpoint para evaluar una expresión prefija
        [HttpGet("prefix")]
        public IActionResult EvaluatePrefix([FromQuery] string expression)
        {
            try
            {
                double result = _evaluator.EvaluatePrefix(expression);
                return Ok(new { Result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}