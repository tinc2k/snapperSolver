using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using SnapperSolver.Models;
using System.Collections;

namespace SnapperSolver.Controllers
{
    public class CreateController : Controller
    {

        private static ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ActionResult Index()
        {
            try
            {
                _logger.Info("Creator Loaded.");
                return View();
            }
            catch (Exception e)
            {
                _logger.Error("Something went to shit: " + e);
                throw e;
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(FormCollection collection)
        {
            var matrix = new Matrix();

            for (int i = 1; i <= Models.Dimensions.NumberOfRows; i++)
            {
                for (int j = 1; j <= Models.Dimensions.NumberOfColumns; j++)
                {
                    var value = collection["block" + i + j].ToString();
                    if (value != null && value != string.Empty && value != "null")
                    {
                        matrix.Blocks.Add(new Block()
                        {
                            Row = i,
                            Column = j,
                            Type = GetBlockType(value)
                        });
                    }
                }
            }

            /*log created matrix*/
            _logger.Info("User created level:");
            matrix.Log();

            if (matrix.Blocks.Count > 0)    /*if level is valid*/
            {
                return RedirectToAction("Index", "Play", new { id = matrix.ToString() });
            }
           
            return View();
        }

        public BlockType GetBlockType(string value)
        {
            switch (value) {
                case "red":
                    return BlockType.Red;
                case "green":
                    return BlockType.Green;
                case "orange":
                    return BlockType.Orange;
                case "blue":
                    return BlockType.Blue;
                default:
                    return BlockType.Null;
            }
        }

    }
}
