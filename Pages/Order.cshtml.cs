
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OrdenDeCompraP2.Data;
using System.Text;
using System.Threading.Tasks;

    namespace OrdenDeCompraP2.Pages
    {
        public class OrderModel : PageModel
        {
            private readonly ApplicationDbContext _context;

            public OrderModel(ApplicationDbContext context)
            {
                _context = context;
            }

            [BindProperty]
            public Order NewOrder { get; set; }

            public async Task<IActionResult> OnPostAsync()
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                _context.Orders.Add(NewOrder);
                await _context.SaveChangesAsync();  // Guardar los cambios en la base de datos

                return RedirectToPage("./OrderList");  // Redirecciona a una página para ver las órdenes
            }

       

    }
}

