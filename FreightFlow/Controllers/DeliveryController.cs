using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FreightFlow.Context;
using FreightFlow.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FreightFlow.Views
{
    public class DeliveryController: Controller
    {
        private readonly ApplicationDbContext _context;
        public int clientId = Global1.ClientId;
        public int? DeliveryId = Global1.DelId;
        public DeliveryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderDelivery([Bind("DeliveryId,DeliveryName,Origin,Contents,Destination,Weight,Length" +
            "Width", "Height", "ScheduledLoad", "ScheduledDelivery", "Status", "Description", "Global.ClientId")] FreightInventories FreightInventory)
        {

            if (ModelState.IsValid)
            {
                try
                {
                 FreightInventory.Id = clientId;
                    _context.Add(FreightInventory);
                await _context.SaveChangesAsync();

                    DeliveryId = FreightInventory.DeliveryId;

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
        public IActionResult OrderDelivery()
        {
            return View();
        }

        public IActionResult OrderHistory()
        {
            var freightInventories = _context.FreightInventory
                .Where(f => f.Id == clientId)
                .ToList();

            var model = new OrderHistoryViewModel
            {
                ClientId = clientId,
                FreightInventories = freightInventories
            };

            return View(model);
        }

        public IActionResult OrderEdit()
        {
            var freightInventories = _context.FreightInventory
                .Where(f => f.Id == clientId)
                .ToList();


            var model = new OrderHistoryViewModel
            {
                ClientId = clientId,
                FreightInventories = freightInventories
            };

            return View(model);
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id, FreightInventories updatedItem)
        {

            if (ModelState.IsValid)
            {
                var existingItem = await _context.FreightInventory.FindAsync(id);

                if (existingItem == null)
                {
                    return NotFound(); // If item not found, return NotFound status
                }

                // Check if updatedItem is null
                if (updatedItem == null)
                {
                    return BadRequest("Updated item is null."); // Handle the case where updatedItem is null
                }

                try
                {
                    existingItem.DeliveryName = updatedItem.DeliveryName;
                    existingItem.Origin = updatedItem.Origin;
                    existingItem.Contents = updatedItem.Contents;
                    existingItem.ScheduledLoad = updatedItem.ScheduledLoad;
                    existingItem.ScheduledDelivery = updatedItem.ScheduledDelivery;
                    existingItem.Status = updatedItem.Status;
                    existingItem.Description = updatedItem.Description;
                    existingItem.Destination = updatedItem.Destination;
                    existingItem.Width = updatedItem.Width;
                    existingItem.Weight = updatedItem.Weight;
                    existingItem.Length = updatedItem.Length;
                    existingItem.Height = updatedItem.Height;

                    _context.FreightInventory.Update(existingItem);
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

        public IActionResult OrderDelete()
        {
           
            var freightInventories = _context.FreightInventory
                .Where(f => f.Id == clientId)
                .ToList();


            var model = new OrderHistoryViewModel
            {
                ClientId = clientId,
                FreightInventories = freightInventories
            };

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Global1.DelId = id;
            var freightInventory = await _context.FreightInventory.FindAsync(id);
            if (freightInventory != null)
            {
                _context.FreightInventory.Remove(freightInventory);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("OrderHistory");
        }

    }
}