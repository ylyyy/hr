using Entity;
using IBLL;
using IocContainer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Filters;

namespace UI.Controllers
{
    [Login]
    public class clientController : Controller
    {

        ISalaryBll isbll = IocContain.CreateAll<ISalaryBll>("yibll", "sarybll");
        Iconfig_file_first_kindBLL iffk = IocContain.CreateAll<Iconfig_file_first_kindBLL>("yibll", "config_file_first_kindBLL");
        Iconfig_file_second_kindBLL ifsk = IocContain.CreateAll<Iconfig_file_second_kindBLL>("yibll", "config_file_second_kindBLL");
        Iconfig_file_third_kindBLL iftk = IocContain.CreateAll<Iconfig_file_third_kindBLL>("yibll", "config_file_third_kindBLL");

        Isalary_standard_detailsBll isd = IocContain.CreateAll<Isalary_standard_detailsBll>("yibll", "ssdbll");
        // GET: client
        [HttpGet]
        public ActionResult salary_item()
        {
            List<salary> list = isbll.SelectAll();
            return View(list);
        }
        //薪酬项目名称添加
        [HttpPost]
        public ActionResult salary_item(string itemName)
        {
            salary sary = new salary()
            {
                sid_sname = itemName
            };
            if (isbll.Add(sary) > 0)
            {
                return Content("<script>alert('添加成功！');location.href='/client/salary_item';</script>");
            }
            else
            {
                return Content("<script>alert('添加失败！');location.href='/client/salary_item';</script>");
            }
        }
        public ActionResult salary_item_register()
        {
            return View();
        }
        //薪酬项目名称删除   如果薪酬项目被引用则不能删除
        public ActionResult Del(int id)
        {
            if (isd.SelectWhere(e => e.item_id == id).Count > 0)
            {
                return Content("<script>alert('该薪酬项目被使用中！无法删除！！');location.href='/client/salary_item';</script>");
            }
            else {
            salary sary = new salary()
            {
                sid_id = id
            };
            if (isbll.Delete(sary) > 0)
            {
                return Content("<script>alert('删除成功！');location.href='/client/salary_item';</script>");
            }
            else
            {
                return Content("<script>alert('删除失败！');location.href='/client/salary_item';</script>");
            }
        }
        }



        
        Iconfig_public_charBLL ipcc = IocContain.CreateAll<Iconfig_public_charBLL>("yibll", "config_public_charBLL");
        Iconfig_major_kindBLL ifmk = IocContain.CreateAll<Iconfig_major_kindBLL>("yibll", "config_major_kindBLL");
        Iconfig_majorBLL ifm = IocContain.CreateAll<Iconfig_majorBLL>("yibll", "config_majorBLL");
        //-I级机构设置
        #region
        public ActionResult first_kind()
        {
            return View();
        }
        public ActionResult first_kindIndex()
        {
            return Content(JsonConvert.SerializeObject(iffk.SelectAll()));
        }
        public ActionResult first_kind_register_success()
        {
            return View();
        }
        // GET: client/Create
        public ActionResult first_kind_register()
        {
            return View();
        }

