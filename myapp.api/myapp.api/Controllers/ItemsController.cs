using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myapp.api.Data;
using myapp.api.Models;
using myapp.api.DTO;
using myapp.api.Services;
using Microsoft.AspNetCore.Authorization;


namespace myapp.api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;

        public ItemsController(AppDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Paczki>>> GetItems()
        {
            return await _context.Paczkis.ToListAsync();
        }

        [HttpGet("show_parcels")]
        public async Task<ActionResult<IEnumerable<Paczki>>> GetParcels()
        {
            try
            {
                var parcels = await _context.Paczkis.ToListAsync();
                return Ok(parcels);
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching parcels: {ex.Message}");
                return StatusCode(500, "An error occurred while fetching parcels.");
            }
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("show_users")]
        public async Task<ActionResult<IEnumerable<Pracownicy>>> GetUsers()
        {
            try
            {
                var users = await _context.Pracownicies.ToListAsync();
                return Ok(users);
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching parcels: {ex.Message}");
                return StatusCode(500, "An error occurred while fetching parcels.");
            }
        }

        [HttpPost("login")] //przesylamy dane aby sprawdzic czy znajduja sie one w bazie
        public async Task<IActionResult> Login ([FromBody] loginRequest request) //Frombody oznacza ze dane json z żadania zostana przypisane do klasy login request (przekonwertowane na c#)
        {
            var user = await _context.Pracownicies.FirstOrDefaultAsync(u => u.Login == request.Username);

            if (user == null)
                return Unauthorized("Invalid username or password");
            
            if(user.Hasło != request.Password)
            {
                return Unauthorized("Invalid username or password");
            }

            try
            {
                var token = _jwtService.GenerateToken(user);
                return Ok(new { Token = token });
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error generating token: {ex.Message}");
                return StatusCode(500, "Error generating token");
            }

        }

        
        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("add-user")]
        public async Task<IActionResult> AddUser([FromBody] CreateUserRequest request)
        {
            // Sprawdź, czy użytkownik o podanej nazwie już istnieje
            if (await _context.Pracownicies.AnyAsync(u => u.Login == request.Login))
            {
                return BadRequest("User with the same username already exists.");
            }

            // Utwórz nowego użytkownika
            var newUser = new Pracownicy
            {
                Imię = request.Imię,
                Nazwisko = request.Nazwisko,
                Adres = request.Adres,
                NrTelefonu = request.NrTelefonu,
                Login = request.Login,
                Hasło = request.Hasło,
                IdUprawnienia = request.IdUprawnienia
            };

            // Dodaj użytkownika do bazy danych
            _context.Pracownicies.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User added successfully", UserId = newUser.IdPracownika });
        }

        
        [HttpPost("add_parcel")]
        public async Task<IActionResult> AddParcel([FromBody] ParcelRequest newparceldata)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var new_parcel = new Paczki
                {
                    Gabaryt = newparceldata.Gabaryt,
                    KodPocztowyNadawcy = newparceldata.KodPocztowyNadawcy,
                    KodPocztowyOdbiorcy = newparceldata.KodPocztowyOdbiorcy,
                    DaneOdbiorcy = newparceldata.DaneOdbiorcy,
                    DaneNadawcy = newparceldata.DaneNadawcy,
                    AdresNadawcy = newparceldata.AdresNadawcy,
                    AdresOdbiorcy = newparceldata.AdresOdbiorcy,
                    DataNadania = DateTime.Now,
                    Status = newparceldata.Status,
                    CzyZniszczona = newparceldata.CzyZniszczona
                };


                _context.Paczkis.Add(new_parcel);
                await _context.SaveChangesAsync();

                return Ok(new{ Message = "Parcel added successfully", ParcelId = new_parcel.IdPaczki});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while adding the parcel.", Details = ex.Message });
            }
        }
        

        [HttpDelete("delete_parcel/{id}")]
        public async Task<IActionResult> DeleteParcel(int id)
        {
            var parcel = await _context.Paczkis.FindAsync(id);

            if (parcel == null)
            {
                return NotFound();
            }

            _context.Paczkis.Remove(parcel);
            await _context.SaveChangesAsync();

            return NoContent(); // Zwraca 204 No Content po pomyślnym usunięciu
        }

        [HttpDelete("delete_employee/{employeeId}")]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            var user = await _context.Pracownicies.FindAsync(employeeId);

            if (user == null)
            {
                return NotFound();
            }

            _context.Pracownicies.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent(); // Zwraca 204 No Content po pomyślnym usunięciu
        }

        [HttpGet("show_ladunki")]
        public async Task<ActionResult<IEnumerable<Ladunek>>> GetLadunki()
        {
            try
            {
                var ladunki = await _context.Laduneks.ToListAsync();
                return Ok(ladunki);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching ladunki: {ex.Message}");
                return StatusCode(500, "An error occurred while fetching ladunki.");
            }
        }

        // Endpoint do pobrania szczegółów ładunku na podstawie Id_Ladunku
        [HttpGet("show_ladunek/{id}")]
        public async Task<ActionResult<LadunekDetailsDto>> GetLadunekDetails(int id)
        {
            try
            {
                var ladunek = await _context.Laduneks
                    .FirstOrDefaultAsync(l => l.IdLadunek == id);

                if (ladunek == null)
                {
                    return NotFound($"Ladunek with ID {id} not found.");
                }

                // Przygotowanie DTO z szczegółami
                var ladunekDetails = new LadunekDetailsDto
                {
                    Id_Ladunku = ladunek.IdLadunek,
                    IdSamochodu = ladunek.IdSamochod,
                    Status = ladunek.Status
                };

                return Ok(ladunekDetails);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching ladunek details: {ex.Message}");
                return StatusCode(500, "An error occurred while fetching ladunek details.");
            }
        }

        [HttpGet("get_paczki_count/{idLadunku}")]
        public async Task<IActionResult> GetPaczkiCount(int idLadunku)
        {
            var count = await _context.PaczkaLaduneks
                .Where(p => p.IdLadunku == idLadunku)
                .CountAsync();

            return Ok(count);
        }
    }

}



