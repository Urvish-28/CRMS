using CRMS1.Core.Models;
using CRMS1.Core.ViewModels;
using CRMS1.SQL.Repositories.SqlRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;

namespace CRMS1.SQL.Repositories.TicketAttachments
{
    public interface ITicketAttachmentRepository
    {
        void CreateAttachment(TicketViewModel obj);
        void DeleteAttachment(Guid IdList);
        void DeleteAttachmentByTicket(Guid TicketId);
        string GetImageName(Guid Id);
        IEnumerable<TicketAttachment> AttachmentList(Guid TicketId);
        TicketAttachment GetByTicketId(Guid TicketId);
        TicketAttachment GetById(Guid Id);
    }
    public class TicketAttachmentRepository : Page, ITicketAttachmentRepository
    {
        private readonly IRepository<TicketAttachment> _repository;
        private CRMSEntities _context;
        public TicketAttachmentRepository(IRepository<TicketAttachment> repository, CRMSEntities context)
        {
            _repository = repository;
            _context = context;
        }
        public void CreateAttachment(TicketViewModel obj)
        {
            TicketAttachment model = new TicketAttachment();
            model.TicketId = obj.Id;
            model.CreatedBy = (Guid)Session["UserId"];
            string fileExtention = System.IO.Path.GetExtension(obj.Image.FileName);
            string imageName = model.TicketId.ToString() + '_' + DateTime.Now.Ticks + fileExtention;
            string ImagePath = ConfigurationManager.AppSettings["TicketImage"] + imageName;
            obj.Image.SaveAs(HostingEnvironment.MapPath(ImagePath));
            model.FileName = imageName;
            _repository.Insert(model);
        }
        public void DeleteAttachment(Guid Id)
        {
            List<TicketAttachment> obj = _repository.Collection().Where(x => x.Id == Id).ToList();
            foreach (var item in obj)
            {
                item.IsDelete = true;
            }
            _repository.Commit();
        }
        public void DeleteAttachmentByTicket(Guid TicketId)
        {
            List<TicketAttachment> list = _repository.Collection().Where(x => x.TicketId == TicketId).ToList();
            foreach (var item in list)
            {
                item.IsDelete = true;
            }
            _repository.Commit();
        }
        public string GetImageName(Guid Id)
        {
            var imagename = _context.TicketAttachment.Where(x => x.Id == Id).Select(x => x.FileName).FirstOrDefault();
            return imagename;
        }
        public TicketAttachment GetByTicketId(Guid TicketId)
        {
            return _context.TicketAttachment.Where(x => x.TicketId == TicketId && x.IsDelete == false).FirstOrDefault();
        }
        public IEnumerable<TicketAttachment> AttachmentList(Guid TicketId)
        {
            return _context.TicketAttachment.Where(x => x.TicketId == TicketId && x.IsDelete == false).ToList();
        }
        public TicketAttachment GetById(Guid Id)
        {
            return _repository.Find(Id);
        }
    }
}
