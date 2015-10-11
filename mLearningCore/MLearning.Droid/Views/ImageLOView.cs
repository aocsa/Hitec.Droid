
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Graphics.Drawables;
using Square.Picasso;

namespace MLearning.Droid
{
	public class ImageLOView : LinearLayout
	{
		Context context;

		string sTitle;

		string sAuthor;
		string sChapter;

		string sUrl;

		public int index;

		public string Url{
			get { return sUrl; }
			set { sUrl = value; 
				ImageView cover = new ImageView (context);
				//cover.LayoutParameters = new LinearLayout.LayoutParams (-1, -1);
				Picasso.With (context).Load (Url).Resize (Configuration.getWidth (160),Configuration.getWidth (160)).CenterCrop().Into (cover);
				this.AddView (cover);



			}
		}

		public string Title{
			get{return sTitle;	}
			set{sTitle = value;	}
		}

		public string Author{
			get{return sAuthor;	}
			set{sAuthor = value;	}
		}

		public string Chapter{
			get{return sChapter;	}
			set{sChapter = value;	}
		}


		Bitmap bmUser;

		public Bitmap ImagenUsuario{
			get{ return bmUser;}
			set{ bmUser = value;}
		}


		public ImageLOView (Context context) :
		base (context)
		{
			this.context = context;
			Initialize ();
		}


		void Initialize ()
		{
			
			this.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth (160), Configuration.getWidth (160));
			//Drawable dr = new BitmapDrawable (getBitmapFromAsset("icons/imdownloading.png"));
			//this.SetBackgroundDrawable (dr);


		}

		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s = context.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}



		private Bitmap _imageBitmap;
		public Bitmap ImageBitmap{
			get{ return _imageBitmap;}
			set{ _imageBitmap = value;			

				Drawable dr = new BitmapDrawable (ImageBitmap);
				this.SetBackgroundDrawable (dr);

			}

		}



	}
}

