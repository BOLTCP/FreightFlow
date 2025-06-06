using FreightFlow.Context;
using FreightFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FreightFlow.Views
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customers.ToListAsync());
        }


        public IActionResult Details()
        {

            var customers = _context.Customers
                .Where(f => f.Id == Global1.ClientId).ToList();

            var model = new OrderHistoryViewModel
            {
                ClientId = Global1.ClientId,
                Customers = customers
            };

            return View(model);
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Phone,Password")] Customer customer)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    _context.Add(customer);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Inventory added successfully!";
                    return RedirectToAction("Create", "Customers");

                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "An error occurred while adding inventory.";
                }

            }
            return RedirectToAction("Login", "Customers");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Customer model)
        {
            Global1.ClientId = model.Id;

            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Password))
            {
                ViewBag.Error = "Please enter both username and password.";
                return View();
            }

            var customer = _context.Customers.FirstOrDefault(c => c.Name == model.Name && c.Password == model.Password);
            if (customer == null)
            {

                ViewBag.Error = "Invalid username or password.";
                return View(model);
            }
            else if (customer != null)
            {

                int id = Convert.ToInt32(customer.Id);
                Global1.ClientId = id;
            }
            return RedirectToAction("Dashboard", "Customers");

        }

        public IActionResult Dashboard()
        {
            return View();
        }


        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> CustomerEdit([Bind("Name,Email,Phone,Password")] Customer updatedItem)
        {
            if (ModelState.IsValid)
            {
                var existingItem = await _context.Customers.FindAsync(Global1.ClientId);


                TempData["EditConfirmation"] = "Customer details updated successfully.";

                // Check if updatedItem is null
                if (updatedItem == null)
                {
                    return BadRequest("Updated item is null."); // Handle the case where updatedItem is null
                }

                try
                {
                    existingItem.Name = updatedItem.Name;
                    existingItem.Phone = updatedItem.Phone;
                    existingItem.Email = updatedItem.Email;
                    existingItem.Password = updatedItem.Password;

                    _context.Customers.Update(existingItem);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Inventory added successfully!";
                    return View();

                }

                catch (Exception ex)
                {
                    // Log the exception and set an error message
                    TempData["ErrorMessage"] = "An error occurred while adding inventory.";
                }
            }
            return View();
        }

       /* [HttpGet]
        public IActionResult Delete()
        {
            return RedirectToAction("Details", "Customers");
        }
       */

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete()
        {
            var customer = await _context.Customers.FindAsync(Global1.ClientId);
          
                _context.Customers.Remove(customer);
           
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
