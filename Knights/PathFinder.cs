using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Knights
{
    class PathFinder
    {
        Map map;
        Step currentStep;
        StateController stateController;
        int x;
        int y;

        static public Map Map { get; set; }

        public PathFinder(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public List<Coord> GetPath(int size)
        {
            FindPath(size);
            return ExtractCoords();
        }
        private List<Coord> ExtractCoords()
        {
            Step temp = currentStep;
            List<Coord> coords = new List<Coord>();
            while (!(temp is null))
            {
                coords.Insert(0, new Coord { X = temp.X, Y = temp.Y });
                temp = temp.PrevStep;
            }

            return coords;
        }
        private void FindPath(int size)
        {
            Map = map = new Map(size);
            currentStep = new Step(x, y);
            stateController = new StateController();
            do
            {
                map.MarkPosition(currentStep.X, currentStep.Y);
                if (map.GetScore() == map.size * map.size)
                    return;
                if (currentStep.StepsCheck() > 0)
                {
                    currentStep.Sort();
                    currentStep = currentStep.PosibleSteps.First();
                    currentStep.PrevStep.NextStep = currentStep;
                    stateController.State = map.Load();
                }
                else
                {
                    //FindPath(size);
                    currentStep.PrevStep?.RemoveBadWay(currentStep);
                    currentStep = currentStep.PrevStep;
                    map.Save(stateController.State);
                }
            } while (!(currentStep is null));
        }
    }
}
