using invoice_demo_app.Basic.models;
using invoice_demo_app.Basic.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace invoice_app.Basic.controllers
{
    public abstract class IBaseController<T> : ControllerBase where T : BaseEntity
    { 
        public abstract IBasicService<T> GetService(); 


        // GET: api/T
        [HttpGet]
        public async Task<ActionResult<IEnumerable<T>>> GetAllEntities()
        {
            List<T> res = await GetService().FindAll().ToListAsync() ;
            return res;
        }

        // GET: api/Invoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<T>> GetEntity(int id)
        {
            T entity = await GetService().FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            return entity;
        }

        // POST: api/Invoices
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<T>> CreateEntity(T entity)
        {
            Console.Out.WriteLine("[HttpPost]");
            entity = await GetService().CreateAsync(entity);
            return CreatedAtAction("GetInvoice", new { id = entity.Id }, entity);
        }

        // PUT: api/Invoices/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEntity(int id, T entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }

            try
            {
                await GetService().UpdateAsync(entity);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Invoices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<T>> DeleteInvoice(int id)
        {
            T entity = await GetService().DeleteAsync(id); 
            return entity;
        }

        private bool ItemExists(int id)
        {
            return GetService().FindAll().Any(e => e.Id == id);
        }
    }
}
