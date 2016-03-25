using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterScraper
{
    public class Follow: IComparable
    {
        string node1;
        string node2;


        int IComparable.CompareTo(object obj)
        {
            Follow other = obj as Follow;
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
        public Follow(string node10, string node20)
        {
            node1 = node10;
            node2 = node20;
        }
        public string getNode1()
        {
            return node1;
        }
        public void setNode1(string node11)
        {
            node1 = node11;
        }

        public string getNode2()
        {
            return node2;
        }
        public void setNode2(string node22)
        {
            node2 = node22;
        }
    }
}
