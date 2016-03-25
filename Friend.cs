using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterScraper
{
    public class Friend : IComparable
    {
        string ID1;
        string node1;
        string ID2;
        string node2;
        int weight;


        int IComparable.CompareTo(object obj)
        {
            Friend other = obj as Friend;
            try
            {
                if (other != null)
                {
                    if (this.node1 == other.getNode1())
                    {
                        return this.node2.CompareTo(other.getNode2());
                    }
                    // Default to salary sort. [High to low]
                    // return other.Salary.CompareTo(this.Salary);
                    return this.node1.CompareTo(other.node1);
                }
                else
                    throw new ArgumentException("Object is not a myKey");
            }
            catch (Exception ex)
            {
                return this.node1.CompareTo(other.getNode1());
            }

        }
        public Friend(string node10, string node20)
        {
            node1 = node10;
            node2 = node20;
        }
        public Friend(string node10, string node20, int wt)
        {
            node1 = node10;
            node2 = node20;
            weight = wt;
        }
        public Friend(string ID11, string node10, string ID22, string node20)
        {
            node1 = node10;
            node2 = node20;
            ID1 = ID11;
            ID2 = ID22;
        }
        public string getNode1()
        {
            return node1;
        }

        public int getWt()
        {
            return weight;

        }

        public void setWT(int wt)
        {
            weight = wt;
        }
        public void setNode1(string node11)
        {
            node1 = node11;
        }

        public string getNode2()
        {
            return node2;
        }
        public string getID2()
        {
            return ID2;
        }
        public string getID1()
        {
            return ID1;
        }
        public void setNode2(string node22)
        {
            node2 = node22;
        }
    }
}