        // POST: client/Create
        [HttpPost]
        public ActionResult first_kind_register(config_file_first_kind collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (iffk.Add(collection) > 0)
                {
                    return Content("<script>location.href='/client/first_kind_register_success';</script>");
                }
                else
                {
                    return Content("<script>alert('添加失败');</script>");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: client/Edit/5
        public ActionResult first_kind_change(int id)
        {
            config_file_first_kind cffk = iffk.SelectWhere(e => e.ffk_id == id).FirstOrDefault();
            return View(cffk);
        }

        // POST: client/Edit/5
        [HttpPost]
        public ActionResult first_kind_change(config_file_first_kind collection)
        {
            if (iffk.Update(collection) > 0)
            {
                return Content("<script>location.href='/client/first_kind_change_success';</script>");
            }
            else
            {
                return Content("<script>alert('修改失败');</script>");
            }
        }
        public ActionResult first_kind_change_success()
        {
            return View();
        }
        public ActionResult first_kindDelete(int id)
        {
            try
            {
                config_file_first_kind c = new config_file_first_kind
                {
                    ffk_id = (short)id
                };
                // TODO: Add delete logic here
                if (iffk.Delete(c) > 0)
                {
                    return Content("<script>location.href='/client/first_delete_success';</script>");
                }
                else
                {
                    return Content("<script>alert('删除失败');</script>");
                }
            }
            catch
            {
                return View();
            }
        }
        public ActionResult first_delete_success()
        {
            return View();
        }
        #endregion
        private void NewMethod()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<config_file_first_kind> l = iffk.SelectAll();
            foreach (config_file_first_kind c in l)
            {
                SelectListItem s = new SelectListItem
                {
                    Text = c.first_kind_name,
                    Value = c.first_kind_id
                };
                list.Add(s);
            }
            SelectListItem ll = new SelectListItem()
            {
                Text = "请选择一级机构名称",
                Value = "0"
            };
            list.Insert(0, ll);
            ViewData["s"] = list;
        }
        //II级机构设置
        #region
        public ActionResult second_kind()
        {
            return View();
        }
        public ActionResult second_kindIndx()
        {
            return Content(JsonConvert.SerializeObject(ifsk.SelectAll()));
        }
        [HttpGet]
        public ActionResult second_kind_register()
        {
            NewMethod();
            return View();
        }



        [HttpPost]
        public ActionResult second_kind_register(config_file_second_kind c)
        {
            if (c.first_kind_id == "0")
            {
                NewMethod();
                return View(c);
            }
            else
            {
                config_file_first_kind cffk = iffk.SelectWhere(e => e.first_kind_id == c.first_kind_id).FirstOrDefault();
                c.first_kind_name = cffk.first_kind_name;
                if (ifsk.Add(c) > 0)
                {
                    return Content("<script>location.href='second_kind_register_success';</script>");
                }
                else
                {
                    return Content("<script>alert('添加失败');</script>");
                }
            }
        }
        public ActionResult second_kind_register_success()
        {
            NewMethod();
            return View();
        }
        [HttpGet]
        public ActionResult second_kind_change(int id)
        {
            config_file_second_kind cfsk = ifsk.SelectWhere(e => e.fsk_id == id).FirstOrDefault();
            return View(cfsk);
        }
        [HttpPost]
        public ActionResult second_kind_change(config_file_second_kind c)
        {
            config_file_first_kind cffk = iffk.SelectWhere(e => e.first_kind_name == c.first_kind_name).FirstOrDefault();
            c.first_kind_id = cffk.first_kind_id;
            if (ifsk.Update(c) > 0)
            {
                return Content("<script>location.href='/client/second_kind_change_success';</script>");
            }
            else
            {
                return View(c);
            }
        }
        public ActionResult second_delete(int id)
        {
            config_file_second_kind cfsk = new config_file_second_kind()
            {
                fsk_id = (short)id
            };
            if (ifsk.Delete(cfsk) > 0)
            {
                return Content("<script>location.href='/client/second_delete_success';</script>");
            }
            else
            {
                return Content("<script>alert('删除失败');location.href='/client/second_kind';</script>");
            }
        }
        public ActionResult second_delete_success()
        {
            return View();
        }
        public ActionResult second_kind_change_success()
        {
            return View();
        }
        #endregion
        //III级机构设置
        #region
        public ActionResult third_kind()
        {
            return View();
        }
        public ActionResult third_kindIndex()
        {
            return Content(JsonConvert.SerializeObject(iftk.SelectAll()));
        }
        [HttpGet]
        public ActionResult third_kind_register()
        {
            List<SelectListItem> list1 = new List<SelectListItem>();
            List<config_file_second_kind> l = ifsk.SelectAll();
            foreach (config_file_second_kind c in l)
            {
                SelectListItem s = new SelectListItem
                {
                    Text = c.second_kind_name,
                    Value = c.second_kind_id,
                };
                list1.Add(s);
            }
            SelectListItem s1 = new SelectListItem
            {
                Text = "请选择II级机构名称",
                Value = "0"
            };
            list1.Insert(0, s1);
            ViewData["ss"] = list1;
            NewMethod();
            return View();
        }
        [HttpPost]
        public ActionResult third_kind_register(config_file_third_kind cftk)
        {
            config_file_first_kind cffk = iffk.SelectWhere(e => e.first_kind_id == cftk.first_kind_id).FirstOrDefault();
            config_file_second_kind cfsk = ifsk.SelectWhere(e => e.second_kind_id == cftk.second_kind_id).FirstOrDefault();
            cftk.first_kind_name = cffk.first_kind_name;
            cftk.second_kind_name = cfsk.second_kind_name;
            if (iftk.Add(cftk) > 0)
            {
                return Content("<script>location.href='/client/third_kind_register_success';</script>");
            }
            else
            {
                return Content("<script>alert('添加失败');</script>");
            }
        }
        public ActionResult third_kind_register_success()
        {
            return View();
        }
        [HttpGet]
        public ActionResult third_kind_change(int id)
        {
            config_file_third_kind cftk = iftk.SelectWhere(e => e.ftk_id == id).FirstOrDefault();
            return View(cftk);
        }
        [HttpPost]
        public ActionResult third_kind_change(config_file_third_kind cftk)
        {
            config_file_first_kind cffk = iffk.SelectWhere(e => e.first_kind_name == cftk.first_kind_name).FirstOrDefault();
            config_file_second_kind cfsk = ifsk.SelectWhere(e => e.second_kind_name == cftk.second_kind_name).FirstOrDefault();
            cftk.first_kind_id = cffk.first_kind_id;
            cftk.second_kind_id = cfsk.second_kind_id;
            if (iftk.Update(cftk) > 0)
            {
                return Content("<script>location.href='/client/third_kind_change_success'</script>");
            }
            else
            {
                return Content("<script>alert('修改失败');</script>");
            }
        }
        public ActionResult third_kind_change_success()
        {
            return View();
        }
        public ActionResult third_delete(int id)
        {
            config_file_third_kind c = new config_file_third_kind
            {
                ftk_id = (short)id
            };
            if (iftk.Delete(c) > 0)
            {
                return Content("<script>location.href='/client/third_delete_success'</script>");
            }
            else
            {
                return Content("<script>alert('删除失败');</script>");
            }
        }
        public ActionResult third_delete_success()
        {
            return View();
        }
        #endregion
        //职称设置
        #region
        public ActionResult profession_design()
        {
            return View();
        }
        public ActionResult profession_designIndex()
        {
            return Content(JsonConvert.SerializeObject(ipcc.SelectWhere(e=>e.attribute_kind.Equals("职称"))));
        }
        public ActionResult profession_designDelete(int id)
        {
            config_public_char cpc = new config_public_char
            {
                pbc_id = (short)id
            };
            if (ipcc.Delete(cpc) > 0)
            {
                return Content("<script>alert('删除成功');location.href='/client/profession_design';</script>");
            }
            else
            {
                return Content("<script>alert('删除失败');</script>");
            }
        }
        #endregion
        //职位分类设置
        #region
        public ActionResult major_kind()
        {
            return View();
        }
        public ActionResult major_kindIndex()
        {
            return Content(JsonConvert.SerializeObject(ifmk.SelectAll()));
        }
        [HttpGet]
        public ActionResult major_kind_add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult major_kind_add(config_major_kind cmk)
        {
            if (ifmk.Add(cmk) > 0)
            {
                return Content("<script>alert('添加成功');location.href='/client/major_kind';</script>");
            }
            else
            {
                return Content("<script>alert('添加失败');</script>");
            }
        }
        public ActionResult major_kind_delete(int id)
        {
            config_major_kind cmk = new config_major_kind
            {
                mfk_id = (short)id
            };
            if (ifmk.Delete(cmk) >0)
            {
                return Content("<script>alert('删除成功');location.href='/client/major_kind';</script>");
            }
            else
            {
                return Content("<script>alert('删除失败');</script>");
            }
        }
        #endregion
        //职位设置
        #region
        public ActionResult major()
        {
            return View();
        }
        public ActionResult majorIndex()
        {
            return Content(JsonConvert.SerializeObject(ifm.SelectAll()));
        }
        [HttpGet]
        public ActionResult major_add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult major_add(config_major cmk)
        {
            if (ifm.Add(cmk) > 0)
            {
                return Content("<script>alert('添加成功');location.href='/client/major';</script>");
            }
            else
            {
                return Content("<script>alert('添加失败');</script>");
            }
        }
        public ActionResult major_delete(int id)
        {
            config_major cmk = new config_major
            {
                mak_id = (short)id
            };
            if (ifm.Delete(cmk) > 0)
            {
                return Content("<script>alert('删除成功');location.href='/client/major';</script>");
            }
            else
            {
                return Content("<script>alert('删除失败');</script>");
            }
        }
        #endregion
        //公共属性设置
        #region
        public ActionResult public_char()
        {
            return View();
        }
        public ActionResult public_charIndex()
        {
            return Content(JsonConvert.SerializeObject(ipcc.SelectAll()));
        }
        [HttpGet]
        public ActionResult public_char_add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult public_char_add(config_public_char cmk)
        {
            if (ipcc.Add(cmk) > 0)
            {
                return Content("<script>alert('添加成功');location.href='/client/public_char';</script>");
            }
            else
            {
                return Content("<script>alert('添加失败');</script>");
            }
        }
        public ActionResult public_char_delete(int id)
        {
            config_public_char cmk = new config_public_char
            {
                pbc_id = (short)id
            };
            if (ipcc.Delete(cmk) > 0)
            {
                return Content("<script>alert('删除成功');location.href='/client/public_char';</script>");
            }
            else
            {
                return Content("<script>alert('删除失败');</script>");
            }
        }
        #endregion
    }
}