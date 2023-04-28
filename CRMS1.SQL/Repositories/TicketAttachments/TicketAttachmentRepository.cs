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
        void UpdateAttachment(TicketViewModel obj);
        void DeleteAttachment(Guid Id);
    }
    public class TicketAttachmentRepository :Page, ITicketAttachmentRepository
    {
        private readonly IRepository<TicketAttachment> _repository;
        public TicketAttachmentRepository(IRepository<TicketAttachment> repository)
        {
            _repository = repository;
        }
        public void CreateAttachment(TicketViewModel obj)
        {
            TicketAttachment model = new TicketAttachment();
            model.TicketId = obj.Id;
            model.CreatedBy = (Guid)Session["UserId"];
            string fileExtention = System.IO.Path.GetExtension(obj.Image.FileName);
            string imageName = model.TicketId.ToString() + '_' + DateTime.Now.Ticks + fileExtention;
            string ImagePath = ConfigurationManager.AppSettings["TicketImage"] +'_' + imageName;
           // string ImagePath = "~/Content/TicketImage/" + imageName;
            obj.Image.SaveAs(HostingEnvironment.MapPath(ImagePath));
            model.FileName = imageName;
            _repository.Insert(model);
        }
        public void UpdateAttachment(TicketViewModel obj)
        {
            throw new NotImplementedException();
        }
        public void DeleteAttachment(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
