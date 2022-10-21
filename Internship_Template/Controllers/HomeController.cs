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
using System.Web.Helpers;
using System.IO;

namespace Internship_Template.Controllers
{
    /// <summary>
    /// Top画面のコントローラです。
    /// </summary>
    /// <remarks>Top画面に機能を追加したい場合はここに処理を追加しましょう。</remarks>
    public class HomeController : _BaseController
    {
        public ActionResult Index(string hospitalId = null)
        {
            TempData.Remove("model");
            TOP画面 model = new TOP画面();
            model.Users = _db.T_USER.ToList() ?? new List<T_USER>();
            model.DPUser = DPUser;
            model.Departments = _dbDP.T_DEPARTMENT.ToList() ?? new List<T_DEPARTMENT>();
            model.DepartmentsList = new List<SelectListItem>();
            foreach(var department in model.Departments)
            {
                model.DepartmentsList.Add(new SelectListItem
                {
                    Text = department.NAME,
                    Value = department.CODE,
                });
            }

            List<T_DEPARTMENT> test = _dbDP.T_DEPARTMENT.ToList() ?? new List<T_DEPARTMENT>();
            string name = test[0].T_HOSPITAL.NAME;

            model.Hospitals = _dbDP.T_HOSPITAL.ToList() ?? new List<T_HOSPITAL>();
            model.HospitalsList = new List<SelectListItem>();
            foreach (var hospital in model.Hospitals)
            {
                if (hospital.ID == hospitalId)
                {
                    model.HospitalsList.Add(new SelectListItem
                    {
                        Text = hospital.NAME,
                        Value = hospital.ID,
                        Selected = true,
                    });
                } else
                {
                    model.HospitalsList.Add(new SelectListItem
                    {
                        Text = hospital.NAME,
                        Value = hospital.ID,
                        Selected = false,
                    });
                }
            }

            

            return View(model);
        }

        //public ActionResult Index()
        //{
        //    TempData.Remove("model");
        //    TOP画面 model = new TOP画面();
        //    model.DepartMents = _dbDP.T_DEPARTMENT.ToList() ?? new List<T_DEPARTMENT>();
        //    //model.LoginedUser = User;

        //    return View(model);
        //}

        public ActionResult MakeChart(string hospitalID = null)
        {
            TempData.Remove("model");
            TOP画面 model = new TOP画面();
            model.Charts = _dbDP.T_CHART.ToList() ?? new List<T_CHART>();
            model.Patients = _dbDP.T_PATIENT.ToList() ?? new List<T_PATIENT>();
            model.Doctors = _dbDP.T_DOCTOR.ToList() ?? new List<T_DOCTOR>();




            var allPatients = 0;
            string title = "患者数";
            List<string> doctorName = new List<string>();
            List<int> patients = new List<int>();
            foreach (var data in model.Doctors)
            {
                if(data.T_CHART.Count > 0)
                {
                    if(hospitalID == null || hospitalID == "0")
                    {
                        doctorName.Add(data.NAME + "先生：" + data.T_CHART.Count + "人");
                        patients.Add(data.T_CHART.Count);
                        allPatients += data.T_CHART.Count;
                    }

                    if(data.HOSPITAL_ID == hospitalID)
                    {
                        doctorName.Add(data.NAME + "先生："　+ data.T_CHART.Count + "人");
                        patients.Add(data.T_CHART.Count);
                        allPatients += data.T_CHART.Count;
                    }
                    //else
                    //{
                    //    doctorName.Add(data.NAME + "先生：" + data.T_CHART.Count + "人");
                    //    patients.Add(data.T_CHART.Count);
                    //    allPatients += data.T_CHART.Count;
                    //}
                    
                }


            };
            title = title + allPatients.ToString();

            //string x = "患者数" + allPatients.ToString();


            var chart = new Chart(width: 600, height: 600)
            //.AddTitle("患者数" + allPatients.ToString())
            .AddTitle(title)
            .AddSeries("Default", chartType: "Pie",
            //xValue: new[] { "Peter", "Andrew", "Julie", "Mary", "Dave" }, xField: "Name",
            xValue: doctorName, xField: "DoctorName",
            //yValues: new[] { "2", "6", "4", "5", "3" }, yFields: "Sales")
            yValues: patients, yFields: "Patients");


            //T_CHARTテーブルのDOCTOR_IDとT_DOCTORテーブルのIDを結合してNAMEを取り出したい
            //var result = from c in _dbDP.T_CHART
            //             join d in _dbDP.T_DOCTOR on c.DOCTOR_ID equals d.ID

            //model.Charts = _dbDP.T_CHART.ToList() ?? new List<T_CHART>();

            //var charts = _dbDP.T_CHART.ToList();

            //string doctorName = charts[0].T_DOCTOR.NAME;

            //model.Charts[0].T_DOCTOR.NAME;

            return View(chart);
        }

        //public ActionResult DepartmentSelect()
        //{
        //    ViewBag.SelectOptions = new SelectListItem[]
        //    {
        //        new SelectListItem() { Value="Internal-Medicine", Text="内科" },
        //        new SelectListItem() { Value="Pediatrics", Text="小児科" },
        //        new SelectListItem() { Value="General-Surger", Text="外科" },
        //        new SelectListItem() { Value="Orthopedic-Surgery", Text="整形外科" },
        //        new SelectListItem() { Value="Ophthalmology", Text="眼科" },
        //        new SelectListItem() { Value="Otorhinolaryngology", Text="耳鼻咽喉科" },
        //        new SelectListItem() { Value="Dermatology", Text="皮膚科" },
        //        new SelectListItem() { Value="Urology", Text="泌尿器科" },
        //        new SelectListItem() { Value="Radiology", Text="放射線科" },
        //        new SelectListItem() { Value="Neurological-Surgery", Text="脳神経外科" },
        //        new SelectListItem() { Value="Obstetrics-Gynecology", Text="産科・婦人科" },
        //        new SelectListItem() { Value="Psychiatry", Text="精神科" }
        //    };

        //    return View();
        //}

    }
}