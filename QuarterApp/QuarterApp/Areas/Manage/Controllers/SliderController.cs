using Microsoft.AspNetCore.Mvc;
using QuarterApp.DAL;
using QuarterApp.Helpers;
using QuarterApp.Models;
using Microsoft.AspNetCore.Authorization;


namespace QuarterApp.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]

    public class SliderController : Controller
    {
        private readonly QuarterDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(QuarterDbContext context,IWebHostEnvironment env)
        {
            _context= context;
            _env = env;
        }
        public IActionResult Index()
        {
            var model = _context.Sliders.OrderBy(x => x.Order).ToList();



            return View(model);
        }
        public IActionResult Create()
        {
            var slider = _context.Sliders.OrderByDescending(x => x.Order).FirstOrDefault();
            int order = slider == null ? 1 : slider.Order + 1;
            ViewBag.Order = order;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            if (slider.ImageFile != null && slider.ImageFile.ContentType != "image/png" && slider.ImageFile.ContentType != "image/jpeg" && slider.ImageFile.ContentType != "image/jpg")
            {
                ModelState.AddModelError("ImageFile", "Content type must be image/png or image/jpeg or image/jpg");
                return View();
            }

            if(slider.ImageFile != null && slider.ImageFile.Length> 3145728)
            {
                ModelState.AddModelError("ImageFile", "ImageFile size must be lower than 3MB");
                return View();
            }



            if (!ModelState.IsValid)
                return View();


            slider.Image = FileManager.Save(slider.ImageFile, _env.WebRootPath, "uploads/sliders");

            _context.Sliders.Add(slider);
            _context.SaveChanges();


            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var slider=_context.Sliders.FirstOrDefault(x=>x.Id == id);

            if (slider == null)
                return RedirectToAction("error", "dashboard");

            return View(slider);
        }
        [HttpPost]
        public IActionResult Edit(Slider slider)
        {
            if (slider.ImageFile != null && slider.ImageFile.ContentType != "image/png" && slider.ImageFile.ContentType != "image/jpeg" && slider.ImageFile.ContentType != "image/jpg")
            {
                ModelState.AddModelError("ImageFile", "Content type must be image/png or image/jpeg or image/jpg");
                return View();
            }

            if (slider.ImageFile != null && slider.ImageFile.Length > 3145728)
            {
                ModelState.AddModelError("ImageFile", "ImageFile size must be lower than 3MB");
                return View();
            }

            if (!ModelState.IsValid)
                return View();

            Slider existedSlider = _context.Sliders.FirstOrDefault(x=>x.Id==slider.Id);

            if (existedSlider == null)
                return RedirectToAction("error", "dashboard");

            if (slider.ImageFile != null)
            {
                var newImageName = FileManager.Save(slider.ImageFile, _env.WebRootPath, "uploads/sliders");

                FileManager.Delete(_env.WebRootPath,"uploads/sliders",existedSlider.Image);

                existedSlider.Image= newImageName;
            }


            existedSlider.Order = slider.Order;
            existedSlider.Tittle1 = slider.Tittle1;
            existedSlider.Tittle2 = slider.Tittle2;
            existedSlider.Desc = slider.Desc;
            existedSlider.FirstBtnText = slider.FirstBtnText;
            existedSlider.FirstBtnUrl = slider.FirstBtnUrl;
            existedSlider.SecondBtnText = slider.SecondBtnText;
            existedSlider.SecondBtnUrl = slider.SecondBtnUrl;
            existedSlider.FirstBtnUrl = slider.FirstBtnUrl;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var slider=_context.Sliders.FirstOrDefault(x=>x.Id== id);

            if (slider == null)
                return RedirectToAction("error", "dashboard");

            FileManager.Delete(_env.WebRootPath, "uploads/sliders", slider.Image);

            _context.Sliders.Remove(slider);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
