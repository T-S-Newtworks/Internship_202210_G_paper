using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Internship_Template.Models.Entity;
using Internship_Template.Models.VM;


namespace Internship_Template.Controllers
{
    public class UsersController : _BaseController
    {
        /// <summary>
        /// 初期画面表示
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index()
        {
            TempData.Remove("model");
            ユーザー画面 model = new ユーザー画面();
            model.Users = _db.T_USER.ToList() ?? new List<T_USER>();
            model.LoginedUser = User;
            TempData["model"] = model;

            return View(model);

        }

        /// <summary>
        /// プロフィール画面表示
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(string id)
        {
            ユーザー画面 model = new ユーザー画面();
            ユーザー画面 IndexData = (ユーザー画面)TempData["model"];
            model.LoginedUser = User;
            model.TargetUser = _db.T_USER.Where(e => e.ID == id).FirstOrDefault();
            if (IndexData != null)
            {
                model.Users = IndexData.Users;
            }
            TempData["model"] = model;

            return View(model);

        }

        /// <summary>
        /// ユーザー追加画面
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ユーザー画面 model = new ユーザー画面();
            model.LoginedUser = User;

            return View(model);
        }

        /// <summary>
        /// ユーザー追加画面
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateComplete (ユーザー画面 user, HttpPostedFileBase uploadImage = null)
        {

            if (uploadImage.ContentType.StartsWith("image/"))
            {
                byte[] data = new Byte[uploadImage.ContentLength];
                uploadImage.InputStream.Read(data, 0, uploadImage.ContentLength);
                string mimeType = uploadImage.ContentType;
                user.TargetUser.ICON = data;  // データ本体
                user.TargetUser.MIMETYPE = mimeType;
                user.TargetUser.T_LOGIN.ID = user.TargetUser.ID;

                // エンティティを追加＆データソースに反映
                _db.T_USER.Add(user.TargetUser);
                _db.SaveChanges();
            }
            else
            {
                // 画像ファイルでない場合はエラー
                ViewData["msg"] = "画像以外はアップロードできません。";
                return View("Create", user);
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 更新画面表示
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit()
        {
            ユーザー画面 model = (ユーザー画面)TempData["model"];
            model.LoginedUser = User;


            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                TempData["model"] = model;
                return View(model);
            }
        }

        /// <summary>
        /// 更新完了時
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditComplete(ユーザー画面 model, HttpPostedFileBase uploadImage = null)
        {
            if (ModelState.IsValid)
            {
                T_USER beforeData = _db.T_USER.Single(e => e.ID == model.TargetUser.ID);

                if (uploadImage != null && uploadImage.ContentType.StartsWith("image/"))
                {
                        byte[] data = new Byte[uploadImage.ContentLength];
                        uploadImage.InputStream.Read(data, 0, uploadImage.ContentLength);
                        string mimeType = uploadImage.ContentType;
                        model.TargetUser.ICON = data;  // データ本体
                        model.TargetUser.MIMETYPE = mimeType;

                }
                else if(uploadImage == null)
                {
                    model.TargetUser.ICON = beforeData.ICON;
                    model.TargetUser.MIMETYPE = beforeData.MIMETYPE;
                }
                else
                {
                        // 画像ファイルでない場合はエラー
                        ViewData["msg"] = "画像以外はアップロードできません。";
                        return View("Edit", model);
                }
                //modelをそのまま突っ込むとbeforeModelの追跡情報が消えるので、Stateの変更時に新規追加扱いになる。
                //その結果PKの一意制約違反になるので登録できない。したがって変更した項目のみ代入する。
                beforeData.FULLNAME = model.TargetUser.FULLNAME;
                beforeData.ICON = model.TargetUser.ICON;
                beforeData.MIMETYPE = model.TargetUser.MIMETYPE;
                _db.Entry(beforeData).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit", model);
        }

        public ActionResult Delete(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ユーザー画面 model = new ユーザー画面();
            model.LoginedUser = User;

            model.TargetUser = _db.T_USER.Where(e => e.ID == id).FirstOrDefault();
            if (model.TargetUser == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        /// <summary>
        /// 削除完了時
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: Users/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExecuteDelete(ユーザー画面 model)
        {
            using (var tra = _db.Database.BeginTransaction())
            {
                try
                {
                    T_USER user = _db.T_USER.Find(model.TargetUser.ID);
                    if (user != null)
                    {
                        _db.T_USER.Remove(user);
                        _db.SaveChanges();
                    }

                    tra.Commit();
                }
                catch(Exception ex)
                {
                    ex.ToString();
                    tra.Rollback();
                }
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //    db.Dispose();
            //}
            base.Dispose(disposing);
        }
    }
}
