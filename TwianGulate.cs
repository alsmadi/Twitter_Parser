//using Barbar.ShadowApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using WatiN.Core;
using WatiN.Core.Exceptions;
using WatiN.Core.Native.InternetExplorer;
using System.IO;
using System.Collections;
//using TwitterScraper;
using HttpWatch;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;

namespace TwitterScraper
{
    public class TwianGulate : MarshalByRefObject
    {
       
        public void GetTwitter(string inputfile)
        {
             WatiN.Core.IE browser1 = null;
           // var browser = new WatiN.Core.IE();
           
            try {

                //browser.ShowWindow(WatiN.Core.Native.Windows.NativeMethods.WindowShowStyle.Hide);
                //browser = new WatiN.Core.IE("http://twiangulate.com/search");
                //  browser = Browser.AttachTo<WatiN.Core.IE>(Find.ByUrl("http://twiangulate.com/search"));
            //    browser.Navigate().GoToUrl("http://twiangulate.com/search");

               

                //   browser = new WatiN.Core.IE("www.google.com");
                //HttpWatch.Controller ct = new HttpWatch.Controller();
                // HttpWatch.Plugin plugin = ct.IE.New();

                // Attach WatiN to this instance of IE
                //  browser = WatiN.Core.IE.AttachToIE(Find.By("hwnd", plugin.Container.HWND.ToString()));
                //  browser.GoTo("http://twiangulate.com/search");
            }
            catch(Exception ex)
            {
                string t = ex.InnerException.Message;
            }
        //    var users = new List<FacebookUser>();
        StreamWriter swdevs = new StreamWriter("devs.csv");
            StreamWriter swdevs2 = new StreamWriter("devs2.csv");
            StreamWriter swtexts = new StreamWriter("texts.csv");
            StreamWriter swlists = new StreamWriter("lists.csv");
            StreamWriter swelements = new StreamWriter("elements.csv");
            StreamWriter switems = new StreamWriter("items.csv");
            StreamWriter sw1 = new StreamWriter("news.csv");
            StreamReader sr = new StreamReader(inputfile);
          //  Settings.Instance.MakeNewIeInstanceVisible = false;
            //  using (var browser = new IE("http://twiangulate.com/search/"))
            //      Browser browser = new WatiN.Core.IE("http://twiangulate.com/search");
            string line = null;
            Hashtable foll = new Hashtable();
            int count = 0;
            string temp = null;
            while ((line = sr.ReadLine()) != null)
            {

                string[] temp1 = line.Split(',');
        /*        if (count < 1)
                {
                    temp = line;
                    count++;
                    continue;
                }
                else
                { */
                    Follow fill = new Follow(temp1[0], temp1[1]);
                    if (foll.ContainsKey(fill) == false)
                    {
                        foll.Add(fill, fill);
                    }
                    temp = line;
              //  }
            }
            foreach (DictionaryEntry de in foll)
            {
                Follow fol1 = (Follow)de.Key;
                try
                {
                  //  browser.BringToFront();
                    int counter = 0;

                /*    IList<IWebElement> all = browser.FindElements(By.ClassName("tweep-input"));
                    IWebElement element1 = all[0];
                    IWebElement element2 = all[1];
                    element1.SendKeys(fol1.getNode1());
                     element1.SendKeys(Keys.Enter);
                    element2.SendKeys(fol1.getNode2());
                    element2.SendKeys(Keys.Enter); */

              /*      try {
                        //     IList<IWebElement> all1 = browser.FindElements(By.ClassName("btn"));

                        IList<IWebElement> all1 = browser.FindElements(By.TagName("button"));
                        all1[0].Click();
                      //  browser.FindElement(By.LinkText("RETRIEVE")).Click(); ;
                       // browser.FindElement(By.PartialLinkText("btn")).Click();
                       //browser.FindElement(By.ClassName("btn")).Click();
                    }
                    catch( Exception ex)
                    {

                    } 
                  

                    String[] allText = new String[all.Count];
                 //   int i = 0;
                   // foreach (IWebElement element in all)
                   // {
                     //   element.SendKeys("Selenium WebDriver c#");
                       // element.SendKeys(Keys.Enter);
                        //allText[i++] = element.Text;
                   // }
                    IWebElement test = browser.FindElement(By.PartialLinkText("tweep-input"));
                    */
                    // IWebElement test=browser.FindElement(By.PartialLinkText("tweep-input"));
                    foreach (TextField tx in browser1.TextFields)
                    {

                        if (tx != null && tx.ClassName != null && tx.ClassName.Contains("tweep-input"))
                        {
                            if (counter == 0)
                            {
                                tx.Value = fol1.getNode1();
                                counter++;

                            }
                            else
                            {
                                tx.Value = fol1.getNode2(); ;
                            }
                        }
                    }
                    //  browser.TextField(t => t.ClassName== "tweep-input" && t.GetAttributeValue("size") == "50").Value = "value";
                    //    browser.TextField(Find.ByName("tweep-input ui-autocomplete-input").Or(Find.ByClass("tweep-input ui-autocomplete-input"))).Value = "alsmadi";
                    //  browser.TextField(Find.ByName("tweep-input ui-autocomplete-input").Or(Find.ByClass("tweep-input ui-autocomplete-input"))).Value = "ialsmadi";
                    again:
                    try
                    {
                        browser1.Button(Find.ByClass("btn pull-right")).Click();
                    }
                    catch (Exception ex)
                    {
                        System.Threading.Thread.Sleep(1000);
                        goto again;
                    }

                    try
                    {

                        sw1.WriteLine();
                        string text1 = browser1.Text;
                        if (text1.Contains("autoSearchUserData"))
                        {
                            sw1.Write(fol1.getNode1() + "," + fol1.getNode2() + ",");
                            int k1 = text1.IndexOf("autoSearchUserData");
                            int k = text1.IndexOf("ZeroClipboard");
                            string t1 = text1.Substring(k1, k - k1);
                            char[] delimiters = new char[] { ':', ',' };
                            string[] parts = t1.Split(delimiters,
                                             StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < parts.Length; i++)
                            {
                                sw1.Write(parts[i] + ",");
                            }

                        }
                        if (text1.Contains("common_friends"))
                        {
                            sw1.WriteLine(fol1.getNode1() + "," + fol1.getNode2() + ",");

                            string t1 = text1.Substring(text1.IndexOf("common_friends"), 100);
                            char[] delimiters = new char[] { ':', ',' };
                            string[] parts = t1.Split(delimiters,
                                             StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < parts.Length; i++)
                            {
                                sw1.Write(parts[i] + ",");
                            }

                        }

                        foreach (Element el in browser1.Elements)
                        {
                            swelements.WriteLine(el.InnerHtml);
                            swelements.Write(el.Text);
                            if (el.Text.Contains("autoSearchUserData") || el.Text.Contains("common_friends"))
                            {
                                int k = 0;
                            }
                        }

                        foreach (ListItem li in browser1.ListItems)
                        {

                            swelements.WriteLine(li.InnerHtml);
                            swelements.Write(li.Text);
                            if (li.Text.Contains("autoSearchUserData") || li.Text.Contains("common_friends"))
                            {
                                int k = 0;
                            }

                        }
                        foreach (Div tx in browser1.Divs)
                        {
                            foreach (Div tx1 in tx.Divs)
                            {
                                swdevs2.WriteLine(tx1.InnerHtml + "," + tx.OuterText + "," + tx1.Text);
                                if (tx1.Text.Contains("autoSearchUserData") || tx1.Text.Contains("common_friends"))
                                {
                                    int k = 0;
                                }
                                if (tx1.OuterText.Contains("autoSearchUserData") || tx1.OuterText.Contains("common_friends"))
                                {
                                    int k = 0;
                                }
                                if (tx1.InnerHtml.Contains("autoSearchUserData") || tx1.InnerHtml.Contains("common_friends"))
                                {
                                    int k = 0;
                                }
                            }
                            foreach (ListItem item in tx.ListItems)
                            {
                                switems.WriteLine("item" + item.OuterText);

                            }
                            // string ts = tx.Id;
                            //  string id = tx.Id;
                            string text = tx.Text;
                            swdevs.WriteLine(tx.InnerHtml + "," + tx.OuterText);
                            if (tx.Text.Contains("autoSearchUserData") || tx.Text.Contains("common_friends"))
                            {
                                int k = 0;
                            }

                            foreach (TextField tx1 in tx.TextFields)
                            {
                                // string ts = tx.Id;
                                string text2 = tx1.OuterText;
                                swtexts.WriteLine(tx1.InnerHtml + "," + tx1.OuterText);
                            }
                        }
                        foreach (TextField tx in browser1.TextFields)
                        {
                            // string ts = tx.Id;
                            string text = tx.OuterText;
                            swtexts.WriteLine(tx.InnerHtml + "," + tx.OuterText);
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                    //  sw3.WriteLine(browser.Text);
                    /*               foreach (Div tx in browser.Divs)
                                   {
                                       foreach (ListItem item in tx.ListItems)
                                       {
                                           sw.WriteLine("item"+item.OuterText);

                                       }
                                           // string ts = tx.Id;
                                           //  string id = tx.Id;
                                           string text = tx.Text;
                                       sw.WriteLine(tx.InnerHtml + "," + tx.OuterText);

                                       foreach (TextField tx1 in tx.TextFields)
                                       {
                                           // string ts = tx.Id;
                                           string text1 = tx1.OuterText;
                                           sw1.WriteLine(tx1.InnerHtml + "," + tx1.OuterText);
                                       }
                                   }
                                   foreach (TextField tx in browser.TextFields)
                                   {
                                       // string ts = tx.Id;
                                       string text = tx.OuterText;
                                       sw1.WriteLine(tx.InnerHtml + "," + tx.OuterText);
                                   } 
                }
                catch (Exception ex)
                    {

                    }

                    sw.Close();
                    sw1.Close();
                    browser.Button(Find.ByClass("btn pull-right")).Click();
         // browser.TextField(Find.ByName("pass")).Value = password;
          //browser.Form(Find.ById("login_form")).Submit(); */
                    browser1.WaitForComplete();
                }
                catch (System.AggregateException ex1)
                {
                    // sw2.Flush();
                    //    sw3.Flush();
                    switems.Flush();
                    swlists.Flush();
                    swtexts.Flush();
                    swdevs.Flush();
                    swdevs2.Flush();
                    swelements.Flush();
                    sw1.Flush();

                    int k = 15 * 60 * 1000;
                    System.Threading.Thread.Sleep(k);
                    continue;
                }
                catch (ElementNotFoundException ex)
                {
                    // we're already logged in
                    string t = ex.InnerException.Message;
                }
                /*   browser.GoTo("https://www.facebook.com/izzat.alsmadi/friends");
                   var watch = new Stopwatch();
                   watch.Start();

                   Link previousLastLink = null;
                   while (maxTimeoutInMilliseconds.HasValue && watch.Elapsed.TotalMilliseconds < maxTimeoutInMilliseconds.Value)
                   {
                     var lastLink = browser.Links.Where(l => l.GetAttributeValue("data-hovercard") != null
               && l.GetAttributeValue("data-hovercard").Contains("user.php")
               && l.Text != null
             ).LastOrDefault();
                     if (lastLink == null || previousLastLink == lastLink)
                     {
                       break;
                     }

                     var ieElement = lastLink.NativeElement as IEElement;
                     if (ieElement != null)
                     {
                       var htmlElement = ieElement.AsHtmlElement;
                       htmlElement.scrollIntoView();
                       browser.WaitForComplete();
                     }

                     previousLastLink = lastLink;
                   }

                   var links = browser.Links.Where(l => l.GetAttributeValue("data-hovercard") != null
                     && l.GetAttributeValue("data-hovercard").Contains("user.php")
                     && l.Text != null
                   ).ToList();

                   var idRegex = new Regex("id=(?<id>([0-9]+))");
                   foreach (var link in links)
                   {
                     string hovercard = link.GetAttributeValue("data-hovercard");
                     var match = idRegex.Match(hovercard);
                     long id = 0;
                     if (match.Success)
                     {
                       id = long.Parse(match.Groups["id"].Value);
                     }
                     users.Add(new FacebookUser
                     {
                       Name = link.Text,
                       Id = id
                     });
                   }
                 } */
                //  }
            }
            // sw1.Close();

            //  sw3.Close();
            sw1.Close();
            swdevs.Close();
            swelements.Close();
            switems.Close();
            swlists.Close();
            swtexts.Close();
            swdevs2.Close();

          //  return users;
        }

        IWebDriver browser = new FirefoxDriver();
        public void GetTwitter1(Hashtable foll1)
        {
            // WatiN.Core.IE browser = null;
           // var browser1 = new WatiN.Core.IE();
            
            try
            {

                //browser.ShowWindow(WatiN.Core.Native.Windows.NativeMethods.WindowShowStyle.Hide);
                //browser = new WatiN.Core.IE("http://twiangulate.com/search");
                //  browser = Browser.AttachTo<WatiN.Core.IE>(Find.ByUrl("http://twiangulate.com/search"));
                browser.Navigate().GoToUrl("http://twiangulate.com/search");



                //   browser = new WatiN.Core.IE("www.google.com");
                //HttpWatch.Controller ct = new HttpWatch.Controller();
                // HttpWatch.Plugin plugin = ct.IE.New();

                // Attach WatiN to this instance of IE
                //  browser = WatiN.Core.IE.AttachToIE(Find.By("hwnd", plugin.Container.HWND.ToString()));
                //  browser.GoTo("http://twiangulate.com/search");
            }
            catch (Exception ex)
            {
                string t = ex.InnerException.Message;
            }
            //    var users = new List<FacebookUser>();
            //StreamWriter swdevs = new StreamWriter("devs.csv");
            //StreamWriter swdevs2 = new StreamWriter("devs2.csv");
            //StreamWriter swtexts = new StreamWriter("texts.csv");
            //StreamWriter swlists = new StreamWriter("lists.csv");
            //StreamWriter swelements = new StreamWriter("elements.csv");
            //StreamWriter switems = new StreamWriter("items.csv");
            StreamWriter sw1 = new StreamWriter("news.csv");
            StreamWriter sw2 = new StreamWriter("temp.csv");

            foreach (DictionaryEntry de in foll1)
            {
                Follow fol1 = (Follow)de.Key;
                try
                {
                    //  browser.BringToFront();
                    int counter = 0;

                    IList<IWebElement> all = browser.FindElements(By.ClassName("tweep-input"));
                    IWebElement element1 = all[0];
                    element1.Clear();
                    
                    IWebElement element2 = all[1];
                    element2.Clear();
                    element1.SendKeys("");
                    element1.SendKeys(fol1.getNode1());
                    element2.SendKeys("");
                    element2.SendKeys(fol1.getNode2());
                 //   element1.SendKeys(Keys.Enter);
                   // element2.SendKeys(Keys.Enter);

                    try
                    {
                        //     IList<IWebElement> all1 = browser.FindElements(By.ClassName("btn"));

                       
                        IList<IWebElement> all1 = browser.FindElements(By.TagName("button"));
                        try1: if (all1[0].Displayed == true)
                        {

                            try {
                                all1[5].Click();
                            }
                            catch(Exception ex)
                            {

                            }
                            all1[0].Click();
                            System.Threading.Thread.Sleep(2000);
                        }
                        else
                        {
                            System.Threading.Thread.Sleep(2000);
                            goto try1;
                        }
                      //  System.Threading.Thread.Sleep(2000);
                       // all1[0].Click();
                       // System.Threading.Thread.Sleep(2000);

                       
                        //  browser.FindElement(By.LinkText("RETRIEVE")).Click(); ;
                        // browser.FindElement(By.PartialLinkText("btn")).Click();
                        //browser.FindElement(By.ClassName("btn")).Click();
                    }
                     
                    catch (Exception ex)
                    {

                    }
                    List<IWebElement> all5 = browser.FindElements(By.TagName("script")).ToList();
                    foreach (IWebElement field in all5)
                    {
                        string text = field.GetAttribute("innerHTML");
                        IJavaScriptExecutor js = browser as IJavaScriptExecutor;
                        string title = "";
                        try
                        {
                            title = (string)js.ExecuteScript(field.TagName);
                        }
                        catch (Exception ex)
                        {

                        }
                        string title1 = "";
                        try {
                            title1 = (string)js.ExecuteScript(field.Text);
                        }
                        catch (Exception ex)
                        {

                        }
                        //  String htmlCode = (String)((IJavaScriptExecutor)browser).executeScript("return arguments[0].innerHTML;", fields);
                        sw2.WriteLine(field.TagName + "," + field.Text + "," + text + "," + title + "," + title1);
                    }
                    List<IWebElement> textfields1 = new List<IWebElement>();

                    var test1 = browser.FindElements(By.XPath("//*[@type='text/template']"));

                    foreach (IWebElement fields in test1)
                    {
                        textfields1.Add(fields);
                        string text = fields.GetAttribute("innerHTML");
                        IJavaScriptExecutor js = browser as IJavaScriptExecutor;
                        string title = (string)js.ExecuteScript("return arguments[0].innerHTML; ", fields);
                        //  String htmlCode = (String)((IJavaScriptExecutor)browser).executeScript("return arguments[0].innerHTML;", fields);
                        sw2.WriteLine(title);
                    }

                    List<IWebElement> textfields = new List<IWebElement>();

                    var test = browser.FindElements(By.XPath("//*[@type='text']"));

                    foreach (IWebElement fields in test)
                    {
                        textfields.Add(fields);
                    }
                    string text1 = browser.PageSource;
                    //sw2.WriteLine(text1);
                    if (text1.Contains("autoSearchUserData"))
                    {
                        sw1.Write(fol1.getNode1() + "," + fol1.getNode2() + ",");
                        int k1 = text1.IndexOf("autoSearchUserData");
                        int k = text1.IndexOf("ZeroClipboard");
                        string t1 = text1.Substring(k1, k - k1);
                        char[] delimiters = new char[] { ':', ',' };
                        string[] parts = t1.Split(delimiters,
                                         StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < parts.Length; i++)
                        {
                            sw1.Write(parts[i] + ",");
                        }

                    }
                    //sw1.WriteLine(text);
                    sw1.Flush();
                    sw2.Flush();

                    //String[] allText = new String[all.Count];
                    //   int i = 0;
                    // foreach (IWebElement element in all)
                    // {
                    //   element.SendKeys("Selenium WebDriver c#");
                    // element.SendKeys(Keys.Enter);
                    //allText[i++] = element.Text;
                    // }
                    //IWebElement test = browser.FindElement(By.PartialLinkText("tweep-input"));

                    // IWebElement test=browser.FindElement(By.PartialLinkText("tweep-input"));
                   
                   
                }
                catch (System.AggregateException ex1)
                {
                    // sw2.Flush();
                    //    sw3.Flush();
                  //  switems.Flush();
                    //swlists.Flush();
                    //swtexts.Flush();
                    //swdevs.Flush();
                    //swdevs2.Flush();
                    //swelements.Flush();
                    sw1.Flush();

                    int k = 15 * 60 * 1000;
                    System.Threading.Thread.Sleep(k);
                    continue;
                }
                catch (ElementNotFoundException ex)
                {
                    // we're already logged in
                    string t = ex.InnerException.Message;
                }
               
                //  }
            }
            // sw1.Close();

            //  sw3.Close();
            sw1.Close();
            sw2.Close();
            //swdevs.Close();
            //swelements.Close();
            //switems.Close();
            //swlists.Close();
            //swtexts.Close();
            //swdevs2.Close();

            //  return users;
        }

        public void UnloadApi()
        {
        }
    }
}
