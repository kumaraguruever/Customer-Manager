using CustomerManagementApp.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace CustomerManagementApp.Controllers.api
{
    public class CustomerController : ApiController
    {
        private CustomerDBContext db = new CustomerDBContext();

        // GET: api/Customer
        public IQueryable<CustomerModel> GetCustomer()
        {
            return db.Customers;
        }

        // GET: api/Customer/5
        [ResponseType(typeof(CustomerModel))]
        public async Task<IHttpActionResult> GetCustomerModel(int id)
        {
            CustomerModel customerModel = await db.Customers.FindAsync(id);
            if (customerModel == null)
            {
                return NotFound();
            }

            return Ok(customerModel);
        }

        // PUT: api/Customer/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCustomerModel(int id, CustomerModel customerModel)
        {

            ValidateUniqueEmailAddress(customerModel);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customerModel.CustomerID)
            {
                return BadRequest();
            }

            db.Entry(customerModel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        public bool IsEmailExist(string emailAddress, int? ID)
        {
         
            return db.Customers.Any(customer => customer.EmailAddress == emailAddress
                                    && customer.CustomerID != ID);
        }

        private void ValidateUniqueEmailAddress(CustomerModel customerModel)
        {
            if (IsEmailExist(customerModel.EmailAddress, customerModel.CustomerID))
            {
                ModelState.AddModelError("EmailAddress", "Email Address is already exists");
            }
        }

        // POST: api/Customer
        [ResponseType(typeof(CustomerModel))]
        public async Task<IHttpActionResult> PostCustomerModel(CustomerModel customerModel)
        {
            ValidateUniqueEmailAddress(customerModel);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customers.Add(customerModel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = customerModel.CustomerID }, customerModel);
        }

        // DELETE: api/Customer/5
        [HttpDelete]
        [ResponseType(typeof(CustomerModel))]
        public async Task<IHttpActionResult> DeleteCustomerModel(int id)
        {
            CustomerModel customerModel = await db.Customers.FindAsync(id);
            if (customerModel == null)
            {
                return NotFound();
            }

            db.Customers.Remove(customerModel);
            await db.SaveChangesAsync();

            return Ok(customerModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerModelExists(int id)
        {
            return db.Customers.Count(e => e.CustomerID == id) > 0;
        }
    }
}