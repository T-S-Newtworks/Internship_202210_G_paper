using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Text;
using Internship_Template.Models.Entity;
using Internship_Template.Models.VM;


namespace Internship_Template.Controllers
{
    public class RecordController : _BaseController
    {
        // GET: Record
        public ActionResult Index()
        {
            TempData.Remove("model");
            カルテ一覧画面 model = new カルテ一覧画面();
            model.Chart = _dbDP.T_CHART.ToList() ?? new List<T_CHART>();  //.Where(x => x.I > 3)
            model.Doctor = _dbDP.T_DOCTOR.ToList() ?? new List<T_DOCTOR>();
            model.Patient = _dbDP.T_PATIENT.ToList() ?? new List<T_PATIENT>();
            model.DPUser = DPUser;


            return View(model);
        }

        //検索
        public ActionResult Search(string patientid, string patientname, DateTime? date)
        {
            TempData.Remove("model");
            カルテ一覧画面 model = new カルテ一覧画面();
            
            model.Chart = _dbDP.T_CHART.ToList() ?? new List<T_CHART>();
            model.Patient = _dbDP.T_PATIENT.ToList() ?? new List<T_PATIENT>();


            if (patientid != "" && patientname == ""){
                if(date != DateTime.MinValue && date != null ){
                    DateTime tom = date.Value.Date.AddDays(1);
                     //string strDate = datetime.ToString("yyyy/MM/dd");
                    model.Chart = _dbDP.T_CHART.Where(x => x.PATIENT_ID == patientid).Where(y => y.YMD >= date && y.YMD < tom)
                                    .ToList();
                }
                else {
                    model.Chart = _dbDP.T_CHART.Where(x => x.PATIENT_ID == patientid)
                                    .ToList();
                }
            }else if (patientid == "" && patientname != ""){
                T_PATIENT targetPatient = _dbDP.T_PATIENT.Where(y => y.NAME == patientname)
                                    .FirstOrDefault();
                if (date != DateTime.MinValue && date != null)
                {
                    DateTime tom = date.Value.Date.AddDays(1);
                    //string strDate = datetime.ToString("yyyy/MM/dd");
                    model.Chart = _dbDP.T_CHART.Where(x => x.PATIENT_ID == targetPatient.ID).Where(y => y.YMD >= date && y.YMD < tom)
                                    .ToList();
                }
                else
                {
                    model.Chart = _dbDP.T_CHART.Where(x => x.PATIENT_ID == targetPatient.ID)
                                    .ToList();
                }
            }else if (patientid != "" && patientname != "")
            {
                T_PATIENT targetPatient = _dbDP.T_PATIENT.Where(y => y.NAME == patientname)
                                    .FirstOrDefault();
                if (date != DateTime.MinValue && date != null)
                {
                    DateTime tom = date.Value.Date.AddDays(1);
                    //string strDate = datetime.ToString("yyyy/MM/dd");
                    model.Chart = _dbDP.T_CHART.Where(x => x.PATIENT_ID == patientid || x.PATIENT_ID == targetPatient.ID).Where(y => y.YMD >= date && y.YMD < tom)
                                    .ToList();
                }
                else
                {
                    model.Chart = _dbDP.T_CHART.Where(x => x.PATIENT_ID == patientid || x.PATIENT_ID == targetPatient.ID)
                                    .ToList();
                }
            }
            if (date != DateTime.MinValue && date != null)
            {
                DateTime tom = date.Value.Date.AddDays(1);
                //string strDate = datetime.ToString("yyyy/MM/dd");
                model.Chart = _dbDP.T_CHART.Where(y => y.YMD >= date && y.YMD < tom)
                                .ToList();
            }
            model.DPUser = DPUser;


            //一覧画面のViewに返す
            return View("Index", model);
        }

        public ActionResult Detail(string id)
        {
            TempData.Remove("model");
            カルテ詳細画面 model = new カルテ詳細画面();

            model.Chart = _dbDP.T_CHART.Where(x => x.ID == id)
                                    .FirstOrDefault();

            model.DPUser = DPUser;


            return View(model);
        }

        public ActionResult Create()
        {
            TempData.Remove("model");
            カルテ作成画面 model = new カルテ作成画面();
            //model.Chart = _dbDP.T_CHART.ToList() ?? new List<T_CHART>();  //.Where(x => x.I > 3)
            //model.Doctor = _dbDP.T_DOCTOR.ToList() ?? new List<T_DOCTOR>();
            //model.Patient = _dbDP.T_PATIENT.ToList() ?? new List<T_PATIENT>();
            model.DPUser = DPUser;


            return View(model);
        }

        public ActionResult CreateComplete(カルテ作成画面 model)
        {
            TempData.Remove("model");
            int Count = _dbDP.T_CHART.ToList().Count();
            model.Chart.ID = (Count + 1).ToString();
            // エンティティを追加＆データソースに反映
            _dbDP.T_CHART.Add(model.Chart);
            _dbDP.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}