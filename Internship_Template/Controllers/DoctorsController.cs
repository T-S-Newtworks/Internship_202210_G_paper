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
    public class DoctorsController : _BaseController
    {
        // GET: Doctors
        public ActionResult Index()
        {
            先生一覧画面 model = new 先生一覧画面();
            model.Doctors = _dbDP.T_DOCTOR.ToList() ?? new List<T_DOCTOR>();
            model.DPUser = DPUser;
            return View(model);
        }

        public ActionResult Search(string userId, string userName)
        {
            先生一覧画面 model = new 先生一覧画面();
            //一覧から取得するときに条件式で絞る
            if (userId != "")
            {
                model.Doctors = _dbDP.T_DOCTOR.Where(e => e.ID == userId)
                                    .ToList();
            }
            else
            {
                model.Doctors = _dbDP.T_DOCTOR.Where(e => e.NAME == userName)
                                    .ToList();
            }
            model.DPUser = DPUser;


            //一覧画面のViewに返す
            return View("Index", model);
        }

        public ActionResult Detail(string id)
        {
            先生一覧画面 model = new 先生一覧画面();
            //ユーザー画面 IndexData = (ユーザー画面)TempData["model"];
            model.DPUser = DPUser;
            model.TargetDoctor = _dbDP.T_DOCTOR.Where(e => e.ID == id).FirstOrDefault();
            model.DoctorDepartment = _dbDP.T_DEPARTMENT.Where(e => e.CODE == model.TargetDoctor.DEPARTMENT_CD).FirstOrDefault();
            model.DoctorHospital = _dbDP.T_HOSPITAL.Where(e => e.ID == model.TargetDoctor.HOSPITAL_ID).FirstOrDefault();
            //if (IndexData != null)
            //{
            //    model.Users = IndexData.Users;
            //}
            //TempData["model"] = model;

            return View(model);

        }

        public ActionResult Create()
        {
            先生一覧画面 model = new 先生一覧画面();
            model.DPUser = DPUser;

            return View(model);
        }

        public ActionResult CreateComplete(先生一覧画面 user)
        {

                // エンティティを追加＆データソースに反映
                _dbDP.T_DOCTOR.Add(user.TargetDoctor);
                _dbDP.SaveChanges();
           
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {

            先生一覧画面 model = new 先生一覧画面();
            model.DPUser = DPUser;
            model.TargetDoctor = _dbDP.T_DOCTOR.Where(e => e.ID == id).FirstOrDefault();
            model.DoctorDepartment = _dbDP.T_DEPARTMENT.Where(e => e.CODE == model.TargetDoctor.DEPARTMENT_CD).FirstOrDefault();
            model.DoctorHospital = _dbDP.T_HOSPITAL.Where(e => e.ID == model.TargetDoctor.HOSPITAL_ID).FirstOrDefault();

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
        public ActionResult EditComplete(先生一覧画面 model)
        {
            if (ModelState.IsValid)
            {
                T_DOCTOR beforeData = _dbDP.T_DOCTOR.Single(e => e.ID == model.TargetDoctor.ID);
                //T_HOSPITAL beforData2 = _dbDP.T_HOSPITAL.Single(e => e.ID == model.TargetDoctor.HOSPITAL_ID);
                //model.DoctorDepartment = _dbDP.T_DEPARTMENT.Where(e => e.CODE == model.TargetDoctor.DEPARTMENT_CD).FirstOrDefault();
                //model.DoctorHospital = _dbDP.T_HOSPITAL.Where(e => e.ID == model.TargetDoctor.HOSPITAL_ID).FirstOrDefault();

                //modelをそのまま突っ込むとbeforeModelの追跡情報が消えるので、Stateの変更時に新規追加扱いになる。
                //その結果PKの一意制約違反になるので登録できない。したがって変更した項目のみ代入する。
                beforeData.NAME = model.TargetDoctor.NAME;
                beforeData.ID = model.TargetDoctor.ID;
                beforeData.AGE = model.TargetDoctor.AGE;
                beforeData.GENDER = model.TargetDoctor.GENDER;
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

            先生一覧画面 model = new 先生一覧画面();
            model.DPUser = DPUser;
            model.TargetDoctor = _dbDP.T_DOCTOR.Where(e => e.ID == id).FirstOrDefault();
            
            using (var tra = _db.Database.BeginTransaction())
            {
                try
                {
                    T_DOCTOR doctor = _dbDP.T_DOCTOR.Find(model.TargetDoctor.ID);
                    if (doctor != null)
                    {
                        _dbDP.T_DOCTOR.Remove(doctor);
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

            model.Messages.Add("sakuzyo","削除しました。");
            return RedirectToAction(nameof(Index));
        }
    }
}