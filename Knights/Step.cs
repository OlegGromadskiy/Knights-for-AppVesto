using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knights
{
    class Step
    {
        public int X { get; set; }
        public int Y { get; set; }
        bool isStepsChecked;
        public Step PrevStep { get; set; }
        public Step NextStep { get; set; }
        public List<Step> PosibleSteps { get; set; }
        public Step(int x, int y)
        {
            X = x;
            Y = y;

            isStepsChecked = false;
            PosibleSteps = new List<Step>();
        }
        public Step(int x, int y, Step prevStep) : this(x, y)
        {
            PrevStep = prevStep;
        }
        public void RemoveBadWay(Step badWay)
        {
            PosibleSteps.Remove(badWay);
        }
        public int StepsCheck()
        {
            if (isStepsChecked)
                return PosibleSteps.Count;

            if (Y - 2 >= 0 && X - 1 >= 0 && PathFinder.Map.CheckPosition(X - 1, Y - 2))
                PosibleSteps.Add(new Step(X - 1, Y - 2, this));
            if (Y - 2 >= 0 && X + 1 < PathFinder.Map.size && PathFinder.Map.CheckPosition(X + 1, Y - 2))
                PosibleSteps.Add(new Step(X + 1, Y - 2, this));

            if (Y + 2 < PathFinder.Map.size && X - 1 >= 0 && PathFinder.Map.CheckPosition(X - 1, Y + 2))
                PosibleSteps.Add(new Step(X - 1, Y + 2, this));
            if (Y + 2 < PathFinder.Map.size && X + 1 < PathFinder.Map.size && PathFinder.Map.CheckPosition(X + 1, Y + 2))
                PosibleSteps.Add(new Step(X + 1, Y + 2, this));

            if (Y - 1 >= 0 && X - 2 >= 0 && PathFinder.Map.CheckPosition(X - 2, Y - 1))
                PosibleSteps.Add(new Step(X - 2, Y - 1, this));
            if (Y - 1 >= 0 && X + 2 < PathFinder.Map.size && PathFinder.Map.CheckPosition(X + 2, Y - 1))
                PosibleSteps.Add(new Step(X + 2, Y - 1, this));

            if (Y + 1 < PathFinder.Map.size && X - 2 >= 0 && PathFinder.Map.CheckPosition(X - 2, Y + 1))
                PosibleSteps.Add(new Step(X - 2, Y + 1, this));
            if (Y + 1 < PathFinder.Map.size && X + 2 < PathFinder.Map.size && PathFinder.Map.CheckPosition(X + 2, Y + 1))
                PosibleSteps.Add(new Step(X + 2, Y + 1, this));

            isStepsChecked = true;
            return PosibleSteps.Count;
        }
        public void Sort()
        {
            foreach (var step in PosibleSteps)
                step.StepsCheck();

            PosibleSteps = PosibleSteps.OrderBy(s => s.PosibleSteps.Count).ToList();
            var test = PosibleSteps.Select(s => s.PosibleSteps.Count).ToArray();
        }
    }
}
