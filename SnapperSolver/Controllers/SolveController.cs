using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using SnapperSolver.Models;

namespace SnapperSolver.Controllers
{
    public class SolveController : Controller
    {
        private static ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ActionResult Index(string id, int moves)
        {
            _logger.Info("Solve requested for " + id + " in " + moves + " moves.");
            var matrix = new Matrix();
            matrix.Blocks = Matrix.FromString(id);

            Solver solver = new Solver();
            var results = solver.Solve(matrix, moves);

            return View(results);
        }

    }
}
