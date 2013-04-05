using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using log4net;
using SnapperSolver.Models;

namespace SnapperSolver.Controllers
{
    public class PlayController : Controller
    {
        private static ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ActionResult Index(string id)
        {
            _logger.Info("Player requested for " + id + ".");
            var matrix = new Matrix();
            matrix.Blocks = Matrix.FromString(id);

            return View(matrix);
        }

    }
}
