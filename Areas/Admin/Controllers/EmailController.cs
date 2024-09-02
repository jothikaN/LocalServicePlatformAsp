using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using LocalServicePlatform.Domain.Models;

namespace FinProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmailController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public EmailController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendEmail(EmailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var mail = new MailMessage();
                    mail.From = new MailAddress("jothikajo01@gmail.com");
                    mail.To.Add(model.ToAddress);
                    mail.Subject = model.Subject;
                    mail.Body = model.Body;

                    if (model.AttachmentFile != null && model.AttachmentFile.Length > 0)
                    {
                        var attachmentFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", model.AttachmentFile.FileName);
                        try
                        {
                            using (var stream = new FileStream(attachmentFilePath, FileMode.Create))
                            {
                                model.AttachmentFile.CopyTo(stream);
                            }
                            mail.Attachments.Add(new Attachment(attachmentFilePath));
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Error = "Error attaching file: " + ex.Message;
                            return View("Index", model);
                        }
                    }

                    var smtpClient = new SmtpClient("smtp.gmail.com", 587);
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new NetworkCredential("jothikajo01@gmail.com", "tymr xejt tgre bpyx");

                    smtpClient.Send(mail);

                    ViewBag.Message = "Email sent successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Error sending email: " + ex.Message;
                }
            }

            return View("Index", model);
        }
    }
}

