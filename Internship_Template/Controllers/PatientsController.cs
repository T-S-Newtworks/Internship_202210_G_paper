using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Internship_Template.Models.VM;
using Internship_Template.Models.Entity;

namespace Internship_Template.Controllers
{
    public class PatientsController : _BaseController
    {
        // GET: Patients
        public ActionResult Index()
        {
            //TempData.Remove("model");
            患者一覧画面 model = new 患者一覧画面();
            model.Patients = _dbDP.T_PATIENT.ToList() ?? new List<T_PATIENT>();
            model.DPUser = DPUser;
            //TempData["model"] = model;

            return View(model);
        }

        public ActionResult Search(string userId, string userName)
        {
            患者一覧画面 model = new 患者一覧画面();
            //一覧から取得するときに条件式で絞る
            if (userId != "")
            {
                model.Patients = _dbDP.T_PATIENT.Where(e => e.ID == userId)
                                    .ToList();
            }
            else
            {
                model.Patients = _dbDP.T_PATIENT.Where(e => e.NAME == userName)
                                    .ToList();
            }
            model.DPUser = DPUser;


            //一覧画面のViewに返す
            return View("Index", model);
        }

        public ActionResult Detail(string id)
        {
            患者一覧画面 model = new 患者一覧画面();
            //ユーザー画面 IndexData = (ユーザー画面)TempData["model"];
            model.DPUser = DPUser;
            model.TargetPatient = _dbDP.T_PATIENT.Where(e => e.ID == id).FirstOrDefault();
            //model.TargetUser = _db.T_USER.Where(e => e.ID == id).FirstOrDefault();
            //if (IndexData != null)
            //{
            //    model.Users = IndexData.Users;
            //}
            //TempData["model"] = model;

            return View(model);

        }

        public ActionResult Create()
        {
            患者一覧画面 model = new 患者一覧画面();
            model.DPUser = DPUser;

            return View(model);
        }

        public ActionResult CreateComplete(患者一覧画面 user)
        {

            // エンティティを追加＆データソースに反映
            _dbDP.T_PATIENT.Add(user.TargetPatient);
            _dbDP.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {

            患者一覧画面 model = new 患者一覧画面();
            model.DPUser = DPUser;
            model.TargetPatient = _dbDP.T_PATIENT.Where(e => e.ID == id).FirstOrDefault();
            //model.DoctorDepartment = _dbDP.T_DEPARTMENT.Where(e => e.CODE == model.TargetDoctor.DEPARTMENT_CD).FirstOrDefault();
            //model.DoctorHospital = _dbDP.T_HOSPITAL.Where(e => e.ID == model.TargetDoctor.HOSPITAL_ID).FirstOrDefault();

            //model.Departments = _dbDP.T_DEPARTMENT.ToList() ?? new List<T_DEPARTMENT>();
            //model.DepartmentsList = new List<SelectListItem>();
            //foreach (var department in model.Departments)
            //{
            //    model.DepartmentsList.Add(new SelectListItem
            //    {
            //        Text = department.NAME,
            //        Value = department.CODE,
            //    });
            //}

            //List<T_DEPARTMENT> test = _dbDP.T_DEPARTMENT.ToList() ?? new List<T_DEPARTMENT>();
            //string name = test[0].T_HOSPITAL.NAME;
            //model.Hospitals = _dbDP.T_HOSPITAL.ToList() ?? new List<T_HOSPITAL>();
            //model.HospitalsList = new List<SelectListItem>();
            //foreach (var hospital in model.Hospitals)
            //{
            //    model.HospitalsList.Add(new SelectListItem
            //    {
            //        Text = hospital.NAME,
            //        Value = hospital.ID,
            //    });
            //}

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditComplete(患者一覧画面 model)
        {
            if (ModelState.IsValid)
            {
                T_PATIENT beforeData = _dbDP.T_PATIENT.Single(e => e.ID == model.TargetPatient.ID);
                //T_HOSPITAL beforData2 = _dbDP.T_HOSPITAL.Single(e => e.ID == model.TargetDoctor.HOSPITAL_ID);
                //model.DoctorDepartment = _dbDP.T_DEPARTMENT.Where(e => e.CODE == model.TargetDoctor.DEPARTMENT_CD).FirstOrDefault();
                //model.DoctorHospital = _dbDP.T_HOSPITAL.Where(e => e.ID == model.TargetDoctor.HOSPITAL_ID).FirstOrDefault();

                //modelをそのまま突っ込むとbeforeModelの追跡情報が消えるので、Stateの変更時に新規追加扱いになる。
                //その結果PKの一意制約違反になるので登録できない。したがって変更した項目のみ代入する。
                beforeData.NAME = model.TargetPatient.NAME;
                beforeData.ID = model.TargetPatient.ID;
                beforeData.AGE = model.TargetPatient.AGE;
                beforeData.GENDER = model.TargetPatient.GENDER;
                beforeData.BIRTHDAY = model.TargetPatient.BIRTHDAY;
                beforeData.BLOODTYPE = model.TargetPatient.BLOODTYPE;
                beforeData.ADDRESS = model.TargetPatient.ADDRESS;
                beforeData.TEL = model.TargetPatient.TEL;
                beforeData.ALLERGY = model.TargetPatient.ALLERGY;
                beforeData.MEDICALHISTORY = model.TargetPatient.MEDICALHISTORY;
                beforeData.PREGNANCY = model.TargetPatient.PREGNANCY;
                
                //beforeData.HOSPITAL_ID = model.TargetDoctor.HOSPITAL_ID;
                //beforeData.DEPARTMENT_CD = model.TargetDoctor.DEPARTMENT_CD;

                _dbDP.Entry(beforeData).State = EntityState.Modified;
                _dbDP.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit", model);
        }
        public ActionResult Delete(string id)
        {

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            患者一覧画面 model = new 患者一覧画面();
            model.DPUser = DPUser;

            //model.TargetUser = _db.T_USER.Where(e => e.ID == id).FirstOrDefault();
            //if (model.TargetUser == null)
            //{
            //    return HttpNotFound();
            //}

            model.TargetPatient = _dbDP.T_PATIENT.Where(e => e.ID == id).FirstOrDefault();

            using (var tra = _db.Database.BeginTransaction())
            {
                try
                {
                    T_PATIENT patient = _dbDP.T_PATIENT.Find(model.TargetPatient.ID);
                    if (patient != null)
                    {
                        _dbDP.T_PATIENT.Remove(patient);
                        _dbDP.SaveChanges();
                    }

                    tra.Commit();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    tra.Rollback();
                }
            }

            model.Messages.Add("sakuzyo", "削除しました。");
            return RedirectToAction(nameof(Index));
        }
    }
}