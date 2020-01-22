using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace WindowsFormsApp1
{
	public partial class Form1 : Form
	{

		public Subject<string> s1 = new Subject<string>();
		public Subject<string> s2 = new Subject<string>();
		public Form1()
		{
			InitializeComponent();
			
			Observable.Zip(
				s1, s2
			).Subscribe(x =>
			{
				for (var i = x.Count - 1; i >= 0; i--)
				{
					Console.WriteLine(i.ToString());
				}
			});

//			Observable
//				.FromEventPattern(e => button1.Click += e, e => button1.Click -= e)
//				.Subscribe(x => textBox1.Text = "123");

//			button1.Click += (sender, args) =>
//			{
//				Debug.WriteLine("1");
//				Console.WriteLine("1");
//				textBox1.Text = "11";
//			};
//			button1.Click += (sender, args) =>
//			{
//				Debug.WriteLine("2");
//				Console.WriteLine("2");
//				throw new Exception();
//				textBox2.Text = "22";
//			};
//			button1.Click += (sender, args) =>
//			{
//				Debug.WriteLine("3");
//				Console.WriteLine("3");
//				textBox3.Text = "33";
//			};
		}

		private void button1_Click(object sender, EventArgs e)
		{
//			var httpClient = new HttpClient();

			s1.OnNext(DateTime.Now.ToLongTimeString());
			this.textBox1.Show();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			s2.OnNext(DateTime.Now.ToLongTimeString());
		}
	}
}