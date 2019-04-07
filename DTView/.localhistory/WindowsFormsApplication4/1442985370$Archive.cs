using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Cortex
{
    class Archive : IEnumerable<Scroll>
    {

        public List<Scroll> Library = new List<Scroll>();

        public void addScroll(String nam, String sym, String tex, String rot, String user, String car, String bene, String map)
        {
            Library.Add(new Scroll(nam, sym, tex, rot, user, car, bene, map));
        }

        public Scroll getScrollN(String text)
        {
            foreach (Scroll paper in Library)
            {
                if (paper.Name.Equals(text))
                {
                    return paper;
                }
            }
            return null;
        }

        public Scroll findScrollC(char text)
        {
            foreach (Scroll paper in Library)
            {
                if (paper.Symbol.Equals(text))
                {
                    return paper;
                }
            }
            return null;
        }
        public Scroll getScroll(int i)
        {
            return Library[i].Clone();
        }
        public IEnumerator<Scroll> GetEnumerator()
        {
            return Library.GetEnumerator();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<int> IEnumerable<int>.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
